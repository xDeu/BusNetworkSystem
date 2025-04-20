using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNetworkSystem.DAL
{
    using global::BusNetworkSystem.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using static global::BusNetworkSystem.Models.User;

    namespace BusNetworkSystem.DAL
    {
        public class BusStationDbContext : DbContext
        {
            // Конструктор с указанием имени подключения из App.config
            public BusStationDbContext() : base("name=BusStationDB")
            {
                // инициализатор (для начального заполнения)
                Database.SetInitializer(new BusStationDbInitializer());

                // повышение производительности
                Configuration.LazyLoadingEnabled = false;

                // более подробные ошибки
                Configuration.ValidateOnSaveEnabled = true;
            }

            // DbSet'ы для всех таблиц базы данных
            public DbSet<User> Users { get; set; }
            public DbSet<Bus> Buses { get; set; }
            public DbSet<Route> Routes { get; set; }
            public DbSet<Schedule> Schedules { get; set; }
            public DbSet<Ticket> Tickets { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                // Убираем соглашение о множественном числе таблиц
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

                // Запрещаем каскадное удаление
                modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

                // Конфигурация сущностей
                ConfigureUserEntity(modelBuilder);
                ConfigureBusEntity(modelBuilder);
                ConfigureRouteEntity(modelBuilder);
                ConfigureScheduleEntity(modelBuilder);
                ConfigureTicketEntity(modelBuilder);
            }

            private void ConfigureUserEntity(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>()
                    .Property(u => u.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                modelBuilder.Entity<User>()
                    .Property(u => u.Email)
                    .HasMaxLength(100)
                    .IsRequired();

                modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();
            }

            private void ConfigureBusEntity(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Bus>()
                    .Property(b => b.LicencePlate)
                    .HasMaxLength(50)
                    .IsRequired();

                modelBuilder.Entity<Bus>()
                    .Property(b => b.Model)
                    .HasMaxLength(50)
                    .IsRequired();
            }

            private void ConfigureRouteEntity(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Route>()
                    .Property(r => r.RouteName)
                    .HasMaxLength(255)
                    .IsRequired();

                modelBuilder.Entity<Route>()
                    .Property(r => r.Distance)
                    .HasPrecision(18, 2);
            }

            private void ConfigureScheduleEntity(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Schedule>()
                    .HasRequired(s => s.Route)
                    .WithMany(r => r.Schedules)
                    .HasForeignKey(s => s.RouteID)
                    .WillCascadeOnDelete(false);

                modelBuilder.Entity<Schedule>()
                    .HasRequired(s => s.Bus)
                    .WithMany(b => b.Schedules)
                    .HasForeignKey(s => s.BusID)
                    .WillCascadeOnDelete(false);
            }

            private void ConfigureTicketEntity(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Ticket>()
                    .Property(t => t.Price)
                    .HasPrecision(18, 2);

                modelBuilder.Entity<Ticket>()
                    .HasRequired(t => t.Schedule)
                    .WithMany(s => s.Tickets)
                    .HasForeignKey(t => t.ScheduleID)
                    .WillCascadeOnDelete(false);

                modelBuilder.Entity<Ticket>()
                    .HasRequired(t => t.User)
                    .WithMany(u => u.Tickets)
                    .HasForeignKey(t => t.UserID)
                    .WillCascadeOnDelete(false);
            }
        }
    }
}
