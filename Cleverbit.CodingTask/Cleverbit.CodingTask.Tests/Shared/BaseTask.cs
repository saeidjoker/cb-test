using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Utilities;

namespace Cleverbit.CodingTask.Tests.Shared {

    public abstract class BaseTask {
        protected readonly HttpClient client;
        private readonly FlyweightUser user;

        protected BaseTask(HttpClient client, FlyweightUser user) {
            this.client = client;
            this.user = user;
        }

        protected virtual void BeforeRequest() {
            if (user == null || string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password)) {
                return;
            }
            var headers = client.DefaultRequestHeaders;
            headers.Remove("Authorization");
            headers.TryAddWithoutValidation("Authorization", "Basic " + $"{user.UserName}:{user.Password}".ToBase64());
        }

        protected async Task<HttpResponseMessage> GetAsync(string url) {
            BeforeRequest();

            return await client.GetAsync(url);
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, object body) {
            BeforeRequest();

            return await client.PostAsJsonAsync(url, body);
        }

        protected async Task<HttpResponseMessage> PutAsync(string url, object body) {
            BeforeRequest();

            return await client.PutAsJsonAsync(url, body);
        }

        protected async Task<HttpResponseMessage> PatchAsync(string url, object body) {
            BeforeRequest();

            return await client.PatchAsync(url, JsonContent.Create(body));
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url) {
            BeforeRequest();

            return await client.DeleteAsync(url);
        }
    }

}