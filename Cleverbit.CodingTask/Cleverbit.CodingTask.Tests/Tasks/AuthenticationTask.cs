using System.Net.Http;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.DTO.Authentication;
using Cleverbit.CodingTask.Tests.Shared;

namespace Cleverbit.CodingTask.Tests.Tasks {

    public class AuthenticationTask : BaseTask {
        public AuthenticationTask(HttpClient client, FlyweightUser user) : base(client, user) {
        }

        public Task<HttpResponseMessage> SignIn(SignInInput input) {
            return PostAsync("api/authentication/sign-in", input);
        }
    }

}