using CafeteriaBooker.Context;
using CafeteriaBooker.DTO;
using CafeteriaBooker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CafeteriaBooker.Controllers
{
    public class ReservationController : ApiController
    {
        private ICafeteriaDTO cafeteriaDTO = new CafeteriaDTO();
 
        [HttpGet]
        [Route("GetFirstFreeTable/{minimumSeats}/{isSmokingAllowed}/{reservationDateTime}")]
        public async Task<HttpResponseMessage> GetFirstFreeTable(int minimumSeats, bool isSmokingAllowed, String reservationDateTime)
        {
            var _response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var _tables = await cafeteriaDTO.GetTablesMeetingReq(minimumSeats, isSmokingAllowed);
                DateTime _when = cafeteriaDTO.ParseToDateTime(reservationDateTime);
                _response.Content = new StringContent(JsonConvert.SerializeObject(await cafeteriaDTO.GetFirstFreeTable(_tables, _when)), Encoding.UTF8, "application/json");
                return _response;
            }catch(Exception e)
            {
                _response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                _response.Content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                return _response;
            }
        }

        [HttpGet]
        [Route("GetTableReservations/{tableId}")]
        public async Task<HttpResponseMessage> GetTableReservations(int tableId)
        {
            var _response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                _response.Content = new StringContent(JsonConvert.SerializeObject(await cafeteriaDTO.GetReservationsByTableID(tableId)), Encoding.UTF8, "application/json");
                return _response;
            }
            catch (Exception e)
            {
                _response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                _response.Content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                return _response;
            }
        }

        [HttpGet]
        [Route("GetListOfFreeTables/{minimumSeats}/{isSmokingAllowed}/{reservationDateTime}")]
        public async Task<HttpResponseMessage> GetListOfFreeTables(int minimumSeats, bool isSmokingAllowed, String reservationDateTime)
        {
            var _response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var _tables = await cafeteriaDTO.GetTablesMeetingReq(minimumSeats, isSmokingAllowed);
                DateTime _when = cafeteriaDTO.ParseToDateTime(reservationDateTime);
                _response.Content = new StringContent(JsonConvert.SerializeObject(await cafeteriaDTO.GetAllFreeTables(_tables, _when)), Encoding.UTF8, "application/json");
                return _response;
            }catch(Exception e)
            {
                _response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                _response.Content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                return _response;
            }
        }

        [HttpPost]
        [Route("BookTable/{tableId}/{reservationDateTime}/{reservingName}")]
        public async Task<HttpResponseMessage> BookTable(int tableId, String reservationDateTime, String reservingName)
        {
            try
            {
                var _table = await cafeteriaDTO.GetTableById(tableId);
                DateTime _reservationDateTime = cafeteriaDTO.ParseToDateTime(reservationDateTime);

                await cafeteriaDTO.MakeReservation(_table, _reservationDateTime, reservingName);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                var _response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                _response.Content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                return _response;
            }
        }

        [HttpDelete]
        [Route("CancelReservationById/{reservationId}")]
        public async Task<HttpResponseMessage> CancelReservationById(int reservationId)
        {
            try
            {
                await cafeteriaDTO.RemoveReservation(reservationId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                var _response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                _response.Content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                return _response;
            }
        }
    }
}