using AB.BL.Abtract;
using AB.BL.Concrete;
using AB.DAL.GenericRepository.Abstract;
using AB.DAL.GenericRepository.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.CompilerServices;

namespace AB.MVC.Extensions
{
    public static class ABExtensions
    {
        public static  IServiceCollection AddAB_Services(this IServiceCollection services)
        {

            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped(typeof(IManager<>), typeof(ManagerBase<>));
            return services;
        }

        public static IServiceCollection Add_AB_AddCookie(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(p =>
               {

                   
                   p.Cookie.Name = "ABCompany";
                   p.LoginPath = "/Account/Login";
                   p.LogoutPath = "/Account/Logout";
                   p.AccessDeniedPath = "/Account/Yasak";
                   p.ExpireTimeSpan=TimeSpan.FromMinutes(10); // KAç Tadika Geçerli olacak
                   p.Cookie.SameSite = SameSiteMode.Strict;//Bizim tarayicimiz disinda kullanilamasin
                   p.SlidingExpiration = true;// herhangi bir hareket oldugunda ExpireTimeSpan kadar suresi uzatilir

               });

            return services;
        }
    }
}
