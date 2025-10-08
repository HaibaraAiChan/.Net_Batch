using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApplicationCore.Entities
{
    [Table("Crew")]
    public class Crew
    {
        public int Id { get; set; }
        // primary key
        public string Name { get; set; }
        [MaxLength(2084)]
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        [MaxLength(2084)]
        public string ProfilePath { get; set; }

        public ICollection<MovieCrew> MovieCrews { get; set; } // # navigation property
    }
}
