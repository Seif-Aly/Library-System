using System;
namespace Library
{
    public class LibraryService
    {
        private readonly List<Book> books;

        private int bookCounter = 1;

        public LibraryService()
        {
            books = new();
        }

        /// <summary>
        /// Show all books.
        /// </summary>
        public List<Book> GetAllBooks()
        {
            return books;
        }

        /// <summary>
        /// Returns a book by <paramref name="bookTitle"/>.
        /// </summary>
        public Book GetBookByTitle(string bookTitle)
        {
            foreach (var book in books)
            {
                if (book.Title == bookTitle)
                {
                    return book;
                }
            }
            return null;
        }

        /// <summary>
        /// Add a new book by <paramref name="title"/>, <paramref name="author"/>, and <paramref name="isbn"/>.
        /// </summary>
        public void AddBook(string title, string author, string isbn)
        {
            var newBook = new Book
            {
                Id = bookCounter++,
                Title = title,
                Author = author,
                ISBN = isbn,
                IsAvailable = true
            };
            books.Add(newBook);
            Console.WriteLine($"Book with title: '{title}' and ID {newBook.Id} added.");
        }

        /// <summary>
        /// Searches for the books that contain <paramref name="searchText"/> in title or author.
        /// </summary>
        /// <returns></returns>
        public List<Book> SearchBooks(string searchText)
        {
            searchText = searchText.ToLower();
            return books.Where(b =>
                b.Title.ToLower().Contains(searchText) ||
                b.Author.ToLower().Contains(searchText)).ToList();
        }
    }
}

