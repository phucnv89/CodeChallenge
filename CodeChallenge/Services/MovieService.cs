using CodeChallenge.DAL.Interfaces;
using CodeChallenge.IServices;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;

namespace CodeChallenge.Services
{
    public class MovieService : IMovieService
    {
        private readonly ILogger<MovieService> _logger;
        private readonly IMovieRepository _movieRepository;

        public MovieService(ILogger<MovieService> logger, IMovieRepository movieRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }       

        public async Task<Movie_DTO?> GetMovieById(Guid movieId)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");

            return await _movieRepository.GetMovieById(movieId);
        }

        public async Task<IEnumerable<Movie_DTO>> GetMoviesAll()
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");

            return await _movieRepository.GetMoviesAll();
        }

        public async Task<IEnumerable<Movie_DTO>> GetMoviesAllByDirectorName(string name)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");

            return await _movieRepository.GetMoviesAllByDirectorName(name);
        }

        public async Task<Movie_DTO?> CreateMovie(AddMovieViewModel addMovieViewModel)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");

            return await _movieRepository.CreateMovie(addMovieViewModel);
        }

        public async Task<bool> UpdateMovie(Guid movieId, UpdateMovieViewModel updateMovieViewModel)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");
            return await _movieRepository.UpdateMovie(movieId, updateMovieViewModel);
        }

        public async Task<bool> DeleteMovie(Guid movieId)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");
            return await _movieRepository.DeleteMovie(movieId);
        }
    }
}
