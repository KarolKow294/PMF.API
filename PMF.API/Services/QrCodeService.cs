using PMF.API.Entities;
using QRCoder;
using System.Drawing;

namespace PMF.API.Services
{
    public class QrCodeService
    {
        public string GenerateQrCode(string data)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.L);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10);

            return ConvertBitmapToString(qrCodeImage);
        }

        private string ConvertBitmapToString(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public string GenerateQrCodeData(Part part)
        {
            var qrCodeData = $"localhost:3000/Orders/{part.OrderId}?part={part.Id}";
            return qrCodeData;
        }
    }
}
