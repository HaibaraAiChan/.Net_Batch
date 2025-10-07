using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorites>(ConfigureFa);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);

        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084).IsRequired(false);
            builder.Property(m => m.Title).HasMaxLength(256).IsRequired();
            builder.Property(m => m.OverView).HasMaxLength(4096).IsRequired(false);
            builder.Property(m => m.Tagline).HasMaxLength(512).IsRequired(false);
            builder.Property(m => m.Runtime);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 2)").HasDefaultValue(0.0m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 2)").HasDefaultValue(0.0m);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084).IsRequired(false);

            builder.Property(m => m.PosterUrl).HasMaxLength(2084).IsRequired(false);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084).IsRequired(false);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64).IsRequired(false);
            builder.Property(m => m.ReleaseDate).HasColumnType("datetime2(7)");
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m).IsRequired(false);
            
            
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getutcdate()");
            builder.Property(m => m.UpdatedDate).HasDefaultValueSql("getutcdate()");
            builder.Property(m => m.CreatedBy).IsRequired(false);
            builder.Property(m => m.UpdatedBy).IsRequired(false);
            builder.Ignore(m => m.Rating);


        }
        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
        }
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character });
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mc => new { mc.MovieId, mc.CrewId, mc.Department, mc.Job });

        }
        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)").IsRequired();

        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(128).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(128).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
            builder.Property(u => u.HashedPassword).HasMaxLength(1024).IsRequired();
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);
            builder.Property(u => u.TwoFactorEnabled);
            builder.Property(u => u.IsLocked);
            builder.Property(u => u.LockoutEndDate).HasColumnType("datetime2(7)");
            builder.Property(u => u.DateOfBirth).HasColumnType("datetime2(7)");
            builder.Property(u => u.LastLoginDateTime).HasColumnType("datetime2(7)");
            builder.Property(u => u.AccessFailedCount).HasDefaultValue(0);

        }

        private void ConfigureFa(EntityTypeBuilder<Favorites> builder)
        {
            builder.ToTable("Favorites");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.MovieId).IsRequired();
            builder.Property(u => u.UserId).IsRequired();
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PurchaseNumber).HasMaxLength(450).IsRequired();
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)");
            builder.Property(p => p.PurchaseDateTime).HasColumnType("datetime2(7)").HasDefaultValueSql("getutcdate()").IsRequired();


        }


        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);

        }
        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        }
    }
   }
