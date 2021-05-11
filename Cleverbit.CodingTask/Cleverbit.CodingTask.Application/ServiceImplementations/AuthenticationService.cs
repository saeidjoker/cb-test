using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.DTO.Authentication;
using Cleverbit.CodingTask.Application.Models.Exceptions;
using Cleverbit.CodingTask.Application.Services;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.Models;
using Cleverbit.CodingTask.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingTask.Application.ServiceImplementations {

    public class AuthenticationService : IAuthenticationService {
        private readonly CodingTaskContext db;
        private readonly IHashService hashService;

        public AuthenticationService(CodingTaskContext db, IHashService hashService) {
            this.db = db;
            this.hashService = hashService;
        }

        public async Task SignIn(SignInInput input) {
            // find user by user-name
            var user = await findByUserName(input.UserName);

            if (user == null) {
                throw new ApiError(404, "Username not found!");
            }

            // check password
            var hashedPassword = await hashService.HashText(input.Password);
            if (hashedPassword.Equals(user.Password) == false) {
                throw new ApiError(403, "Invalid credentials!");
            }
        }

        private Task<User> findByUserName(string userName) {
            return db.Users.FirstOrDefaultAsync(a => a.UserName == userName);
        }
    }

}