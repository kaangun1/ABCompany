using AB.BL.Abtract;
using AB.Entities.Models.Concrete;
using AB.MVC.Models.VMS.AccountVM;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AB.MVC.AutoMapperConfig;

namespace AB.MVC.Controllers
{
    public class AccountController: Controller
    {
        private readonly IManager<User> userManager;
        private readonly IManager<Role> roleManager;
        private readonly INotyfService notyfService;
        private readonly IMapper mapper;

        public AccountController(IManager<User> userManager,
                                    IManager<Role> roleManager, 
                                    INotyfService notyfService,
                                    IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.notyfService = notyfService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {

            LoginVM loginVM = new LoginVM();
            return View(loginVM);
        }
        


            [HttpPost]
            public async Task<IActionResult> Login(LoginVM loginVM)
            {
                if (!ModelState.IsValid)
                    return View(loginVM);

                // Find the user with the email and password
                var user = userManager
                                .GetAllInclude(p => p.Email == loginVM.Email && 
                                    p.Password == loginVM.Password, 
                                    p => p.Roller)
                                .FirstOrDefault();

                if (user == null)
                {
                    notyfService.Error("Kullanıcı adı veya şifre hatalı");
                    return View(loginVM);
                }

            // Prepare user claims
            //TODO : Roller Tablosu Gelmiyor
            //var role = user.Roller.FirstOrDefault().Role.RoleName;
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.Ad),
                new Claim(ClaimTypes.Email, loginVM.Email),
                new Claim(ClaimTypes.GivenName, user.Ad + ' ' + user.Soyad),
                new Claim(ClaimTypes.Gender, user.Cinsiyet==true? "Erkek" : "Kadın"),
                new Claim(ClaimTypes.Role, "admin")
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties()
                {
                    IsPersistent = loginVM.RememberMe
                };

                var userClaimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign the user in
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaimsPrincipal, authProperties);

                return RedirectToAction("Index", "Home"); // Redirect to home after login
            }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var user =userManager.GetAllInclude(p=>p.Ad==User.Identity.Name,p=>p.Roller).FirstOrDefault();
            EditVM vm = mapper.Map<EditVM>(user);//Gelen User'i EditVM classina donustur
            EditVM vm2 = TipDonusturucu.Donustur<User, EditVM>(user);
            vm.RePassword = user.Password;
            //EditVM vm = new EditVM()
            //{
            //    Id= user.Id,
            //    Ad=user.Ad,
            //    Soyad=user.Soyad,
            //    Password=user.Password,
            //    RePassword=user.Password,
            //    Email=user.Email,
            //    Gsm=user.Gsm
            //};
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Details(EditVM editVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }

            var user = userManager.Get(p=>p.Id==editVM.Id);
            var user2 = mapper.Map<User>(editVM);
            user.Ad= editVM.Ad;
            user.Soyad= editVM.Soyad;
            user.Password=editVM.Password;
            user.Email=editVM.Email;
            user.Gsm=editVM.Gsm;

            userManager.Update(user);
            return RedirectToAction("Index","Home");
        }
     }
    }

