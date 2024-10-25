using PlatformService.DTOs;
namespace AsyncDataServices {
    public interface IMessageClient {
        void publishNewPlatform(PlatformPublishDto platformPublishedDto);
    }
}