using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MovieGenre
    {
        // composite key (MovieId, GenreId)
        public int MovieId { get; set; } // # foreign key
        public int GenreId { get; set; } // # foreign key
        public Movie Movie { get; set; } // # navigation property
        public Genre Genre { get; set; } // # navigation property
    }
}
