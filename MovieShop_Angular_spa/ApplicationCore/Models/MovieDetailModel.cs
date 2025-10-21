using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? OverView { get; set; }
        public string? Tagline { get; set; }

        public decimal? Revenue { get; set; }
        public decimal? Budget { get; set; }

        public string? ImdbUrl { get; set; }
        public string? TmdbUrl { get; set; }
        public string? PosterUrl { get; set; }
        public string? BackdropUrl { get; set; }
        public string? OriginalLanguage { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Runtime { get; set; }
        public decimal? Price { get; set; } = 9.9m;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public decimal? Rating { get; set; }
        public List<GenreModel> Genres { get; set; } // # navigation property

        public List<CastModel> Casts { get; set; }  // # navigation property
        public List<TrailerModel> Trailers { get; set; }  // # navigation property

    }
}
