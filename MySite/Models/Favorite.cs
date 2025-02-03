using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySite.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }

        [ForeignKey("Movie")]
        public int movie_id { get; set; }

        public User User { get; set; }  // Навігаційна властивість
        public Movie Movie { get; set; } // Навігаційна властивість
    }
}
