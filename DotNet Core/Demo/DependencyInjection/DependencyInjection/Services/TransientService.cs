namespace DependencyInjection.Services
{
    public class TransientService : IInstanceService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
 
}
