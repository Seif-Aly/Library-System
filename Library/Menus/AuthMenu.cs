using System;
using System.Net;

namespace Library
{
    public class AuthMenu
    {
        public static bool ShowAuthMenu()
        {
            Console.WriteLine($"\n---MENU---");
            Console.WriteLine("1. View All Books");
            Console.WriteLine("2. View Active Borrows");
            Console.WriteLine("3. Search Books");
            Console.WriteLine("4. Borrow Book");
            Console.WriteLine("5. Return Book");

            if (Program.loggedInUser.UserRole == Role.Admin)
            {
                Console.WriteLine("6. Add Book (Admin only)");
            }
            Console.WriteLine("\n7. Logout");
            Console.WriteLine("8. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewAllBooks();
                    break;
                case "2":
                    ViewActiveBorrows();
                    break;
                case "3":
                    SearchBooks();
                    break;
                case "4":
                    BorrowBook();
                    break;
                case "5":
                    ReturnBook();
                    break;
                case "6":
                    if (Program.loggedInUser.UserRole == Role.Admin)
                    {
                        AddBook();
                    }
                    else
                    {
                        Console.WriteLine("!! You do not have permission to do this.");
                    }
                    break;
                case "7":
                    Program.loggedInUser = null;
                    Console.WriteLine("You have been logged out.");
                    break;
                case "8":
                    Console.WriteLine("Exit");
                    return true;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            return false;
        }

        private static void ViewAllBooks()
        {
            List<Book> books = Program.libraryService.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("No books currently in the library.");
                return;
            }

            Console.WriteLine("Books in Library:");
            foreach (var book in books)
            {
                Console.WriteLine($"\nID: {book.Id}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Available: {book.IsAvailable}");
            }
        }

        private static void ViewActiveBorrows()
        {
            List<Borrow> borrows = Program.bookingService.GetBorrowsByUser(Program.loggedInUser.Id);
            if (borrows.Count == 0)
            {
                Console.WriteLine("You have no active borrows.");
                return;
            }

            Console.WriteLine("Active Borrows:");
            foreach (var borrow in borrows)
            {
                Book book = Program.libraryService.GetBookByTitle(borrow.BookTitle);
                Console.WriteLine($"\nID: {borrow.Id}, Title: {book.Title}, Borrowed On: {borrow.BorrowDate}");
            }
        }

        private static void SearchBooks()
        {
            Console.Write("Enter search term (title or author): ");
            string term = Console.ReadLine();
            var results = Program.libraryService.SearchBooks(term);
            if (results.Count == 0)
            {
                Console.WriteLine("No books matching your search.");
            }
            else
            {
                Console.WriteLine("Search Results:");
                foreach (var book in results)
                {
                    Console.WriteLine($"\nID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Available: {book.IsAvailable}");
                }
            }
        }

        private static void BorrowBook()
        {
            Console.Write("Enter Book Name to borrow: ");
            string input = Console.ReadLine();
            Program.bookingService.BorrowBook(Program.loggedInUser, input);
        }

        private static void ReturnBook()
        {
            Console.Write("Enter Book Title to return: ");
            string input = Console.ReadLine();
            Program.bookingService.ReturnBook(Program.loggedInUser, input);
        }

        private static void AddBook()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine();

            Program.libraryService.AddBook(title, author, isbn);
        }
    }
}

