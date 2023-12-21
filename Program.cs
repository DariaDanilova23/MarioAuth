using MarioAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConection");

builder.Services.AddDbContext<ApplicationDbContext>(
    contex=>contex.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);//добавление DbContexts к Services


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<IdentityErrorDescriber>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCoreAdmin("Admin");//админка

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    
    
    );

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" }; //Определение ролей в системе
    foreach (var role in roles) //Выполнение для каждой определенной роли
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role)); //Добавление роли в систему
    }

}


using (var scope = app.Services.CreateScope())// Добавление администратора
{
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = configuration["AdminUser:Email"];
    string password = configuration["AdminUser:Password"];

    if (await userManager.FindByEmailAsync(email) == null) //Проверка на наличие пользовател в системе
    {
        var user = new IdentityUser();
        
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password); //Создание пользователя

        await userManager.AddToRoleAsync(user, "Admin"); //Добавление роли

    }

}
app.MapRazorPages();// для генерации страниц identity

app.MapDefaultControllerRoute();//для админки


app.Run();
