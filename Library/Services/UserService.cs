using System;
namespace Library
{
    public class UserService
    {
        private readonly List<User> users;

        private int userCounter = 1;

        public UserService()
        {
            users = new();
        }

        /// <summary>
        /// Show all users.
        /// </summary>
        public List<User> GetAllUsers()
        {
            return users;
        }

        /// <summary>
        /// Authenticate a user with <paramref name="name"/> and <paramref name="password"/>.
        /// </summary>
        /// <param name="name">User name.</param>
        /// <param name="password">User passowrd.</param>
        public User Login(string name, string password)
        {
            foreach (var user in users)
            {
                if (user.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="name">User name.</param>
        /// <param name="password">User Password.</param>
        /// <param name="role">User role.</param>
        public User Register(string name, string password, Role role)
        {
            var newUser = new User
            {
                Id = userCounter++,
                Name = name,
                Password = password,
                UserRole = role
            };
            users.Add(newUser);
            return newUser;
        }
    }
}

