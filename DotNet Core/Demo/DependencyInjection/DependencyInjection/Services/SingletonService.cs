namespace DependencyInjection.Services
{
    public class SingletonService : IInstanceService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }

}
