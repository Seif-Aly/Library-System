using System;
namespace Library
{
    public class UnAuthMenu
    {
        public static bool ShowUnAuthMenu()
        {
            Console.WriteLine("\n1. Login");
            Console.WriteLine("2. Register as user");
            Console.WriteLine("3. Register as admin");
            Console.WriteLine("4. Exit");

            Console.Write("Please choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register(Role.User);
                    break;
                case "3":
                    Register(Role.Admin);
                    break;
                case "4":
                    Console.WriteLine("Exit");
                    return true;
                default:
                    Console.WriteLine("Incorrect option. Please try again.");
                    break;
            }
            return false;
        }

        private static void Login()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            User user = Program.userService.Login(name, password);

            if (user != null)
            {
                Program.loggedInUser = user;
                Console.WriteLine("Logged in successfully");
            }
            else
            {
                Console.WriteLine("Incorrect credentials. Please try again.");
            }
        }


        private static void Register(Role role)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            User user = Program.userService.Register(name, password, role);

            Console.WriteLine("Registration successful.");
        }
    }
}

