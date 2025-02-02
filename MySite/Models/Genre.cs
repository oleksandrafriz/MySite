namespace MySite.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string name { get; set; }

        // Колекція фільмів через проміжну таблицю
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
