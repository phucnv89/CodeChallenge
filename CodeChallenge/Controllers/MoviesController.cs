using CodeChallenge.IServices;
using CodeChallenge.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }


        [HttpGet()]
        public async Task<IActionResult> GetMoviesAll()
        {
            var result = await _movieService.GetMoviesAll();

            return Ok(result);
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieById(Guid movieId)
        {
            var result = await _movieService.GetMovieById(movieId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("directors/{directorName}")]
        public async Task<IActionResult> GetMovieByDirectorName(string directorName)
        {
            var result = await _movieService.GetMoviesAllByDirectorName(directorName);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] AddMovieViewModel addMovieViewModel)
        {
            var result = await _movieService.CreateMovie(addMovieViewModel);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPatch("{movieId}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] Guid movieId, [FromBody] UpdateMovieViewModel updateMovieViewModel)
        {
            var result = await _movieService.UpdateMovie(movieId, updateMovieViewModel);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] Guid movieId)
        {
            var result = await _movieService.DeleteMovie(movieId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}