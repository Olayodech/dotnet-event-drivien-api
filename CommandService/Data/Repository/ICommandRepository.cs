using CommandService.Models;
namespace CommandService.Data
{
    public interface ICommandRepository {
        bool SaveChanges();
        IEnumerable<PlatformModels> GetAllPlatforms();
        void CreatePlatform(PlatformModels platform);
        bool PlatformExists(int id);

        IEnumerable<CommandModel> GetCommandsForPlatform(int platformId);
        CommandModel GetCommand(int id, int commandId);
        void CreatePlatformCommand(int platformId, CommandModel command);
    }
}