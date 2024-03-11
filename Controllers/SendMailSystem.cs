using EnterpriceWeb.Mailutils;
using EnterpriceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO.Compression;

namespace EnterpriceWeb.Controllers
{
    public class SendMailSystem
    {
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SendMailSystem(IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _emailSender = emailSender;
            _hostEnvironment = hostEnvironment;
        }
        public async void Sendgmail(User user)
        {
            var receiver = user.us_gmail;
            var subject = "please comment at the new article";
            var message = "You have 14 days to feedback for the new article";
            await _emailSender.SenderEmailAsync(receiver, subject, message);
        }
        private MemoryStream DownloadSingleFile(List<string> files)
        {
            // Create a memory stream to store the ZIP archive
            var memoryStream = new MemoryStream();

            // Create a zip archive
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    // Construct the path to the file
                    var filePath = Path.Combine(_hostEnvironment.WebRootPath, "image", "Article_File", file);

                    if (System.IO.File.Exists(filePath))
                    {
                        // Create a new entry in the zip archive
                        var entry = archive.CreateEntry(file);

                        // Open the file and copy its contents into the zip entry
                        using (var fileStream = new FileStream(filePath, FileMode.Open))
                        using (var entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                }
            }

            // Reset the memory stream position
            memoryStream.Position = 0;

            // Return the ZIP archive as a file
            return memoryStream;
        }
    }
}

