using System;
using System.IO;
using System.Drawing;
using System.Linq;
using QRCoder;

class Program
{
    static void Main()
    {
        string[] studentIds = {
            "00-00000PU", "11-11111PU",
        };

        // Filter only 10-character IDs
        var validIds = studentIds.Where(id => id.Length == 10).ToList();

        string outputFolder = @"C:\QR_Students";
        Directory.CreateDirectory(outputFolder);

        Console.WriteLine($"Found {validIds.Count} valid student IDs out of {studentIds.Length}");

        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            foreach (string id in validIds)
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q);

                using (QRCode qrCode = new QRCode(qrCodeData))
                using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                {
                    string filePath = Path.Combine(outputFolder, $"{id}.png");
                    qrCodeImage.Save(filePath);
                    Console.WriteLine($"✅ Saved QR for {id}");
                }
            }
        }

        Console.WriteLine("✅ All valid QR codes generated successfully!");
    }
}
