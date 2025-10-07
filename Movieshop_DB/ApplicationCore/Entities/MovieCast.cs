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
        public int MovieId;
        // # foreign key
        public int CastId;
        // # foreign key
        [MaxLength(450)]
        public string Character;
        // actor's character in the movie
    }
}
