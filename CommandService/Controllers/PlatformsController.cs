namespace CommandService.Controller {
    using AutoMapper;
    using CommandService.Data;
    using CommandService.DTO;
    using Microsoft.AspNetCore.Mvc;
    [ApiController]
    [Route("api/c/[controller]")]
    // [Consumes("application/json")]
    // [Produces("application/json")]
    public class PlatformsController: ControllerBase {

        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;
        public PlatformsController(ICommandRepository commandRepository, IMapper mapper) {  
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        [HttpPost("test-command")]
        public async Task<ActionResult> TestInboundConnection() {
            Console.WriteLine("-----> INBOUD POST COMMAND INBOUND CONNECTION TEST <-----");
            return Ok("Inbound connection test successful.");
        }

        [HttpGet("get-platforms")]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms() {  
            Console.WriteLine("-----> Getting All platforms <-----");
            var platformItems =  _commandRepository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

    }
}