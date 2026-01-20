using ConfigDemo.Settings;
using Microsoft.Extensions.Options;

namespace ConfigDemo.Services
{
    public class PaymentService
    {
        private readonly FeatureSettings _features;

        public PaymentService(IOptions<FeatureSettings> features)
        {
            _features = features.Value;
        }

        public string ProcessPayment()
        {
            if (!_features.EnablePayments)
                return "Payments disabled";

            return "Payment processed";
        }
    }
}
