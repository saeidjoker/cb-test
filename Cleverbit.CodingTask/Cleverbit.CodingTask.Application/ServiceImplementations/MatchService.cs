using System;
using System.Linq;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.Models;
using Cleverbit.CodingTask.Application.Models.Exceptions;
using Cleverbit.CodingTask.Application.Models.Shared;
using Cleverbit.CodingTask.Application.Services;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.DateAndTime;
using Cleverbit.CodingTask.Data.Models;
using Cleverbit.CodingTask.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingTask.Application.ServiceImplementations {

    public class MatchService : IMatchService {
        private const uint PageSize = 20;

        private readonly CodingTaskContext db;
        private readonly IScoreGenerator scoreGenerator;
        private readonly IClock clock;

        public MatchService(CodingTaskContext db, IScoreGenerator scoreGenerator, IClock clock) {
            this.db = db;
            this.scoreGenerator = scoreGenerator;
            this.clock = clock;
        }

        public async Task<Match> GetCurrentMatchToPlay(int userId) {
            var now = clock.Now().ToUnixTime();

            var found = await db.Matches
                .AsNoTracking()
                .Where(a => a.ExpiresTimestamp > now)
                .GroupJoin(db.Plays, match => match.Id, play => play.MatchId, (match, plays) => new {
                    match,
                    plays
                })
                .SelectMany(a => a.plays.DefaultIfEmpty(), (a, play) => new {
                    a.match,
                    play
                })
                .Where(a => a.play.UserId != userId)
                .OrderBy(a => a.match.ExpiresTimestamp)
                .Select(a => a.match)
                .FirstOrDefaultAsync();

            if (found == null) {
                throw new ApiError(404, "There's currently no match running");
            }

            return found;
        }

        public async Task<Page<FinishedMatchModel>> GetFinishedMatches(int pageIndex) {
            var now = clock.Now().ToUnixTime();
            int skip = (int) (PageSize * pageIndex);
            int take = (int) PageSize;

            var query = db.Matches
                .AsNoTracking()
                .Where(a => a.ExpiresTimestamp <= now)
                .GroupJoin(db.Plays, match => match.Id, play => play.MatchId, (match, plays) => new {
                    match,
                    plays
                })
                .SelectMany(a => a.plays.DefaultIfEmpty(), (a, play) => new {
                    a.match,
                    play
                })
                .GroupJoin(db.Users, a => a.play.UserId, user => user.Id, (a, users) => new {
                    a.match,
                    a.play,
                    users
                })
                .SelectMany(a => a.users.DefaultIfEmpty(), (a, user) => new {
                    a.match,
                    a.play,
                    user
                })
                // todo: fix this later!!! This loads everything in memory. I hate EF
                .AsEnumerable()
                .GroupBy(a => new {
                    a.match.Id,
                    a.match.ExpiresTimestamp,
                    a.user
                })
                .Where(a => a.Key.user != null)
                .Select(a => new {
                    item = a.Key,
                    winnerScore = a.Max(aa => aa.play.Score)
                })
                .Select(a => new FinishedMatchModel {
                    MatchId = a.item.Id,
                    ExpiresTimestamp = a.item.ExpiresTimestamp,
                    WinnerUserName = a.item.user.UserName,
                    WinnerScore = a.winnerScore
                });

            var total = query.Count();

            var items = query
                .OrderByDescending(a => a.ExpiresTimestamp)
                .Skip(skip)
                .Take(take)
                .ToList();

            return new Page<FinishedMatchModel> {
                Items = items,
                Total = total
            };
        }

        private Task<bool> hasPlayedTheMatchBefore(int userId, Guid matchId) {
            return db.Plays.AnyAsync(a => a.UserId == userId && a.MatchId == matchId);
        }

        private Task<bool> matchExists(Guid matchId) {
            return db.Matches.AnyAsync(a => a.Id == matchId);
        }

        public async Task Play(int userId, Guid matchId) {
            if (await matchExists(matchId) == false) {
                throw new ApiError(StatusCodes.Status404NotFound, "Match was not found!");
            }

            if (await hasPlayedTheMatchBefore(userId, matchId)) {
                throw new ApiError(StatusCodes.Status409Conflict, "Already played this match!");
            }

            var play = new Play {
                Score = await scoreGenerator.GenerateScoreForUser(userId),
                DateTimestamp = clock.Now().ToUnixTime(),
                Id = Guid.NewGuid(),
                MatchId = matchId,
                UserId = userId
            };

            await db.Plays.AddAsync(play);
            await db.SaveChangesAsync();
        }
    }

}