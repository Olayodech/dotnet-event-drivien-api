
namespace CommandService.Controller 
{
    using AutoMapper;
    using CommandService.Data;
    using CommandService.DTO;
    using CommandService.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    [Consumes("application/json")]
    public class CommandsController : ControllerBase {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepository commandRepository, IMapper mapper) {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        [HttpGet("get-commands")]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId) {
            Console.WriteLine($"-----> Getting Commands for platform {platformId} <-----");
            if (!_commandRepository.PlatformExists(platformId)) { 
                return NotFound($"Platform with Id: {platformId} not found.");
            }
            var commandItems = _commandRepository.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("get-command-by-id/{commandId}")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"-----> Getting Command by Id for platform {platformId} and command {commandId} <-----");
            if (!_commandRepository.PlatformExists(platformId))
            {
                return NotFound($"Platform with Id: {platformId} not found.");
            }
            var commandItem = _commandRepository.GetCommand(platformId, commandId);
            if (commandItem == null)
            {
                return NotFound($"Command with Id: {commandId} not found for platform {platformId}.");
            }
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        // [HttpPost("create-command")]
        // public async Task<ActionResult<CommandReadDto> CreateCommand(int PlatformID, CommandCreateDto commandCreateDto) {
        //     if (!_commandRepository.PlatformExists(PlatformID)) {
        //         return NotFound($"Platform with Id: {PlatformID} not found.");
        //     }
        //     var commandModel = _mapper.Map<CommandModel>(commandCreateDto);
        //     _commandRepository.CreatePlatformCommand(PlatformID, commandModel);
        //     _commandRepository.SaveChanges();
        //     // if (!_commandRepository.SaveChanges()) {
        //     //     throw new Exception($"Creating a command for platform {PlatformID} failed on save.");
        //     // }
        //     var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
        //     return CreatedAtRoute("GetCommandForPlatform", new { platformId = PlatformID, commandId = commandReadDto.Id }, commandReadDto);
        // }
    }
}