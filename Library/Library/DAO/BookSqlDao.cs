using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Library.Models;

namespace Library.DAO
{
    public class BookSqlDao : IBookDao
    {
        private readonly string connectionString;

        public BookSqlDao(string connString)
        {
            connectionString = connString;
        }

        public Book GetBook(int id)
        {
            Book book = null;

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string selectBook = "SELECT * FROM books WHERE id = @id;";
                SqlCommand cmd = new SqlCommand(selectBook, conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    book = MapBookFromReader(reader);
                }
            }
            return book;
        }

        public IList<Book> GetAllBooks()
        {
            IList<Book> books = new List<Book>();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string selectAllBooks = "SELECT * FROM books;";
                SqlCommand cmd = new SqlCommand(selectAllBooks, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Book book = MapBookFromReader(reader);
                    books.Add(book);
                }
            }
            return books;
        }

        public Book CreateBook(Book book)
        {
            int newBookId;

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string addBook = "INSERT INTO books (title, author, publication_date) " +
                                 "OUTPUT INSERTED.id " +
                                 "VALUES (@title, @author, @publication_date);";
                SqlCommand cmd = new SqlCommand(addBook, conn);

                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@publication_date", book.PublicationDate);

                newBookId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return GetBook(newBookId);
        }

        public void UpdateBook(Book book)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string updateBook = "UPDATE books SET title = @title, author = @author, " + 
                                    "publication_date = @publication_date " +
                                    "WHERE id = @id;";
                SqlCommand cmd = new SqlCommand(updateBook, conn);

                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@publication_date", book.PublicationDate);
                cmd.Parameters.AddWithValue("@id", book.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBook(int id)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string deleteBook = "DELETE FROM books WHERE id = @id;";
                SqlCommand cmd = new SqlCommand(deleteBook, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        private Book MapBookFromReader(SqlDataReader reader)
        {
            Book book = new Book();
            book.Id = Convert.ToInt32(reader["id"]);
            book.Title = Convert.ToString(reader["title"]);
            book.Author = Convert.ToString(reader["author"]);
            book.PublicationDate = Convert.ToDateTime(reader["publication_date"]);

            return book;
        }
    }
}
