using CodeChallenge.DAL.Interfaces;
using CodeChallenge.Models.DAO;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.DAL
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly IDbContext _context;

        public MovieRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie_DTO>> GetMoviesAll()
        {
            return await _context.Movies
                .Select(c => new Movie_DTO()
                {
                    Uuid = c.Uuid,
                    Title = c.Title,
                    ReleaseDate = c.ReleaseDate,
                    Rating = c.Rating
                })
                .ToListAsync();
        }

        public async Task<Movie_DTO?> GetMovieById(Guid movieId)
        {
            return await _context.Movies
                .Where(c => c.Uuid == movieId)
                .Select(c => new Movie_DTO()
                {
                    Uuid = c.Uuid,
                    Title = c.Title,
                    ReleaseDate = c.ReleaseDate,
                    Rating = c.Rating
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie_DTO>> GetMoviesAllByDirectorName(string name)
        {
            return await _context.Movies.Where(x=> x.Director.Name.Contains(name))
               .Select(c => new Movie_DTO()
               {
                   Uuid = c.Uuid,
                   Title = c.Title,
                   ReleaseDate = c.ReleaseDate,
                   Rating = c.Rating
               })
               .ToListAsync();
        }

        public async Task<Movie_DTO?> CreateMovie(AddMovieViewModel addMovieViewModel)
        {
            var insertedMovie = new Movie
            {
                Uuid = Guid.NewGuid(),
                Title = addMovieViewModel.Title,
                DirectorUuid = addMovieViewModel.DirectorUuid,
                ReleaseDate = addMovieViewModel.ReleaseDate,
                Rating = addMovieViewModel.Rating
            };


            var newMovie = _context.Movies.AddAsync(insertedMovie).Result.Entity;
            await _context.SaveAsync();
            return new Movie_DTO
            {
                Uuid = newMovie.Uuid,
                Title = addMovieViewModel.Title,
                ReleaseDate = addMovieViewModel.ReleaseDate,
                Rating = addMovieViewModel.Rating
            };
        }

        public async Task<bool> UpdateMovie(Guid movieId, UpdateMovieViewModel updateMovieViewModel)
        {
            var entity = await _context.Movies.Where(c => c.Uuid == movieId).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Title = updateMovieViewModel.Title;
                entity.ReleaseDate = updateMovieViewModel.ReleaseDate;
                entity.Rating = updateMovieViewModel.Rating;

                _context.Movies.Update(entity);
                await _context.SaveAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteMovie(Guid movieId)
        {
            var entity = await _context.Movies.Where(c => c.Uuid == movieId).FirstOrDefaultAsync();

            if (entity != null)
            {
                _context.Movies.Remove(entity);
                await _context.SaveAsync();

                return true;
            }

            return false;
        }
    }
}
