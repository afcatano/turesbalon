using SvcHilton.Business.HiltonBookingService.DTO;

namespace SvcHilton.DAL.HiltonBookingService
{
    interface IHiltonBookingServiceDAL
    {

        long BookRoom(RoomReservationDTO arr_arr);

        long CancelBooking(string as_s);

    }
}