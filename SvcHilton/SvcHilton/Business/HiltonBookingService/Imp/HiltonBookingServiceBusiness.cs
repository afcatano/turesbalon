using SvcHilton.Business.HiltonBookingService.DTO;
using SvcHilton.DAL.HiltonBookingService;
using SvcHilton.DAL.HiltonBookingService.Imp;
using System;

namespace SvcHilton.Business.HiltonBookingService.Imp
{
    public class HiltonBookingServiceBusiness : IHiltonBookingServiceBusiness
    {
        public long BookRoom(RoomReservationDTO arr_arr)
        {

            long ll_bookingId;

            ll_bookingId = 0;

            try
            {

                if (arr_arr.GuestName == null || arr_arr.GuestName.Trim().Length == 0)
                    throw new Exception("El nombre del huesped es obligatorio");

                if (arr_arr.RoomNumber == 0)
                    throw new Exception("El numero de la habitación es obligatorio");

                if (arr_arr.CheckIn == null)
                    throw new Exception("El check-in es obligatorio");

                if (arr_arr.CheckOut == null)
                    throw new Exception("El check-out es obligatorio");

                if (arr_arr.Hotel == null || arr_arr.Hotel.Trim().Length == 0)
                    throw new Exception("El hotel es obligatorio");

                IHiltonBookingServiceDAL lhbsDAL_hbsDAL;

                lhbsDAL_hbsDAL = new HiltonBookingServiceDAL();
                ll_bookingId = lhbsDAL_hbsDAL.BookRoom(arr_arr);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                ll_bookingId = -1;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO HiltonBookingService BookRoom");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);
                throw ae_e;

            }

            return ll_bookingId;

        }

        public long CancelBooking(string as_bookingId)
        {

            long ll_cancelId;

            ll_cancelId = 0;

            try
            {

                long ll_result;

                ll_result = 0;

                if (as_bookingId == null || as_bookingId.Trim().Length == 0)
                    throw new Exception("El id de la reserva es obligatorio");

                if (!long.TryParse(as_bookingId, out ll_result))
                    throw new Exception("El id de la reserva debe ser numerico");


                IHiltonBookingServiceDAL lhbsDAL_hbsDAL;

                lhbsDAL_hbsDAL = new HiltonBookingServiceDAL();
                ll_cancelId = lhbsDAL_hbsDAL.CancelBooking(as_bookingId);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                ll_cancelId = -1;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO HiltonBookingService:CancelBooking");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);
                throw ae_e;

            }

            return ll_cancelId;

        }
    }
}