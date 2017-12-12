using CafeteriaBooker.Context;
using CafeteriaBooker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CafeteriaBooker.DTO
{
    public class CafeteriaDTO:ICafeteriaDTO
    {
        private readonly DatabaseContext db = new DatabaseContext();

        public async Task<List<Table>> GetTablesMeetingReq(int minimumSeats, bool isSmokingAllowed)
        {
            return await db.Tables.Where(x => x.Smoke == isSmokingAllowed && x.Seats >= minimumSeats).ToListAsync();
        }

        public async Task<Table> GetTableById(int tableId)
        {
            return await db.Tables.FirstOrDefaultAsync(x => x.ID == tableId);
        }

        public Task<List<Table>> GetAllFreeTables(List<Table> tables, DateTime reservationDateTime)
        {
            return Task<List<Table>>.Run(() =>
            {
                return tables.Where(x => x.Reservations != null && !x.Reservations.Any(y => y.Date > (reservationDateTime.AddHours(-1)) &&
                (y.Date < (reservationDateTime.AddHours(1))))).ToList();
            });
        }

        public Task<Table> GetFirstFreeTable(List<Table> tables, DateTime reservationDateTime)
        {
            return Task<List<Table>>.Run(() =>
            {
                return tables.FirstOrDefault(x => x.Reservations != null && !x.Reservations.Any(y => y.Date > (reservationDateTime.AddHours(-1)) &&
            (y.Date < (reservationDateTime.AddHours(1)))
            ));
            });
        }

        public async Task<Reservation> GetReservationById(int reservationId)
        {
            return await db.Reservations.FirstOrDefaultAsync(x => x.ID == reservationId);
        }

        public async Task<List<Reservation>> GetReservationsByTableID(int tableId)
        {
            return await db.Reservations.Where(x => x.TableID == tableId && x.Date >= DateTime.Now).ToListAsync();
        }
        public Task MakeReservation(Table table, DateTime reservationDateTime, String name)
        {
            if (reservationDateTime < DateTime.Now)
                throw new System.ArgumentException("Time Traveling doesn't exists");
            return Task<Reservation>.Run(() =>
            {
                if (table.Reservations.Any(y => y.Date > reservationDateTime.AddHours(-1) && y.Date < (reservationDateTime.AddHours(1))))
                    throw new System.ArgumentException("Table is not available in desirable term");

                db.Reservations.Add(new Reservation()
                {
                    Date = reservationDateTime,
                    Name = name,
                    Table = table
                });
                db.SaveChangesAsync();
            });
        }

        public async Task RemoveReservation(int reservationId)
        {
            var _reservation = await db.Reservations.FirstOrDefaultAsync(x => x.ID == reservationId);

            if (_reservation == null)
                throw new System.ArgumentException("Reservation does not exists");
            if (_reservation.Date < DateTime.Now)
                throw new System.ArgumentException("You can not remove reservation from the past");

            db.Reservations.Remove(await db.Reservations.FirstOrDefaultAsync(x => x.ID == reservationId));
        }

        public DateTime ParseToDateTime(String dateToParse)
        {
            if (DateTime.TryParseExact(dateToParse, "dd--MM--yyyyTHH-mm", new CultureInfo("pl-PL"), DateTimeStyles.None, out DateTime date))
                return date;
            else
                throw new System.ArgumentException("Bad DateTime Format");
        }
    }
}