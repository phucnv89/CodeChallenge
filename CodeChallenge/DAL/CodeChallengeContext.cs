using CodeChallenge.DAL.Interfaces;
using CodeChallenge.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.DAL
{
    public partial class CodeChallengeContext : DbContext, IDbContext
    {
        private readonly IConfiguration _configuration;
        public CodeChallengeContext(DbContextOptions<CodeChallengeContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<T> GetSet<T>() where T : class
        {
            return this.Set<T>();
        }

        public async Task SaveAsync()
        {
            try { await this.SaveChangesAsync(); }
            catch { throw; }
        }

        public void SetAsNoTracking()
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public virtual DbSet<Director> Directors { get; set; }

        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //   => optionsBuilder.UseSqlServer("integrated security=False;encrypt=False;connection timeout=30;data source=(local);Initial Catalog=CodeChallenge;User Id=sa;Password=123456789;");
    => optionsBuilder.UseSqlServer(_configuration["LocalDBCS"]);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>(entity =>
            {
                entity.HasKey(e => e.Uuid);

                entity.ToTable("Director");

                entity.Property(e => e.Uuid)
                    .ValueGeneratedNever()
                    .HasColumnName("uuid");
                entity.Property(e => e.Birthdate)
                    .HasColumnType("datetime")
                    .HasColumnName("birthdate");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Uuid);

                entity.ToTable("Movie");

                entity.Property(e => e.Uuid)
                    .ValueGeneratedNever()
                    .HasColumnName("uuid");
                entity.Property(e => e.DirectorUuid).HasColumnName("director_uuid");
                entity.Property(e => e.Rating).HasColumnName("rating");
                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("release_date");
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.HasOne<Director>(d => d.Director).WithMany(p => p.Movies)
                    .HasForeignKey(d => d.DirectorUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movie_Director");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
