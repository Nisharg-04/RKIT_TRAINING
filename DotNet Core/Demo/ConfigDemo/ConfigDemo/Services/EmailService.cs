using ConfigDemo.Settings;
using Microsoft.Extensions.Options;

namespace ConfigDemo.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly FeatureSettings _features;

        public EmailService(
            IOptions<EmailSettings> emailOptions,
            IOptions<FeatureSettings> featureOptions)
        {
            _emailSettings = emailOptions.Value;
            _features = featureOptions.Value;
        }

        public string SendEmail()
        {
            if (!_features.EnableEmail)
                return "Email feature disabled";

            return $"Email sent via {_emailSettings.SmtpServer}";
        }
    }
}
