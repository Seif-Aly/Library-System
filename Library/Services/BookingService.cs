using System;
namespace Library
{
    public class BookingService
    {
        private readonly List<Borrow> transactions;

        private int transactionCounter = 1;

        private readonly LibraryService libraryService;

        public BookingService(LibraryService libraryService)
        {
            this.libraryService = libraryService;
            transactions = new List<Borrow>();
        }

        /// <summary>
        /// Borrow a book for given <paramref name="userId"/>.
        /// </summary>
        public List<Borrow> GetBorrowsByUser(int userId)
        {
            return transactions
                .Where(t => t.UserId == userId && !t.ReturnDate.HasValue)
                .ToList();
        }

        /// <summary>
        /// Borrow a book with <paramref name="booktitle"/> for a given <paramref name="user"/>
        /// </summary>
        public bool BorrowBook(User user, string booktitle)
        {
            var book = libraryService.GetBookByTitle(booktitle);
            if (book == null)
            {
                Console.WriteLine("!! Book does not exist.");
                return false;
            }
            if ((bool)!book.IsAvailable)
            {
                Console.WriteLine("!! Book is currently not available.");
                return false;
            }

            book.IsAvailable = false;
            var newTransaction = new Borrow
            {
                Id = transactionCounter++,
                BookTitle = booktitle,
                UserId = user.Id,
                BorrowDate = DateTime.Now
            };
            transactions.Add(newTransaction);

            Console.WriteLine($"Book with title: '{book.Title}' borrowed by {user.Name}.");
            return true;
        }

        /// <summary>
        /// Return a book with <paramref name="booktitle"/> for a given <paramref name="user"/>
        /// </summary>
        public bool ReturnBook(User user, string bookTitle)
        {
            var book = libraryService.GetBookByTitle(bookTitle);
            if (book == null)
            {
                Console.WriteLine("!! Book does not exist.");
                return false;
            }

            if (book.IsAvailable == true)
            {
                Console.WriteLine("!! Book is already available.");
                return false;
            }

            Borrow transaction = new();
            foreach (var tran in transactions)
            {
                if (tran.BookTitle == bookTitle && tran.UserId == user.Id && !tran.ReturnDate.HasValue)
                {
                    transaction = tran;
                }
            }

            if (transaction == null)
            {
                Console.WriteLine("!! No active borrow transaction found");
                return false;
            }

            transaction.ReturnDate = DateTime.Now;
            book.IsAvailable = true;

            Console.WriteLine($"Book with title: '{book.Title}' returned by {user.Name} on {transaction.ReturnDate.Value.ToShortDateString()}.");
            return true;
        }
    }
}

