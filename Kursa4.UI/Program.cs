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
        await dbContext.Database.ExecuteSqlRawAsync(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PriceHistories')
            BEGIN
                CREATE TABLE PriceHistories (
                    Id int IDENTITY(1,1) PRIMARY KEY,
                    SubserviceId int NOT NULL,
                    OldPrice float NOT NULL,
                    NewPrice float NOT NULL,
                    Comment nvarchar(max) NOT NULL,
                    ChangedAt datetime2 NOT NULL,
                    MasterName nvarchar(max) NOT NULL,
                    MasterSurname nvarchar(max) NOT NULL,
                    CONSTRAINT FK_PriceHistories_Subservices FOREIGN KEY (SubserviceId) REFERENCES Subservices(Id) ON DELETE CASCADE
                )
            END");
        await dbContext.Database.ExecuteSqlRawAsync(@"
            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Reports') AND name = 'Comment')
            BEGIN
                ALTER TABLE Reports ADD Comment nvarchar(max) NOT NULL DEFAULT ''
            END");
        await AppDbContextInitializers.InitializeAsync(dbContext, userManager, rolesManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}





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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
