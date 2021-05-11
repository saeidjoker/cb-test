using System;
using Cleverbit.CodingTask.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Data.Models;

namespace Cleverbit.CodingTask.Data {

    public static class CodingTaskContextExtensions {

        /// <summary>
        /// Initializes database and seeds some data
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hashService"></param>
        /// <returns></returns>
        public static async Task Initialize(this CodingTaskContext context, IHashService hashService) {
            await context.Database.EnsureCreatedAsync();
            await initializeUsers(context, hashService);
            await initializeMatches(context);
        }

        // seeds some sample matches (each 10 seconds there will be a match)
        // todo: remove this in production
        private static async Task initializeMatches(CodingTaskContext context) {
            if (await context.Matches.AnyAsync()) {
                return;
            }

            var now = DateTime.UtcNow;
            for (var i = 0; i < 1000; i++) {
                await context.Matches.AddRangeAsync(new Match {
                    Id = Guid.NewGuid(),
                    ExpiresTimestamp = now.AddSeconds((i + 1) * 10).ToUnixTime()
                });
            }

            await context.SaveChangesAsync();
        }


        // seeds some sample users
        // todo: remove this in production
        private static async Task initializeUsers(CodingTaskContext context, IHashService hashService) {
            var currentUsers = await context.Users.ToListAsync();
            bool anyNewUser = false;

            if (currentUsers.All(u => u.UserName != "User1")) {
                await context.Users.AddAsync(new Models.User {
                    UserName = "User1",
                    Password = await hashService.HashText("Password1")
                });

                anyNewUser = true;
            }

            if (currentUsers.All(u => u.UserName != "User2")) {
                await context.Users.AddAsync(new Models.User {
                    UserName = "User2",
                    Password = await hashService.HashText("Password2")
                });

                anyNewUser = true;
            }

            if (currentUsers.All(u => u.UserName != "User3")) {
                await context.Users.AddAsync(new Models.User {
                    UserName = "User3",
                    Password = await hashService.HashText("Password3")
                });

                anyNewUser = true;
            }

            if (currentUsers.All(u => u.UserName != "User4")) {
                await context.Users.AddAsync(new Models.User {
                    UserName = "User4",
                    Password = await hashService.HashText("Password4")
                });

                anyNewUser = true;
            }

            if (anyNewUser) {
                await context.SaveChangesAsync();
            }
        }
    }
}