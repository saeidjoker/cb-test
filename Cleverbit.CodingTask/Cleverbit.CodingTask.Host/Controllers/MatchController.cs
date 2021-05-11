using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.Models;
using Cleverbit.CodingTask.Application.Models.Shared;
using Cleverbit.CodingTask.Application.Services;
using Cleverbit.CodingTask.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cleverbit.CodingTask.Host.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : BaseController {
        private readonly IMatchService matchService;

        public MatchController(IMatchService matchService) {
            this.matchService = matchService;
        }

        [HttpGet("next-match")]
        public Task<Match> GetNextMatchToPlay() {
            return matchService.GetCurrentMatchToPlay(GetUserId());
        }

        [HttpGet("list-finished-matches/{pageIndex:int}")]
        public Task<Page<FinishedMatchModel>> ListFinishedMatches([FromRoute] int pageIndex) {
            return matchService.GetFinishedMatches(pageIndex);
        }
    }

}