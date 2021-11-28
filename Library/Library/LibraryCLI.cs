using System;
using System.Collections.Generic;
using Library.DAO;
using Library.Models;

namespace Library
{
    public class LibraryCLI
    {
        private readonly IBookDao bookDao;

        public LibraryCLI(IBookDao bookDao)
        {
            this.bookDao = bookDao;
        }

        public void RunCLI()
        {
            DisplayBanner();
            bool running = true;
            while (running)
            {
                DisplayMenu();
                Console.WriteLine();
                Console.WriteLine("Please select an option");
                int selection = (int)int.Parse(Console.ReadLine());

                if (selection == 1)
                {
                    IList<Book> books = bookDao.GetAllBooks();
                    foreach (Book book in books)
                    {
                        Console.WriteLine(book.ToString());
                    }
                }
                else if (selection == 2)
                {
                    Console.WriteLine("Which book would you like?\n");
                    int bookSelection = (int)int.Parse(Console.ReadLine());
                    Console.WriteLine(bookDao.GetBook(bookSelection).ToString());
                }
                else if (selection == 3)
                {
                    AddNewBook();
                }
                else if (selection == 4)
                {
                    Console.WriteLine("Which book would you like to update?\n");
                    int bookSelection = (int)int.Parse(Console.ReadLine());
                    Book book = bookDao.GetBook(bookSelection);
                    UpdateBook(book);
                }
                else if (selection == 5)
                {
                    Console.WriteLine("Which book would you like to remove?");
                    int bookId = (int)int.Parse(Console.ReadLine());
                    bookDao.DeleteBook(bookId);
                    Console.WriteLine("\nBook was deleted :(\n");
                }
                else if (selection == 6)
                {
                    running = false;
                }
            }
        }

        private void AddNewBook()
        {
            Book newBook = PromptForNewBookData();
            newBook = bookDao.CreateBook(newBook);
            Console.WriteLine("Book added to the Library.\n");
        }

        private Book PromptForNewBookData()
        {
            Book book = new Book();

            Console.Write("Enter the title: ");
            string title = Console.ReadLine();
            book.Title = title;

            Console.Write("Enter the author: ");
            string author = Console.ReadLine();
            book.Author = author;

            Console.Write("Enter the publication date: ");
            DateTime pubDate = DateTime.Parse(Console.ReadLine());
            book.PublicationDate = pubDate;

            return book;
        }

        private void UpdateBook(Book bookToUpdate)
        {
            Console.WriteLine("Enter a new title (or press Enter to leave unchanged)");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTitle))
            {
                bookToUpdate.Title = newTitle;
            }
            Console.WriteLine("Enter a new author (or press Enter to leave unchanged)");
            string newAuthor = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAuthor))
            {
                bookToUpdate.Author = newAuthor;
            }
            Console.WriteLine("Enter a new publication date (or press Enter to leave unchanged)");
            string newPubDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPubDate))
            {
                bookToUpdate.PublicationDate = DateTime.Parse(newPubDate);
            }

            bookDao.UpdateBook(bookToUpdate);
            Console.WriteLine($"\nUpdate book {bookToUpdate}\n");
        }

        private void DisplayBanner()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("------|  Library Data Management  |------");
            Console.WriteLine("-----------------------------------------");
        }

        private void DisplayMenu()
        {
            Console.WriteLine("1. View Library books");
            Console.WriteLine("2. Get a book");
            Console.WriteLine("3. Add new book");
            Console.WriteLine("4. Update book information");
            Console.WriteLine("5. Remove book from Library");
            Console.WriteLine("6. Exit");
        }
    }
}