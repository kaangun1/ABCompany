using AB.BL.Abtract;
using AB.BL.Concrete;
using AB.Entities.Models.Concrete;
using AB.MVC.Models.VMS.ProductVM;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;


namespace AB.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager productManager;
        private readonly INotyfService notyfService;
        private readonly IManager<Unite> uniteManager;
        private readonly IManager<Category> categoryManager;

        public ProductController(IProductManager productManager 
                                ,INotyfService notyfService
                                ,IManager<Unite> uniteManager
                                ,IManager<Category> categoryManager)
        {
            this.productManager = productManager;
            this.notyfService = notyfService;
            this.uniteManager = uniteManager;
            this.categoryManager = categoryManager;
        }
        public IActionResult Index()
        {
            notyfService.Success("Islem Başarili");
            var products = productManager.GetAllInclude(null,p=>p.Unite,p=>p.Categories).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ProductInsertVM insertVM = new ProductInsertVM();
            insertVM.Units = uniteManager.GetAll();
            var categories = categoryManager.GetAll(); // Db'deki butun categorileri al
           
            //Her bir category icin benim modelimdeki ProductInCategory alt modelini olustur
            foreach (var cat in categories)
            {
                ProductInCategory category = new ProductInCategory()
                {
                    Id = cat.Id,
                    CategoryName = cat.CategoryName,
                    IsChecked = false
                };
                insertVM.Categories.Add(category);
            }
            return View(insertVM);

        }
        [HttpPost]
        public IActionResult Create(ProductInsertVM insertVM)
        {
            if (!ModelState.IsValid)
            {
                return View(insertVM);
            }


            var product = new Product()
            {
                Id = insertVM.Id,
                Amount = insertVM.Amount,
                Price = insertVM.Price,
                UniteId = insertVM.UniteId,
                ProductCode = insertVM.ProductCode,
                ProductName = insertVM.ProductName,
                CreateDate = DateTime.Now
            };

            int sonuc = 0;
            #region Eger yeni bir urun olusturup icerisine mevcut category'leri eklemek isterseniz Hata alirsiniz
            // Asagidaki kod hatali calisacaktir 

            //foreach (var item in insertVM.Categories.Where(p=>p.IsChecked=true))
            //{
            //    var cat = categoryManager.GetById(item.Id);
            //    product.Categories.Add(cat);
            //}
            //var sonuc = productManager.Insert(product);

            #endregion
            //Burada product kaydi yeni olusur. O yuzden yeni product kaydinini
            //elimizdeki categorilerle iliskilendirmemiz gerekir.
            try
            {
                sonuc = productManager.Insert(product);
            }
            catch (Exception ex)
            {

                notyfService.Error(ex.Message);
                insertVM.Units = uniteManager.GetAll();
                //var categories = categoryManager.GetAll(); // Db'deki butun categorileri al

                ////Her bir category icin benim modelimdeki ProductInCategory alt modelini olustur
                //foreach (var cat in categories)
                //{
                //    ProductInCategory category = new ProductInCategory()
                //    {
                //        Id = cat.Id,
                //        CategoryName = cat.CategoryName,
                //        IsChecked = false
                //    };
                //    insertVM.Categories.Add(category);
                //}
                return View(insertVM);
            }
          

            //foreach (var item in insertVM.Categories.Where(p => p.IsChecked == true))
            //{
            //    var cat = categoryManager.GetAllInclude(p=>p.Id==item.Id,p=>p.Products).FirstOrDefault();
            //    cat.Products.Add(product);
            //    categoryManager.Update(cat);
            //}
                

            //if (sonuc > 0)
            //{
            //    notyfService.Success(product.ProductName + " Başarili şekilde kaydedilmistir");
            //    return RedirectToAction("Index");
            //}
            return View(insertVM);

        }
        [HttpGet]
        public IActionResult Update(int hasan)
        {
            var products = productManager.GetById(hasan);
            var product = productManager.GetAllInclude(p=>p.Id==hasan,p=>p.Unite,p=>p.Categories).FirstOrDefault();

            ProductUpdateVM updateVM = new ProductUpdateVM()
            { 
                Id = product.Id,
                ProductName = product.ProductName,
                Amount = product.Amount,
                Price = product.Price,
                ProductCode = product.ProductCode,
                Description=product.Description
            };
            var categories = categoryManager.GetAllInclude(null,p=>p.Products).ToList(); // Db'deki butun categorileri al


            //Her bir category icin benim modelimdeki ProductInCategory alt modelini olustur
            foreach (var cat in categories)
            {
                ProductInCategory category = new ProductInCategory()
                {
                    Id = cat.Id,
                    CategoryName = cat.CategoryName,
                    IsChecked = cat.Products.Any(p => p.Id == product.Id)
                };
                //updateVM.Categories.Add(category);
            }

            //updateVM.Unites = uniteManager.GetAll().ToList();
            return View(updateVM);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateVM updateVM)
        {
            

            // TODO : Coka Cok iliskide update çakismasina bakilacak
            if (!ModelState.IsValid)
            {
                return View(updateVM);
            }
            // productManager.Update()
            #region N-N Oncesi Kodlar
            //decimal price = 0;
            //price = decimal.Parse(updateVM.Price.ToString());
            //var products = productManager.GetById(updateVM.Id);
            //products.ProductName=updateVM.ProductName;
            //products.Amount = (updateVM.Amount); //decimal.TryParse(updateVM.Amount.ToString(), products.Amount) ==true?updateVM.Amount: products.Amount;
            //products.Price=updateVM.Price;
            //products.ProductCode = updateVM.ProductCode;
            //products.Description=updateVM.Description; 
            #endregion
            #region N-N Sonrasi Kodlar 
            decimal price = 0;
            price = decimal.Parse(updateVM.Price.ToString());
            var product = productManager.GetAllInclude(p=>p.Id==updateVM.Id,p=>p.Categories).FirstOrDefault();
           
            product.ProductName = updateVM.ProductName;
            product.Amount = (updateVM.Amount); //decimal.TryParse(updateVM.Amount.ToString(), products.Amount) ==true?updateVM.Amount: products.Amount;
            product.Price = updateVM.Price;
            product.ProductCode = updateVM.ProductCode;
            product.Description = updateVM.Description;

            //Database'den butun category'leri cektik
            var categories = categoryManager.GetAllInclude(null,p=>p.Products).ToList();
            //foreach (var cat in categories)
            //{
            //    //product.Categories.Remove(cat);
            //    cat.Products.Remove(product);
            //    categoryManager.Update(cat);
            //}
            //productManager.Update(product);
            
            //categories = null;

            //foreach (var cat in updateVM.Categories.Where(p => p.IsChecked == true).ToList())
            //{
            //    var category = categoryManager.GetById(cat.Id);
            //    category.Products.Add(product);
            //    categoryManager.Update(category);
            //}
            
            //foreach (var cat  in categories)
            //{
            //   if (cat.Products.Any(p => p.Id == updateVM.Id) && updateVM.Categories.Any(p=>p.IsChecked==false && p.Id==cat.Id))
            //    {
            //        product.Categories.Remove(cat);
            //        //if (!product.Categories.Any(p => p.Id == cat.Id))
            //        //{
            //        //    product.Categories.Add(cat);
            //        //}
            //        //else
            //        //{
            //        //    product.Categories.Remove(cat);

            //        //}
            //    }
            //   //else if(cat.Products.Any(p => p.Id == updateVM.Id) && updateVM.Categories.Any(p => p.Id == cat.Id))
            //   // {

            //   // }
            //   //Databaseden gelen category icerisinde bu product yoksa ancak Ekrandan gelen ViewModel icerisinde varsa 
            //   //Bunu category nin product'larina ekle
            //    else if(!cat.Products.Any(p => p.Id == updateVM.Id) && updateVM.Categories.Any(p => p.IsChecked == true && p.Id == cat.Id))
            //    {
            //        product.Categories.Add(cat);
            //    }


            //}




            #endregion


            var result = productManager.Update(product);
           
            if (result > 0)
            {

                notyfService.Success("Islem Başarili",5);
                return RedirectToAction("Index");

            }
               
            else
            {

                ModelState.AddModelError("", "Update işlemi Gerçekleşmedi..");
                notyfService.Error("Update işlemi Gerçekleşmedi..");
                return View(updateVM);
            }
        }

        [HttpGet]
        public IActionResult Delete(int ayse)
        {

            var product = productManager.GetAllInclude(p => p.Id == ayse, p => p.Unite, p => p.Categories).FirstOrDefault();

            return View(product);
        }

        public IActionResult Insert()
        {
            var model = new ProductEkleVM();
            var categories = categoryManager.GetAll();
            model.Units = uniteManager.GetAll();
            foreach (var item in categories)
            {
                ProductCategory productCategory = new ProductCategory()
                {
                    Id = item.Id,
                    CategoryName = item.CategoryName,
                    IsCheck=false
                };
                model.Categories.Add(productCategory);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Insert(ProductEkleVM model)
        {

            if (!ModelState.IsValid) 
            {
                model.Units = uniteManager.GetAll();
                return View(model);
            }
            var product = new Product
            {
                Id = model.Id,
                ProductCode = model.ProductCode,
                ProductName = model.ProductName,
                Amount = model.Amount,
                Price = model.Price,
                Description = model.Description,
                UniteId = model.UniteId
            };
            productManager.Insert(product);
            foreach (var item in model.Categories.Where(p=>p.IsCheck==true))
            {
                var category = categoryManager.GetById(item.Id);
                category.Products.Add(product);
                categoryManager.Update(category);
            }
           
            
            return RedirectToAction("Index");

        }
    }
    
}
