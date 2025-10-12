using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MovieCast
    {
        public int MovieId { get; set; }
        // # foreign key
        public int CastId { get; set; }
        // # foreign key
        [MaxLength(450)]
        public string Character { get; set; }
        // actor's character in the movie
        public Movie Movie { get; set; } 
        // # navigation property
        public Cast Cast { get; set; }
        // # navigation property
    }
}
