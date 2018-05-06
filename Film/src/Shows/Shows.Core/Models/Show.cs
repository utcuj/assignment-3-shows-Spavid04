using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shows.Core.Models
{
    public class Show
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; }
        public string Title { get; set; }
        public string Actors { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImdbId { get; set; }
        public int ImdbRating { get; set; }
        public bool Available { get; set; }

        [JsonIgnore] public virtual List<UserReview> UserReviews { get; set; }
        [JsonIgnore] public virtual List<UserShowHistory> UserShowHistory { get; set; }
        [JsonIgnore] public virtual List<UserInterest> UserInterests { get; set; }

        public override string ToString()
        {
            return $"{Title} ({ReleaseDate.Year}){(!Available ? " Unavailable" : "")}";
        }

        public string ToLongString()
        {
            string[] lines =
            {
                $"{Title} ({ReleaseDate.ToShortDateString()}), {Genre}",
                Description,
                Actors,
                $"IMDB: {ImdbRating} ({ImdbId})",
                (!Available ? " Unavailable" : "")
            };

            return String.Join(Environment.NewLine, lines);
        }
    }
}
