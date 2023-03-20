using CodeChallenge.Models.DAO;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;

namespace CodeChallenge.DAL.Interfaces
{
    public interface IDirectorRepository : IGenericRepository<Director>
    {
        Task<IEnumerable<Director_DTO>> GetDirectorsAll();
        Task<Director_DTO?> GetDirectorById(Guid directorId);
        Task<Director_DTO> CreateDirector(AddDirectorViewModel addDirectorViewModel);
        Task<bool> UpdateDirector(Guid directorId, UpdateDirectorViewModel updateDirectorViewModel);
        Task<bool> DeleteDirector(Guid directorId);
    }
}
