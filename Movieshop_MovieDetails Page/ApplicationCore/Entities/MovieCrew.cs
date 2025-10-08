using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MovieCrew
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; } // navigation property
        public int CrewId { get; set; }
        public Crew Crew { get; set; } // navigation property
        [MaxLength(128)]
        public string Department { get; set; }
        [MaxLength(128)]
        public string Job { get; set; }


    }
}
