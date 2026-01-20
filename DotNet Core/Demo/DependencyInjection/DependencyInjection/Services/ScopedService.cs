namespace DependencyInjection.Services
{
    public class ScopedService : IInstanceService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }

}
