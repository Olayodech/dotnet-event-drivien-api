using Models;
using PlatformService.Repository;

namespace PlatformService.Data {
    public class PlatformRepositoryImpl : IPlatformRepository
    {
        private readonly AppDbContext _context;
        public PlatformRepositoryImpl(AppDbContext context) {
         _context = context;
        }
        public void CreatePlatform(PlatformModel platformModel)
        {
            ArgumentNullException.ThrowIfNull(platformModel);
            _context.PlatformModels.Add(platformModel);
        }

        public IEnumerable<PlatformModel> GetAllPlatforms()
        {
             return [.. _context.PlatformModels]; //same as _context.Platforms.ToList()]; //same as _context.Platforms.AsEnumerable();
        }

        public PlatformModel GetPlatformById(int id)
        {
            return _context.PlatformModels.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}