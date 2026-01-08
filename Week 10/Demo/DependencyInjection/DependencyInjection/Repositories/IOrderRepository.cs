using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Repositories
{
    public interface IOrderRepository
    {
        Guid InstanceId { get; }
        string GetOrder();
    }

}
