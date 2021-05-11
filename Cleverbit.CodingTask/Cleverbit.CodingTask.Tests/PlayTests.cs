using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.Models.Exceptions;
using Cleverbit.CodingTask.Application.Services;
using Cleverbit.CodingTask.Tests.Shared;
using Cleverbit.CodingTask.Tests.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cleverbit.CodingTask.Tests {
    public class PlayTests : BaseTest {
        private readonly PlayTask task;
        private readonly FlyweightUser user;

        public PlayTests() {
            user = new FlyweightUser();
            task = new PlayTask(CreateClient(), user);
        }

        [Fact]
        public async Task User_plays_a_match() {
            var matchService = Services.GetService<IMatchService>();

            var match = await matchService.GetCurrentMatchToPlay(1);

            if (match != null) {
                await matchService.Play(1, match.Id);
                await Assert.ThrowsAsync<ApiError>(() => matchService.Play(1, match.Id));
            }
        }
    }
}
