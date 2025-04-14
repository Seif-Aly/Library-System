using System;
namespace Library
{
    /// <summary>
    /// Represents a borrowing in the system.
    /// </summary>
    public class Borrow
    {
        public int Id { get; set; }

        public string? BookTitle { get; set; }

        public int UserId { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}

