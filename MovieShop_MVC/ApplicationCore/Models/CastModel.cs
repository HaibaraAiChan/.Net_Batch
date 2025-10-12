using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CastModel
    {
        public int Id { get; set; }
        // primary key
        public string Name { get; set; }
        public string? ProfilePath { get; set; }
        // # foreign key
        [MaxLength(450)]
        public string Character;
        // actor's character in the movie
    }
}
