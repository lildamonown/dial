using Kursa4.BLL.Extantions;
using Kursa4.BLL.Initializers;
using Kursa4.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddBllServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var dbContext = services.GetService<ApplicationDbContext>();
        
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.ExecuteSqlRawAsync(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CarBrands')
            BEGIN
                CREATE TABLE CarBrands (
                    Id int IDENTITY(1,1) PRIMARY KEY,
                    Name nvarchar(max) NOT NULL
                )
            END");
        await dbContext.Database.ExecuteSqlRawAsync(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CarSeries')
            BEGIN
                CREATE TABLE CarSeries (
                    Id int IDENTITY(1,1) PRIMARY KEY,
                    Name nvarchar(max) NOT NULL,
                    CarBrandId int NOT NULL,
                    CONSTRAINT FK_CarSeries_CarBrands FOREIGN KEY (CarBrandId) REFERENCES CarBrands(Id)
                )
            END");
        await AppDbContextInitializers.InitializeAsync(dbContext, userManager, rolesManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}





if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
