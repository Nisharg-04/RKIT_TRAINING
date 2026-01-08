using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Services
{
    public interface IOrderService
    {
        Guid InstanceId { get; }
        string PlaceOrder();
    }

}
