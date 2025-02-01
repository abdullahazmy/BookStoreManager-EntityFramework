namespace BookStoreManager.Models
{
    internal class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } // make it required
        public string Author { get; set; } // make it required
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
