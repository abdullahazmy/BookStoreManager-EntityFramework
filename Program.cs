using BookStoreManager.Models;

namespace BookStoreManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Welcome to the BookStoreManagementSystem");

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();

                while (true)
                {
                    Console.WriteLine("\nSelect an option:");
                    Console.WriteLine("1. Add a new book");
                    Console.WriteLine("2. View all books");
                    Console.WriteLine("3. Update a book");
                    Console.WriteLine("4. Delete a book");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter choice: ");

                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddBook(context);
                            break;
                        case "2":
                            ViewBooks(context);
                            break;
                        case "3":
                            UpdateBook(context);
                            break;
                        case "4":
                            DeleteBook(context);
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
            }
        }

        static void AddBook(AppDbContext context)
        {
            Console.Write("Enter book title: ");
            string title = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter publish date (yyyy-MM-dd): ");
            DateTime publishDate = DateTime.Parse(Console.ReadLine());

            var book = new Book { Title = title, Author = author, Price = price, PublishDate = publishDate };
            context.Books.Add(book);
            context.SaveChanges();
            Console.WriteLine("Book added successfully!");
        }

        static void ViewBooks(AppDbContext context)
        {
            var books = context.Books.ToList();
            Console.WriteLine("\nBooks in Store:");
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Price: {book.Price}, Published: {book.PublishDate.ToShortDateString()}");
            }
        }

        static void UpdateBook(AppDbContext context)
        {
            Console.Write("Enter book ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int bookId))
            {
                var book = context.Books.Find(bookId);
                if (book == null)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }
                Console.Write("Enter new title (leave blank to keep current): ");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrEmpty(newTitle)) book.Title = newTitle;

                Console.Write("Enter new author (leave blank to keep current): ");
                string newAuthor = Console.ReadLine();
                if (!string.IsNullOrEmpty(newAuthor)) book.Author = newAuthor;

                Console.Write("Enter new price (leave blank to keep current): ");
                string newPrice = Console.ReadLine();
                if (decimal.TryParse(newPrice, out decimal price)) book.Price = price;

                context.SaveChanges();
                Console.WriteLine("Book updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void DeleteBook(AppDbContext context)
        {
            Console.Write("Enter book ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int bookId))
            {
                var book = context.Books.Find(bookId);
                if (book == null)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }

                context.Books.Remove(book);
                context.SaveChanges();
                Console.WriteLine("Book deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }
    }
}
