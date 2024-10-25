using Models;

namespace PlatformService.Repository {
    public interface IPlatformRepository {
        bool SaveChanges();
        IEnumerable<PlatformModel> GetAllPlatforms();
        PlatformModel GetPlatformById(int id);
        void CreatePlatform(PlatformModel platformModel);

    }
}