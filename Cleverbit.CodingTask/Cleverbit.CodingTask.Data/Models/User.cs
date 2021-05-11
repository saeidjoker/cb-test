namespace Cleverbit.CodingTask.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        /// <summary>
        /// A unique user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// A hashed password
        /// </summary>
        public string Password { get; set; }
    }
}
