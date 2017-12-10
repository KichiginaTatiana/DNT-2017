using Microsoft.AspNetCore.Mvc;

namespace DNT2017.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Send()
        {
            return View("Index");
        }
    }
}