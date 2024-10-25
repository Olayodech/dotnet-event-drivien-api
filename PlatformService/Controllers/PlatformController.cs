using AsyncDataServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.DTOs;
using PlatformService.Repository;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class PlatformController : ControllerBase {

        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandHttpClient;
        private readonly IMessageClient _messageClient;

        public PlatformController(IPlatformRepository platformRepository, IMapper mapper,
                 ICommandDataClient icommandDataClient, IMessageClient messageClient) {
            _platformRepository = platformRepository;
            _mapper = mapper;
            _commandHttpClient = icommandDataClient;
            _messageClient = messageClient;
        }
    

        [HttpGet("get_platforms")]
        public async Task<ActionResult<IEnumerable<PlaformReadDto>>> GetPlatforms() {
            Console.WriteLine("<--- GetPlatforms called --->");
            var platformItems = _platformRepository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlaformReadDto>>(platformItems));
        }

        [HttpGet("get_platform/{id}")]
        public async Task<ActionResult<PlaformReadDto>> GetPlatformById(int id) { 
            var platformItem =  _platformRepository.GetPlatformById(id);
            if (platformItem != null) {
                return Ok(_mapper.Map<PlaformReadDto>(platformItem));
            }
            Console.WriteLine($"<--- GetPlatformById called for id: {id} --->");
            return NotFound();
        }

        [HttpPost("create_platform")]
        public async Task<ActionResult<PlaformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto) { 
            var mappedRequest = _mapper.Map<Models.PlatformModel>(platformCreateDto);
            _platformRepository.CreatePlatform(mappedRequest);
            _platformRepository.SaveChanges();

            var createdResponse = _mapper.Map<PlaformReadDto>(mappedRequest);
            // testing command synchronously
            try{
                Console.WriteLine($"<--- Sending platform to command: {createdResponse.Name} --->");
                await _commandHttpClient.SendPlatformToCommand(createdResponse);
            } catch (Exception ex) { 
                Console.WriteLine($"<--- Error sending platform to command: {ex.Message} --->");
            }

            // publishing to RabbitMQ asynchronous
            try {
                var publishDto = _mapper.Map<PlatformPublishDto>(createdResponse);
                _messageClient.publishNewPlatform(publishDto);
            }catch (Exception ex) {
                Console.WriteLine($"<--- Error publishing platform to RabbitMQ: {ex.Message} --->");
            }

            return CreatedAtAction(nameof(CreatePlatform), new { id = createdResponse.Id }, createdResponse);
        }
    }
