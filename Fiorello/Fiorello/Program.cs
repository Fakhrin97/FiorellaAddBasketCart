using Fiorello.DAL;
using Microsoft.EntityFrameworkCore;

namespace Fiorello
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc().AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.AddSession(opt=>opt.IdleTimeout = TimeSpan.FromSeconds(45));
            builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            var app = builder.Build();
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => app.MapControllerRoute(
                "default", "{controller=home}/{action=index}/{id?}"
                ));
            app.Run();
        }
    }
}