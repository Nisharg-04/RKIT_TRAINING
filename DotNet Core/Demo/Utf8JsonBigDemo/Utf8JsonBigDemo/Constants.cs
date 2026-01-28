using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utf8JsonBigDemo
{
    public static class Constants
    {
        public static string bigJson = """
                {
                  "requestId": "req-987654",
                  "timestamp": "2026-01-27T10:30:45Z",
                  "environment": "Production",
                  "service": "PaymentAPI",
                  "level": "Error",
                  "message": "Payment processing failed",
                  "user": {
                    "id": 12345,
                    "name": "Nisharg Patel",
                    "email": "nisharg@example.com",
                    "roles": ["Admin", "User"]
                  },
                  "payment": {
                    "paymentId": "pay_456",
                    "amount": 12999,
                    "currency": "INR",
                    "method": "CreditCard",
                    "cardDetails": {
                      "last4": "1234",
                      "network": "VISA",
                      "issuer": "HDFC"
                    }
                  },
                  "items": [
                    { "id": 1, "name": "Laptop", "price": 100000 },
                    { "id": 2, "name": "Mouse", "price": 999 },
                    { "id": 3, "name": "Keyboard", "price": 2000 }
                  ],
                  "stackTrace": "System.Exception: Payment failed at PaymentService.Process()...",
                  "debugInfo": {
                    "machine": "prod-server-12",
                    "os": "Linux",
                    "dotnetVersion": "8.0",
                    "cpu": "Intel Xeon",
                    "memory": "32GB"
                  }
                }
                """;

                    }
}
