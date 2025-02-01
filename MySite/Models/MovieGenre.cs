namespace MySite.Models
{
    public class MovieGenre
    {
        public int Id { get; set; } // Окремий ключ

        public int movie_id { get; set; }
        public Movie Movie { get; set; }

        public int genre_id { get; set; }
        public Genre Genre { get; set; }
    }
}
