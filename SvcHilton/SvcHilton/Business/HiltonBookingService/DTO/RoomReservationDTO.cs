using System;

namespace SvcHilton.Business.HiltonBookingService.DTO
{
    public class RoomReservationDTO
    {

        public string GuestName { get; set; }
        public int RoomNumber { get; set; }
        public DateTime? CheckOut { get; set; }
        public string Hotel { get; set; }
        public DateTime? CheckIn { get; set; }

    }
}