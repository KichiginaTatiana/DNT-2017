using System.Threading.Tasks;
using DNT2017.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DNT2017.Controllers
{
    public class ContactController : Controller
    {
        private readonly EmailMessageSettings _options;

        public ContactController(IOptions<EmailMessageSettings> options)
        {
            _options = options.Value;
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(Message message)
        {
            if (!TryValidateModel(message))
                return Index();

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(message.Email, _options.Login));
            emailMessage.To.Add(new MailboxAddress("admin", _options.EmailReciever));
            emailMessage.Subject = message.Subject ?? string.Empty;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_options.SmtpServer, _options.Port, true);
                await client.AuthenticateAsync(_options.Login, _options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            } 

            return View("Index");
        }
    }
}