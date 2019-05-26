using SvcHilton.Business.HiltonBookingService;
using SvcHilton.Business.HiltonBookingService.DTO;
using SvcHilton.Business.HiltonBookingService.Imp;
using System;

namespace SvcHilton.Services.HiltonBookingService
{

    public class HiltonBookingService : IHiltonBookingService
    {
        public bookRoomResponse BookRoom(bookRoomRequest abrr_brr)
        {

            bookRoomResponse lbrr_response;

            lbrr_response = new bookRoomResponse();
            lbrr_response.Body = new bookRoomResponseBody();
            lbrr_response.Body.Status = new services.hilton.com.types.Status();

            try
            {

                RoomReservationDTO lrr_rr;
                IHiltonBookingServiceBusiness lhbsb_hbsb;
                long ll_bookingId;

                lrr_rr = new RoomReservationDTO();
                lrr_rr.GuestName = abrr_brr.Body.RoomReservation.guestName;
                lrr_rr.RoomNumber = abrr_brr.Body.RoomReservation.roomNumber;
                lrr_rr.CheckIn = abrr_brr.Body.RoomReservation.checkin;
                lrr_rr.CheckOut = abrr_brr.Body.RoomReservation.checkout;
                lrr_rr.Hotel = abrr_brr.Body.RoomReservation.hotel;
                lhbsb_hbsb = new HiltonBookingServiceBusiness();
                ll_bookingId = lhbsb_hbsb.BookRoom(lrr_rr);

                if (ll_bookingId > 0)
                {

                    lbrr_response.Body.Status.codeError = "0";
                    lbrr_response.Body.Status.message = "";
                    lbrr_response.Body.result = true;
                    lbrr_response.Body.bookingId = ll_bookingId.ToString();

                }
                else
                {

                    lbrr_response.Body.Status.codeError = "01";
                    lbrr_response.Body.Status.message = "Error en la reserva";
                    lbrr_response.Body.result = false;

                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lbrr_response.Body.Status.codeError = "01";
                lbrr_response.Body.Status.message = "Error en la ejecución del servicio:" + ae_e.Message;
                lbrr_response.Body.result = false;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO HiltonBookingService:BookRoom");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);

            }

            return lbrr_response;

        }

        public cancelBookingResponse CancelBooking(cancelBookingRequest acbr_cbr)
        {

            cancelBookingResponse lcbr_response;

            lcbr_response = new cancelBookingResponse();
            lcbr_response.Body = new cancelBookingResponseBody();
            lcbr_response.Body.Status = new services.hilton.com.types.Status();

            try
            {

                string ls_bookingId;
                IHiltonBookingServiceBusiness lhbsb_hbsb;
                long ll_cancelId;

                ls_bookingId = acbr_cbr.Body.bookingId;
                lhbsb_hbsb = new HiltonBookingServiceBusiness();
                ll_cancelId = lhbsb_hbsb.CancelBooking(ls_bookingId);

                if (ll_cancelId > 0)
                {

                    lcbr_response.Body.Status.codeError = "0";
                    lcbr_response.Body.Status.message = "";
                    lcbr_response.Body.result = true;
                    lcbr_response.Body.cancelId = ll_cancelId.ToString();

                }
                else
                {

                    lcbr_response.Body.Status.codeError = "01";
                    lcbr_response.Body.Status.message = "Error en la ejecución del servicio";
                    lcbr_response.Body.result = false;

                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lcbr_response.Body.Status.codeError = "01";
                lcbr_response.Body.Status.message = "Error en la ejecución del servicio:" + ae_e.Message;
                lcbr_response.Body.result = false;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO HiltonBookingService:CancelBooking");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);

            }

            return lcbr_response;

        }
    }
}