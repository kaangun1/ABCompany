using AB.BL.Abtract;
using AB.BL.Concrete;
using AB.DAL.DbContexts;
using AB.DAL.GenericRepository.Abstract;
using AB.DAL.GenericRepository.Concrete;
using AB.MVC.Extensions;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using AB.Entities.Models.Concrete;
using AB.MVC.AutoMapperConfig;
namespace AB.MVC
{
    public class Program
    {
        //public static async Task<Thread> GetThread()
        //{
        //    Thread thread = new Thread(() =>
        //    {
        //        Thread.Sleep(1000);
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri("https://wwww.google.com");
               
        //        Console.WriteLine("Thread 1");
        //    });
        //    return thread;
        //}
        public static void Main(string[] args)
        {

           

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            string connectionstr = builder.Configuration.GetConnectionString("AB");
            builder.Services.AddDbContext<SqlDbContext>(p=>p.UseSqlServer(connectionstr));

            builder.Services.AddAB_Services(); // Service'lerin register edilmesi ile ilgili kisim
            builder.Services.Add_AB_AddCookie(); //Cookie Base Authentication icin
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 3;
                config.IsDismissable = true;
               
                config.Position = NotyfPosition.BottomRight;

            });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
           
            app.UseAuthorization(); // Kullanim Sirasi Onemli. Once Authorization gelmeli
            app.UseAuthentication(); // Authentication Gelmeli.

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
