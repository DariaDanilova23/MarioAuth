using MarioAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConection");

builder.Services.AddDbContext<ApplicationDbContext>(
    contex=>contex.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);//���������� DbContexts � Services

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCoreAdmin();//�������

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
    var roles = new[] { "Admin", "User" }; //����������� ����� � �������
    foreach (var role in roles) //���������� ��� ������ ������������ ����
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role)); //���������� ���� � �������
    }

}
/*
using (var scope = app.Services.CreateScope())// ���������� ��������������
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = "admin@admin.ru"; // ������� ���������� �����
    string password = "Mariosevadmin49";
    if (await userManager.FindByEmailAsync(email) == null) //�������� �� ������� ����������� � �������
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password); //�������� ������������

        await userManager.AddToRoleAsync(user, "Admin"); //���������� ����

    }

}*/
app.MapRazorPages();// ��� ��������� ������� identity

app.MapDefaultControllerRoute();//��� �������


app.Run();
