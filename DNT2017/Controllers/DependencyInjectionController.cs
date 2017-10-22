using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DNT2017.Controllers
{
    public class DependencyInjectionController : Controller
    {
        // GET: DependencyInjection
        public IActionResult Index()
        {
            return View();
        }
    }
}