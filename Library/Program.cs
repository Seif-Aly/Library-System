using System;
namespace Library
{
    public class Program
    {
        internal static LibraryService? libraryService;
        internal static BookingService? bookingService;
        internal static UserService? userService;
        internal static User? loggedInUser;

        public static void Main()
        {
            libraryService = new();
            bookingService = new(libraryService);
            userService = new();

            Console.WriteLine("Library Booking System");

            bool exit = false;
            while (!exit)
            {
                if (loggedInUser == null)
                {
                    exit = UnAuthMenu.ShowUnAuthMenu();
                }
                else
                {
                    exit = AuthMenu.ShowAuthMenu();
                }
            }
        }
    }
}

