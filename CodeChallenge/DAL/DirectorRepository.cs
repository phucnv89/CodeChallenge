using CodeChallenge.DAL.Interfaces;
using CodeChallenge.Models.DAO;
using CodeChallenge.Models.DTO;
using CodeChallenge.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.DAL
{
    public class DirectorRepository : GenericRepository<Director>, IDirectorRepository
    {
        private readonly IDbContext _context;

        public DirectorRepository(IDbContext context) : base(context)
        {
            _context = context;
        }   

        public async Task<Director_DTO?> GetDirectorById(Guid directorId)
        {
            return await _context.Directors
                .Where(c => c.Uuid == directorId)
                .Select(c => new  Director_DTO()
                {
                    Uuid = c.Uuid,
                    Name = c.Name,
                    Birthdate = c.Birthdate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Director_DTO>> GetDirectorsAll()
        {
            return await _context.Directors
               .Select(c => new Director_DTO()
               {
                  Uuid = c.Uuid,
                  Name = c.Name,
                  Birthdate = c.Birthdate
               }).ToListAsync();
        }

        public async Task<Director_DTO?> CreateDirector(AddDirectorViewModel addDirectorViewModel)
        {
            var insertedDirector = new Director
            {
                Uuid = Guid.NewGuid(),
                Name = addDirectorViewModel.Name,
                Birthdate = addDirectorViewModel.Birthdate
            };

            
            var newDirector =  _context.Directors.AddAsync(insertedDirector).Result.Entity;
            await _context.SaveAsync();
            return new Director_DTO
            {
                Uuid = newDirector.Uuid,
                Name = newDirector.Name,
                Birthdate = newDirector.Birthdate
            };
        }

        public async Task<bool> UpdateDirector(Guid directorId, UpdateDirectorViewModel updateDirectorViewModel)
        {
            var entity = await _context.Directors.Where(c => c.Uuid == directorId).FirstOrDefaultAsync();

            if(entity != null)
            {
                entity.Name = updateDirectorViewModel.Name;
                entity.Birthdate = updateDirectorViewModel.Birthdate;

                _context.Directors.Update(entity);
                await _context.SaveAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteDirector(Guid directorId)
        {
            var entity = await _context.Directors.Where(c => c.Uuid == directorId).FirstOrDefaultAsync();

            if (entity != null)
            {
                _context.Directors.Remove(entity);
                 await _context.SaveAsync();

                return true;
            }

            return false;
        }
    }
}
