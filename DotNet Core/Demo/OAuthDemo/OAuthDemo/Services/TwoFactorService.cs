using OtpNet;
using QRCoder;
namespace OAuthDemo.Services
{
   

    public class TwoFactorService
    {
        public string GenerateSecret()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(key);
        }

        public byte[] GenerateQrCode(string email, string secret)
        {
            var uri = $"otpauth://totp/AuthFullDemo:{email}?secret={secret}&issuer=AuthFullDemo";

            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);

            var qr = new PngByteQRCode(data);
            return qr.GetGraphic(20);
        }

        public bool Validate(string secret, string code)
        {
            var totp = new Totp(Base32Encoding.ToBytes(secret));
            return totp.VerifyTotp(code, out _);
        }
    }


}
