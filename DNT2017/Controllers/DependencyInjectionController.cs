using DNT2017.Services;
using Microsoft.AspNetCore.Mvc;

namespace DNT2017.Controllers
{
    public class DependencyInjectionController : Controller
    {
        public ITransientService TransientService { get; }
        public IScopedService ScopedService { get; }
        public ISingletonService SingletonService { get; }
        public ISingletomInstanceService SingletomInstanceService { get; }
        public IBigService BigService { get; }

        public DependencyInjectionController(ITransientService transientService, IScopedService scopedService,
            ISingletonService singletonService, ISingletomInstanceService singletomInstanceService, IBigService bigService)
        {
            TransientService = transientService;
            ScopedService = scopedService;
            SingletonService = singletonService;
            SingletomInstanceService = singletomInstanceService;
            BigService = bigService;
        }

        public IActionResult Index()
        {
            ViewBag.Transient = TransientService;
            ViewBag.Scoped = ScopedService;
            ViewBag.Singleton = SingletonService;
            ViewBag.SingletonInstance = SingletomInstanceService;
            
            ViewBag.Service = BigService;
            return View();
        }
    }
}