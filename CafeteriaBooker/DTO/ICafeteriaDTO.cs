using CafeteriaBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CafeteriaBooker.DTO
{
    public interface ICafeteriaDTO
    {
        Task<List<Table>> GetTablesMeetingReq(int minimumSeats, bool isSmokingAllowed);
        Task<Table> GetTableById(int tableId);
        Task<List<Table>> GetAllFreeTables(List<Table> tables, DateTime when);
        Task<Table> GetFirstFreeTable(List<Table> tables, DateTime when);

        Task<Reservation> GetReservationById(int reservationId);
        Task<List<Reservation>> GetReservationsByTableID(int tableId);
        Task MakeReservation(Table table, DateTime when, String name);
        Task RemoveReservation(int reservationId);

        DateTime ParseToDateTime(String dateToParse);
    }
}