using System;
using System.IO;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureSendgridDemo
{
    class Program
    {
        static void Main(string[] args)
         {
            Console.WriteLine("Hello World!");

            Execute().Wait();
        }

        static async Task Execute()
        {
            ///Store sendgrid key insie environement variable and try accessing it
            
            var apiKey = Environment.GetEnvironmentVariable("sendgridkey");
            
            var client = new SendGridClient(apiKey);
            // Create messgae body
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("SachinTest@example.com", "DX Team"),
                Subject = "Hello World from the SendGrid CSharp SDK!",
                PlainTextContent = "Hello, Email!",
                HtmlContent = "<strong>Hello, Email!</strong>"
            };
            msg.AddTo(new EmailAddress("cloud.hero@domaintext.com", "Cloud Hero"));

            using (var fileStream = File.OpenRead("C:\\temp\\test.txt"))
            {
                var a = fileStream.Name.Split('\\');
                var b = a[a.Length - 1];
                await msg.AddAttachmentAsync(b, fileStream);
            
            }

            /// use this to send message.
            var response = await client.SendEmailAsync(msg);

            

        }
    }
}
