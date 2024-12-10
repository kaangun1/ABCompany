using AB.BL.Abtract;
using AB.DAL.GenericRepository.Abstract;
using AB.Entities.Models.Concrete;
using AB.MVC.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AB.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        private readonly INotyfService notyfService;

        public HomeController(ILogger<HomeController> logger,
            INotyfService notyfService)
        {
            _logger = logger;
           
            this.notyfService = notyfService;
        }

        public IActionResult Index()
        {
            //Unite unite = new Unite() { UniteName = "Adet" };
            //uniteRepository.Insert(unite);
            notyfService.Success("Islem Baþarili");
            notyfService.Information("Information");
            notyfService.Error("Error");
            notyfService.Warning("Warning");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
