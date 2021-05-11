using System;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.DTO.Authentication;
using Cleverbit.CodingTask.Tests.Shared;
using Cleverbit.CodingTask.Tests.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Cleverbit.CodingTask.Tests {
    public class AuthenticationTests : BaseTest {

        private readonly AuthenticationTask task;
        
        public AuthenticationTests() {
            task = new AuthenticationTask(CreateClient(), null);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\n")]
        [InlineData("       ")]
        public async Task SignIn_with_empty_username(string userName) {
            var input = new SignInInput {
                Password = faker.Random.String(10),
                UserName = userName
            };

            var response = await task.SignIn(input);

            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\n")]
        [InlineData("       ")]
        public async Task SignIn_with_empty_password(string password) {
            var input = new SignInInput {
                Password = password,
                UserName = faker.Random.String(10)
            };

            var response = await task.SignIn(input);

            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task SignIn_user_not_found() {
            var input = new SignInInput {
                UserName = Guid.NewGuid().ToString(),
                Password = "password"
            };

            var response = await task.SignIn(input);

            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task SignIn_invalid_credentials() {
            var input = new SignInInput {
                UserName = "User1",
                Password = "invalid-password"
            };

            var response = await task.SignIn(input);

            response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        }

        [Fact]
        public async Task SignIn_ok() {
            var input = new SignInInput {
                UserName = "User1",
                Password = "Password1"
            };

            var response = await task.SignIn(input);

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
