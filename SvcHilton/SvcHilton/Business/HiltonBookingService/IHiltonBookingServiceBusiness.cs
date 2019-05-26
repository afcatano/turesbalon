using SvcHilton.Business.HiltonBookingService.DTO;

namespace SvcHilton.Business.HiltonBookingService
{
    interface IHiltonBookingServiceBusiness
    {

        long BookRoom(RoomReservationDTO arr_arr);

        long CancelBooking(string as_s);

    }
}
