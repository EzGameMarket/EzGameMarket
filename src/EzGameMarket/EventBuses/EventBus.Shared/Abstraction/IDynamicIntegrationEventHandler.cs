using System.Threading.Tasks;

namespace EventBus.Shared.Abstraction
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}