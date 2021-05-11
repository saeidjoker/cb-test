using System;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.Services;

namespace Cleverbit.CodingTask.Application.ServiceImplementations {

    public class RandomScoreGenerator : IScoreGenerator {
        private readonly Random random;

        public RandomScoreGenerator() {
            random = new Random(123);
        }

        public Task<uint> GenerateScoreForUser(int userId) {
            return Task.FromResult((uint) random.Next(100));
        }
    }

}