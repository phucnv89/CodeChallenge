using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;

namespace CodeChallenge.IServices
{
    public interface IMovieService
    {
        Task<Movie_DTO?> GetMovieById(Guid movieId);
        Task<IEnumerable<Movie_DTO>> GetMoviesAll();
        Task<IEnumerable<Movie_DTO>> GetMoviesAllByDirectorName(string name);
        Task<Movie_DTO?> CreateMovie(AddMovieViewModel addMovieViewModel);
        Task<bool> UpdateMovie(Guid movieId, UpdateMovieViewModel updateMovieViewModel);
        Task<bool> DeleteMovie(Guid movieId);
    }
}
