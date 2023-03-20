using CodeChallenge.IServices;
using CodeChallenge.Models.DAO;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
        }

        [HttpGet()]
        public async Task<IActionResult> GetDirectorsAll()
        {
            var result = await _directorService.GetDirectorsAll();

            return Ok(result);
        }

        [HttpGet("{directorId}")]
        public async Task<IActionResult> GetDirectorById(Guid directorId)
        {
            var result = await _directorService.GetDirectorById(directorId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirector([FromBody] AddDirectorViewModel addDirectorViewModel)
        {
            var result = await _directorService.CreateDirector(addDirectorViewModel);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPatch("{directorId}")]
        public async Task<IActionResult> UpdateDirector([FromRoute]Guid directorId,[FromBody]UpdateDirectorViewModel updateDirectorViewModel)
        {
            var result = await _directorService.UpdateDirector(directorId, updateDirectorViewModel);

            return Ok(result);
        }

        [HttpDelete("{directorId}")]
        public async Task<IActionResult> DeleteDirector([FromRoute] Guid directorId)
        {
            var result = await _directorService.DeleteDirector(directorId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
