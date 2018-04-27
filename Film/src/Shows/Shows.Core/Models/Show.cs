using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int ImdbRating { get; set; }

        public virtual List<ShowEvent> Events { get; set; }
    }
}
