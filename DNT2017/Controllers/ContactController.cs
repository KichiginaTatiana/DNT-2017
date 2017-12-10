using DNT2017.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DNT2017.Controllers
{
    public class ContactController : Controller
    {
        private readonly IOptions<EmailMessageSettings> _optionsAccessor;

        public ContactController(IOptions<EmailMessageSettings> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Send(Message message)
        {
            return View("Index");
        }
    }
}