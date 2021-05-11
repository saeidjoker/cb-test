using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Tests.Shared;

namespace Cleverbit.CodingTask.Tests.Tasks {
    public class PlayTask : BaseTask {
        public PlayTask(HttpClient client, FlyweightUser user) : base(client, user) {
        }

        public Task<HttpResponseMessage> Play(Guid matchId) {
            return PostAsync($"api/play/{matchId}", null);
        }
    }
}
