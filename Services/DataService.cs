using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Cryptography;
using BusNetworkSystem.Models;
using static BusNetworkSystem.Models.User;
using BusNetworkSystem.DAL.BusNetworkSystem.DAL;

namespace BusNetworkSystem.Services
{
    public interface IDataService
    {

    }
    internal class DataService : IDataService
    {
        private readonly Func<BusStationDbContext> _contextFactory;

        public DataService(Func<BusStationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region User Methods
        public User GetUserById(int userId)
        {
            using (var context = _contextFactory())
            {
                return context.Users.Find(userId) ?? new User();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var context = _contextFactory())
            {
                return context.Users.FirstOrDefault(u => u.Email == email) ?? new User();
            }
        }

        public bool RegisterUser(User newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Email) ||
                string.IsNullOrWhiteSpace(newUser.PasswordHash))
            {
                return false;
            }

            using (var context = _contextFactory())
            {
                if (context.Users.Any(u => u.Email == newUser.Email))
                {
                    return false;
                }

                newUser.PasswordHash = ComputeSha256Hash(newUser.PasswordHash);
                newUser.RegistrationDate = DateTime.Now;
                newUser.Role = (int)UserRole.User;

                context.Users.Add(newUser);
                context.SaveChanges();
                return true;
            }
        }

        public bool UpdateUser(User user)
        {
            using (var context = _contextFactory())
            {
                var existingUser = context.Users.Find(user.UserID);
                if (existingUser == null) return false;

                context.Entry(existingUser).CurrentValues.SetValues(user);
                return context.SaveChanges() > 0;
            }
        }
        #endregion

        #region Bus Methods
        public IEnumerable<Bus> GetAllBuses()
        {
            using (var context = _contextFactory())
            {
                return context.Buses.ToList();
            }
        }

        public Bus GetBusById(int busId)
        {
            using (var context = _contextFactory())
            {
                return context.Buses.Find(busId) ?? new Bus();
            }
        }
        #endregion

        #region Route Methods
        public IEnumerable<Route> GetAllRoutes()
        {
            using (var context = _contextFactory())
            {
                return context.Routes.ToList();
            }
        }

        public Route GetRouteById(int routeId)
        {
            using (var context = _contextFactory())
            {
                return context.Routes.Find(routeId) ?? new Route();
            }
        }
        #endregion

        #region Schedule Methods
        public IEnumerable<Schedule> GetSchedules(DateTime? date = null)
        {
            using (var context = _contextFactory())
            {
                var query = context.Schedules
                    .Include(s => s.Route)
                    .Include(s => s.Bus)
                    .AsQueryable();

                if (date.HasValue)
                {
                    query = query.Where(s => DbFunctions.TruncateTime(s.DepartureTime) == date.Value.Date);
                }

                return query.OrderBy(s => s.DepartureTime).ToList();
            }
        }

        public Schedule GetScheduleById(int scheduleId)
        {
            using (var context = _contextFactory())
            {
                return context.Schedules
                    .Include(s => s.Route)
                    .Include(s => s.Bus)
                    .FirstOrDefault(s => s.ScheduleID == scheduleId) ?? new Schedule();
            }
        }

        public bool UpdateAvailableSeats(int scheduleId, int seatsChange)
        {
            using (var context = _contextFactory())
            {
                var schedule = context.Schedules.Find(scheduleId);
                if (schedule == null) return false;

                schedule.AvailibleSeats += seatsChange;
                return context.SaveChanges() > 0;
            }
        }
        #endregion

        #region Ticket Methods
        public bool BookTicket(Ticket ticket)
        {
            using (var context = _contextFactory())
            {
                var schedule = context.Schedules.Find(ticket.ScheduleID);
                if (schedule == null || schedule.AvailibleSeats <= 0)
                    return false;

                // Проверка, что место не занято
                if (context.Tickets.Any(t => t.ScheduleID == ticket.ScheduleID &&
                                           t.SeatNumber == ticket.SeatNumber))
                {
                    return false;
                }

                ticket.PurchaseDate = DateTime.Now;
                schedule.AvailibleSeats--;

                context.Tickets.Add(ticket);
                return context.SaveChanges() > 0;
            }
        }

        public IEnumerable<Ticket> GetUserTickets(int userId)
        {
            using (var context = _contextFactory())
            {
                return context.Tickets
                    .Include(t => t.Schedule)
                    .Include(t => t.Schedule.Route)
                    .Include(t => t.Schedule.Bus)
                    .Where(t => t.UserID == userId)
                    .OrderByDescending(t => t.PurchaseDate)
                    .ToList();
            }
        }

        public bool CancelTicket(int ticketId)
        {
            using (var context = _contextFactory())
            {
                var ticket = context.Tickets.Find(ticketId);
                if (ticket == null) return false;

                var schedule = context.Schedules.Find(ticket.ScheduleID);
                if (schedule != null)
                {
                    schedule.AvailibleSeats++;
                }

                context.Tickets.Remove(ticket);
                return context.SaveChanges() > 0;
            }
        }
        #endregion

        #region Helper Methods
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion
    }
}
