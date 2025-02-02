namespace MySite.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string movie_img { get; set; }
        public DateTime release_date { get; set; }
        public int movie_time { get; set; }

        // Ініціалізація MovieGenres
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        // Ініціалізація MovieActors
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();

        // отримання списку акторів через MovieActors
        public List<Actor> Actors => MovieActors.Select(ma => ma.Actor).ToList();
    }
}
