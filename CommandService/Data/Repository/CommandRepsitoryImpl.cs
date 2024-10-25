using CommandService.Models;
namespace CommandService.Data
{
    public class CommandRepository : ICommandRepository
    {

        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(PlatformModels platform)
        {
            ArgumentNullException.ThrowIfNull(platform);

            _context.PlatformModel.Add(platform);
        }

        public void CreatePlatformCommand(int platformId, CommandModel command)
        {
            if (platformId == null){
                throw new ArgumentNullException(nameof(platformId));
            }
            command.PlatformId = platformId;
            _context.CommandModels.Add(command);
        }

        public IEnumerable<PlatformModels> GetAllPlatforms()
        {
            try { 
                return [.. _context.PlatformModel];
            }catch(Exception ex) {
            
            Console.WriteLine($"Error retrieving platforms: {ex.Message}");

            return [];
            }
        }

        public CommandModel GetCommand(int platformId, int commandId)
        {
            return _context.CommandModels.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<CommandModel> GetCommandsForPlatform(int platformId)
        {
            return _context.CommandModels.Where(c => c.PlatformId == platformId).OrderBy(c => c.Platform.Name);
        }

        public bool PlatformExists(int id)
        {
            return _context.PlatformModel.Any(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}