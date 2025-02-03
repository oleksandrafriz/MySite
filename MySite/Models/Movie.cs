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

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();

        public List<Actor> Actors => MovieActors.Select(ma => ma.Actor).ToList();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
