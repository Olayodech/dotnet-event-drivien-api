using System.Text;
using System.Text.Json;
using PlatformService.DTOs;

namespace PlatformService.SyncDataServices.Http {
    public class CommandDataClientImpl: ICommandDataClient { 

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public CommandDataClientImpl(HttpClient httpClient, IConfiguration config) {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task SendPlatformToCommand(PlaformReadDto platformReadDto) {
            var httpClient = new StringContent(
                JsonSerializer.Serialize(platformReadDto), Encoding.UTF8, "application/json"
            );

            var response = await _httpClient.PostAsync($"{_config["CommandService"]}", httpClient);

            if (response.IsSuccessStatusCode) { 
                Console.WriteLine($"---> Platform sent to command: {platformReadDto.Name}");
            } else {
                Console.WriteLine($"---> Failed to send platform to command: {platformReadDto.Name}");
            }
        }
    }
}