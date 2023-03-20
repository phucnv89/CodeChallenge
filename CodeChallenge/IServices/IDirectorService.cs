using CodeChallenge.Models.DAO;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;

namespace CodeChallenge.IServices
{
    public interface IDirectorService
    {
        Task<Director_DTO?> GetDirectorById(Guid directorById);
        Task<IEnumerable<Director_DTO>> GetDirectorsAll();
        Task<Director_DTO?> CreateDirector(AddDirectorViewModel addDirectorViewMode);
        Task<bool> UpdateDirector(Guid directorId, UpdateDirectorViewModel updateDirectorViewModel);
        Task<bool> DeleteDirector(Guid directorId);
    }
}
