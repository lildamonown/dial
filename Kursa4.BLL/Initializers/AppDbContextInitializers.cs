using Kursa4.BLL.Models;
using Kursa4.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.Initializers
{
    public class AppDbContextInitializers
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));

                User admin = new()
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    Name = "Владислав",
                    Surname = "Бердычевец",
                    PhoneNumber = "+375444921364"
                };

                await userManager.CreateAsync(admin, "Admin1234_");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (await roleManager.FindByNameAsync("Client") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Client"));

                User client = new()
                {
                    Email = "client@gmail.com",
                    UserName = "client@gmail.com",
                    Name = "Иван",
                    Surname = "Грозный",
                    PhoneNumber = "+375299836367"
                };

                await userManager.CreateAsync(client, "Client1234_");
                await userManager.AddToRoleAsync(client, "Client");

                User client3 = new()
                {
                    Email = "client3@gmail.com",
                    UserName = "client3@gmail.com",
                    Name = "Евстав",
                    Surname = "Степановский",
                    PhoneNumber = "+375294578126"
                };

                await userManager.CreateAsync(client3, "Client1234_");
                await userManager.AddToRoleAsync(client3, "Client");

                User client4 = new()
                {
                    Email = "client4@gmail.com",
                    UserName = "client4@gmail.com",
                    Name = "Август",
                    Surname = "Март",
                    PhoneNumber = "+375295612478"
                };

                await userManager.CreateAsync(client4, "Client1234_");
                await userManager.AddToRoleAsync(client4, "Client");

                User client5 = new()
                {
                    Email = "client5@gmail.com",
                    UserName = "client5@gmail.com",
                    Name = "Митя",
                    Surname = "Уи-уи-уи",
                    PhoneNumber = "+375299514785"
                };

                await userManager.CreateAsync(client5, "Client1234_");
                await userManager.AddToRoleAsync(client5, "Client");
            }

            if (await roleManager.FindByNameAsync("Master") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Master"));

                User master = new()
                {
                    Email = "master@gmail.com",
                    UserName = "master@gmail.com",
                    Name = "Кирилл",
                    Surname = "Пупкин",
                    PhoneNumber = "+375987463725"
                };

                await userManager.CreateAsync(master, "Master1234_");
                await userManager.AddToRoleAsync(master, "Master");
            }

            if (await roleManager.FindByNameAsync("Economist") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Economist"));

                User economist = new()
                {
                    Email = "economist@gmail.com",
                    UserName = "economist@gmail.com",
                    Name = "Екатерина",
                    Surname = "Пупкова",
                    PhoneNumber = "+375856290874"
                };

                await userManager.CreateAsync(economist, "Economist1234_");
                await userManager.AddToRoleAsync(economist, "Economist");
            }

            if (await roleManager.FindByNameAsync("Owner") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Owner"));

                User owner = new()
                {
                    Email = "owner@gmail.com",
                    UserName = "owner@gmail.com",
                    Name = "Виктор",
                    Surname = "Шибеко",
                    PhoneNumber = "+375019835647"
                };

                await userManager.CreateAsync(owner, "Owner1234_");
                await userManager.AddToRoleAsync(owner, "Owner");
            }

            if (!context.Reviews.Any())
            {
                var rev1 = new Review()
                {
                    UserId = (await userManager.Users.FirstAsync()).Id,
                    Text = "Отличное СТО, вежливые и добрые работники. Моя машина была безнадежна, но благодаря работникам данного СТО она восресла из мертвых.",
                    Grade = 5,
                    CreateAt = DateTime.Now
                };

                var rev2 = new Review()
                {
                    UserId = (await userManager.Users.LastAsync()).Id,
                    Text = "Электрик ноль диагностировать и найти причины поломки отправляет в другие сервисы такие как нтс фольксваген и тд. Увы пришлось столкнуться с этим сервисом. Выброшенные деньги. Никому не советую. По этому там всегда и пусто",
                    Grade = 1
                };

                var rev3 = new Review()
                {
                    UserId = (await userManager.Users.FirstAsync()).Id,
                    Text = "Ремонтировали машину около месяца назад, понравилось, сделали хорошо, было ещё пару вопросов по двигателю, провели диагностику, на всё ответили. Цены очень лояльные, заезжали до этого на другой сервис, загнули цену выше.",
                    Grade = 4
                };

                await context.Reviews.AddRangeAsync(new List<Review> { rev1, rev2, rev3 });
                await context.SaveChangesAsync();
            }

            // Services and Subservices are seeded via SQL script directly in DB

            if (!context.Statuses.Any())
            {
                var s1 = new Status()
                {
                    Name = EStatus.Processing.GetValue(),
                };

                var s2 = new Status()
                {
                    Name = EStatus.PendingClient.GetValue(),
                };

                var s3 = new Status()
                {
                    Name = EStatus.InWork.GetValue(),
                };

                var s4 = new Status()
                {
                    Name = EStatus.PendingPay.GetValue(),
                };

                var s5 = new Status()
                {
                    Name = EStatus.Completed.GetValue(),
                };

                var s6 = new Status()
                {
                    Name = EStatus.Cancelled.GetValue(),
                };

                await context.Statuses.AddRangeAsync(new List<Status> { s1, s2, s3, s4, s5, s6 });
                await context.SaveChangesAsync();
            }

            if (!context.CarBrands.Any())
            {
                var brands = new List<CarBrand>
                {
                    new CarBrand { Name = "Toyota" },
                    new CarBrand { Name = "BMW" },
                    new CarBrand { Name = "Mercedes-Benz" },
                    new CarBrand { Name = "Audi" },
                    new CarBrand { Name = "Volkswagen" },
                    new CarBrand { Name = "Honda" },
                    new CarBrand { Name = "Ford" },
                    new CarBrand { Name = "Hyundai" },
                    new CarBrand { Name = "Kia" },
                    new CarBrand { Name = "Renault" },
                    new CarBrand { Name = "Nissan" },
                    new CarBrand { Name = "Mazda" },
                    new CarBrand { Name = "Skoda" },
                    new CarBrand { Name = "Volvo" },
                    new CarBrand { Name = "Lexus" },
                    new CarBrand { Name = "Chevrolet" },
                    new CarBrand { Name = "Opel" },
                    new CarBrand { Name = "Peugeot" },
                    new CarBrand { Name = "Citroen" },
                    new CarBrand { Name = "Mitsubishi" },
                    new CarBrand { Name = "Lada" },
                    new CarBrand { Name = "ВАЗ" },
                    new CarBrand { Name = "ГАЗ" },
                    new CarBrand { Name = "УАЗ" },
                };

                await context.CarBrands.AddRangeAsync(brands);
                await context.SaveChangesAsync();
            }

            if (!context.CarSeries.Any())
            {
                var toyota = await context.CarBrands.FirstAsync(b => b.Name == "Toyota");
                var bmw = await context.CarBrands.FirstAsync(b => b.Name == "BMW");
                var mercedes = await context.CarBrands.FirstAsync(b => b.Name == "Mercedes-Benz");
                var audi = await context.CarBrands.FirstAsync(b => b.Name == "Audi");
                var vw = await context.CarBrands.FirstAsync(b => b.Name == "Volkswagen");
                var honda = await context.CarBrands.FirstAsync(b => b.Name == "Honda");
                var ford = await context.CarBrands.FirstAsync(b => b.Name == "Ford");
                var hyundai = await context.CarBrands.FirstAsync(b => b.Name == "Hyundai");
                var kia = await context.CarBrands.FirstAsync(b => b.Name == "Kia");
                var renault = await context.CarBrands.FirstAsync(b => b.Name == "Renault");
                var nissan = await context.CarBrands.FirstAsync(b => b.Name == "Nissan");
                var mazda = await context.CarBrands.FirstAsync(b => b.Name == "Mazda");
                var skoda = await context.CarBrands.FirstAsync(b => b.Name == "Skoda");
                var volvo = await context.CarBrands.FirstAsync(b => b.Name == "Volvo");
                var lexus = await context.CarBrands.FirstAsync(b => b.Name == "Lexus");
                var chevrolet = await context.CarBrands.FirstAsync(b => b.Name == "Chevrolet");
                var lada = await context.CarBrands.FirstAsync(b => b.Name == "Lada");

                var seriesList = new List<CarSeries>
                {
                    new CarSeries { Name = "Camry", CarBrandId = toyota.Id },
                    new CarSeries { Name = "Corolla", CarBrandId = toyota.Id },
                    new CarSeries { Name = "RAV4", CarBrandId = toyota.Id },
                    new CarSeries { Name = "Land Cruiser", CarBrandId = toyota.Id },
                    new CarSeries { Name = "Yaris", CarBrandId = toyota.Id },
                    new CarSeries { Name = "5 Series", CarBrandId = bmw.Id },
                    new CarSeries { Name = "3 Series", CarBrandId = bmw.Id },
                    new CarSeries { Name = "X5", CarBrandId = bmw.Id },
                    new CarSeries { Name = "X7", CarBrandId = bmw.Id },
                    new CarSeries { Name = "7 Series", CarBrandId = bmw.Id },
                    new CarSeries { Name = "E-Class", CarBrandId = mercedes.Id },
                    new CarSeries { Name = "C-Class", CarBrandId = mercedes.Id },
                    new CarSeries { Name = "S-Class", CarBrandId = mercedes.Id },
                    new CarSeries { Name = "GLE", CarBrandId = mercedes.Id },
                    new CarSeries { Name = "A4", CarBrandId = audi.Id },
                    new CarSeries { Name = "A6", CarBrandId = audi.Id },
                    new CarSeries { Name = "Q5", CarBrandId = audi.Id },
                    new CarSeries { Name = "Q7", CarBrandId = audi.Id },
                    new CarSeries { Name = "Passat", CarBrandId = vw.Id },
                    new CarSeries { Name = "Golf", CarBrandId = vw.Id },
                    new CarSeries { Name = "Tiguan", CarBrandId = vw.Id },
                    new CarSeries { Name = "Polo", CarBrandId = vw.Id },
                    new CarSeries { Name = "Civic", CarBrandId = honda.Id },
                    new CarSeries { Name = "Accord", CarBrandId = honda.Id },
                    new CarSeries { Name = "CR-V", CarBrandId = honda.Id },
                    new CarSeries { Name = "Focus", CarBrandId = ford.Id },
                    new CarSeries { Name = "Kuga", CarBrandId = ford.Id },
                    new CarSeries { Name = "Mondeo", CarBrandId = ford.Id },
                    new CarSeries { Name = "Solaris", CarBrandId = hyundai.Id },
                    new CarSeries { Name = "Elantra", CarBrandId = hyundai.Id },
                    new CarSeries { Name = "Tucson", CarBrandId = hyundai.Id },
                    new CarSeries { Name = "Rio", CarBrandId = kia.Id },
                    new CarSeries { Name = "Sportage", CarBrandId = kia.Id },
                    new CarSeries { Name = "Ceed", CarBrandId = kia.Id },
                    new CarSeries { Name = "Logan", CarBrandId = renault.Id },
                    new CarSeries { Name = "Duster", CarBrandId = renault.Id },
                    new CarSeries { Name = "Sandero", CarBrandId = renault.Id },
                    new CarSeries { Name = "Qashqai", CarBrandId = nissan.Id },
                    new CarSeries { Name = "X-Trail", CarBrandId = nissan.Id },
                    new CarSeries { Name = "Almera", CarBrandId = nissan.Id },
                    new CarSeries { Name = "3", CarBrandId = mazda.Id },
                    new CarSeries { Name = "6", CarBrandId = mazda.Id },
                    new CarSeries { Name = "CX-5", CarBrandId = mazda.Id },
                    new CarSeries { Name = "Octavia", CarBrandId = skoda.Id },
                    new CarSeries { Name = "Rapid", CarBrandId = skoda.Id },
                    new CarSeries { Name = "Kodiaq", CarBrandId = skoda.Id },
                    new CarSeries { Name = "XC60", CarBrandId = volvo.Id },
                    new CarSeries { Name = "XC90", CarBrandId = volvo.Id },
                    new CarSeries { Name = "S60", CarBrandId = volvo.Id },
                    new CarSeries { Name = "RX", CarBrandId = lexus.Id },
                    new CarSeries { Name = "NX", CarBrandId = lexus.Id },
                    new CarSeries { Name = "LX", CarBrandId = lexus.Id },
                    new CarSeries { Name = "Lacetti", CarBrandId = chevrolet.Id },
                    new CarSeries { Name = "Cruze", CarBrandId = chevrolet.Id },
                    new CarSeries { Name = "Aveo", CarBrandId = chevrolet.Id },
                    new CarSeries { Name = "Granta", CarBrandId = lada.Id },
                    new CarSeries { Name = "Vesta", CarBrandId = lada.Id },
                    new CarSeries { Name = "Niva", CarBrandId = lada.Id },
                };

                await context.CarSeries.AddRangeAsync(seriesList);
                await context.SaveChangesAsync();
            }
        }
    }
}
