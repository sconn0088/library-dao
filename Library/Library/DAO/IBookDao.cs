using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.DAO
{
    public interface IBookDao
    {
        Book GetBook(int id);

        IList<Book> GetAllBooks();

        Book CreateBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(int id);
    }
}
