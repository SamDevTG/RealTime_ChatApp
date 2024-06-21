using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealTime_ChatApp.Data;
using RealTime_ChatApp.Hubs;
using RealTime_ChatApp.Models;
using RealTime_ChatApp.Services;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Verificar se estamos em tempo de design ou tempo de execução
        var isDesignTime = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"));

        if (!isDesignTime)
        {
            // Em tempo de execução, inicializar o SQLitePCL
            SQLitePCL.Batteries.Init();
        }

        // Adicionar serviços ao contêiner
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (isDesignTime)
            {
                // Configuração para tempo de design
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            else
            {
                // Configuração para tempo de execução
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), 
                                  sqliteOptions => sqliteOptions.CommandTimeout(60));
            }
        });

        builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddSignalR();

        // Registering custom services
        builder.Services.AddScoped<PasswordHasherService>();
        builder.Services.AddScoped<AuthService>();

        var app = builder.Build();

        // Configurar o pipeline de solicitação HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
            endpoints.MapHub<ChatHub>("/chatHub");
            endpoints.MapControllers(); // Ensure API controllers are mapped
        });

        app.Run();
    }
}