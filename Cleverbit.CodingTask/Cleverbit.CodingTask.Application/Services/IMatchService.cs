using System;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.Models;
using Cleverbit.CodingTask.Application.Models.Shared;
using Cleverbit.CodingTask.Data.Models;

namespace Cleverbit.CodingTask.Application.Services {
    public interface IMatchService {
        Task<Match> GetCurrentMatchToPlay(int userId);

        Task<Page<FinishedMatchModel>> GetFinishedMatches(int pageIndex);

        Task Play(int userId, Guid matchId);
    }
}
