using System;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cleverbit.CodingTask.Host.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class PlayController : BaseController {
        
        private readonly IMatchService matchService;

        public PlayController(IMatchService matchService) {
            this.matchService = matchService;
        }

        [HttpPost("{matchId:Guid}")]
        public async Task Play([FromRoute] Guid matchId) {
            await matchService.Play(GetUserId(), matchId);
        }
    }
}
