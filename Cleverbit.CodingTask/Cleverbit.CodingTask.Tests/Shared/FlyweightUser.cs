namespace Cleverbit.CodingTask.Tests.Shared {

    /// <summary>
    /// An implementation of flyweight design pattern to share the user for test tasks
    /// </summary>
    public class FlyweightUser {

        public string UserName { get; private set; }
        public string Password { get; private set; }

        public void SetUser(string userName, string password) {
            UserName = userName;
            Password = password;
        }
    }
}
