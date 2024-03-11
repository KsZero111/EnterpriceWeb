using EnterpriceWeb.Mailutils;
using EnterpriceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
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
        public MemoryStream DownloadSingleFile(List<Article_file> files)
        {
            // Create a memory stream to store the ZIP archive
            var memoryStream = new MemoryStream();

            // Create a zip archive
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    // Construct the path to the file
                    var filePath = Path.Combine(_hostEnvironment.WebRootPath, "image", "Article_File", file.article_file_name);

                    if (System.IO.File.Exists(filePath))
                    {
                        // Create a new entry in the zip archive
                        var entry = archive.CreateEntry(file.article_file_name);

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
        public async Task<MemoryStream> DownloadProcessAsync(List<MemoryStream> memoryStreams)
        {
            var finalMemoryStream = new MemoryStream();

            using (var finalArchive = new ZipArchive(finalMemoryStream, ZipArchiveMode.Create, true))
            {
                for (int i = 0; i < memoryStreams.Count; i++)
                {
                    // Reset the position of the memory stream to the beginning
                    memoryStreams[i].Position = 0;

                    // Create a zip entry for each memory stream
                    var entry = finalArchive.CreateEntry($"article_{i}.zip");

                    // Copy the content of the memory stream into the zip entry
                    using (var entryStream = entry.Open())
                    {
                        await memoryStreams[i].CopyToAsync(entryStream);
                    }
                }
            }

            // Reset the final memory stream position
            finalMemoryStream.Position = 0;

            // Return the final ZIP archive as a file
            return finalMemoryStream;
        }
    }
}

