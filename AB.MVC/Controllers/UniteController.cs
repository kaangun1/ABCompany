using AB.BL.Abtract;
using AB.Entities.Models.Concrete;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AB.MVC.Controllers
{

   
    public class UniteController(IManager<Unite> uniteManager,INotyfService notyfService) : Controller
    {
        public IActionResult Index()
        {
            var result = uniteManager.GetAll();
            return View(result);
        }
        [HttpGet]
        [Authorize()]
        public IActionResult Create()
        {
            var unite = new Unite();
            return View(unite);
        }

        [HttpPost]
        [Authorize()]
        public IActionResult Create(Unite unite)
        {
            if (!ModelState.IsValid)
            {
                return View(unite);
            }
            unite.CreateDate = DateTime.Now;
            var sonuc = uniteManager.Insert(unite);
            if(sonuc>0)
            {
                notyfService.Success(unite.UniteName + " başarili şekilde eklenmistir.");
                return RedirectToAction("Index");

            }
            return View(unite);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var unite = uniteManager.GetById(id);
            return View(unite);
        }
        [HttpPost]
        public IActionResult Update(Unite unite)
        {
            if (!ModelState.IsValid)
            {
                return View(unite);
            }
            else
            {
                var sonuc = uniteManager.Update(unite);
                if (sonuc > 0)
                {
                    notyfService.Success("Islem Başarili");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Islem Başarizi. Lütfen Daha sonra tekrar deneyiniz");
                    return View(unite);
                }
            }
          

        }

    }
}
