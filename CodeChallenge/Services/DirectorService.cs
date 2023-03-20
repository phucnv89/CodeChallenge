using CodeChallenge.DAL.Interfaces;
using CodeChallenge.IServices;
using CodeChallenge.Models.DAO;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;

namespace CodeChallenge.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly ILogger<DirectorService> _logger;
        private readonly IDirectorRepository _directorRepository;

        public DirectorService(ILogger<DirectorService> logger, IDirectorRepository directorRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _directorRepository = directorRepository ?? throw new ArgumentNullException(nameof(directorRepository));
        }

        public async Task<Director_DTO?> GetDirectorById(Guid directorById)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");

            return await _directorRepository.GetDirectorById(directorById);
        }

        public async Task<IEnumerable<Director_DTO>> GetDirectorsAll()
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");

            return await _directorRepository.GetDirectorsAll();
        }

        public async Task<Director_DTO?> CreateDirector(AddDirectorViewModel addDirectorViewMode)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");

            return await _directorRepository.CreateDirector(addDirectorViewMode);
        }

        public async Task<bool> UpdateDirector(Guid directorId, UpdateDirectorViewModel updateDirectorViewModel)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");
            return await _directorRepository.UpdateDirector(directorId, updateDirectorViewModel);
        }

        public async Task<bool> DeleteDirector(Guid directorId)
        {
            var source = this.GetType().Name;
            _logger.LogInformation($"Starting {source}");
            return await _directorRepository.DeleteDirector(directorId);
        }
    }
}
