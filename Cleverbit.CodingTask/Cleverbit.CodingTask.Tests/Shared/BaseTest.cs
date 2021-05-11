using Bogus;

namespace Cleverbit.CodingTask.Tests.Shared {
    public abstract class BaseTest : AppFactory {
        protected readonly Faker faker;

        public BaseTest() {
            faker = new Faker();
        }
    }
}
