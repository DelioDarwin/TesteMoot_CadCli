using Microsoft.EntityFrameworkCore;
using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.DataAccess.Repository;
using TesteMoot.DataAcess.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add Framework MVC Services to the container.
        builder.Services.AddMvc();


        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


        var app = builder.Build();
        //Adding MVC Middleware
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
        app.Run();
    }
}
