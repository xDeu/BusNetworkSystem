using BusNetworkSystem.DAL.BusNetworkSystem.DAL;
using BusNetworkSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusNetworkSystem.Models.User;

namespace BusNetworkSystem.DAL
{
    public class BusStationDbInitializer : CreateDatabaseIfNotExists<BusStationDbContext>
    {
        protected override void Seed(BusStationDbContext context)
        {
            // Добавляем администратора
            context.Users.Add(new User
            {
                Name = "Администратор",
                Email = "admin@busstation.com",
                PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", // SHA256 для "password"
                RegistrationDate = DateTime.Now,
                Role = (int)UserRole.Admin,
                Balance = 10000
            });

            // Добавляем тестовые автобусы
            context.Buses.AddRange(new[]
            {
                new Bus { LicencePlate = "А123БВ77", Model = "ПАЗ-3205", Capacity = 35, YearOfManufacture = 2018 },
                new Bus { LicencePlate = "В456ГД77", Model = "МАЗ-256", Capacity = 50, YearOfManufacture = 2020 }
            });

            base.Seed(context);
        }
    }
}
