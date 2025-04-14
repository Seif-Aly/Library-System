using System;
namespace Library
{
    /// <summary>
    /// Represents user of the library system.
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        public Role UserRole { get; set; }

    }
}

