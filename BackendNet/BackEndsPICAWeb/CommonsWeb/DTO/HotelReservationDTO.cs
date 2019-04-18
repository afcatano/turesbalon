using System;

namespace CommonsWeb.DTO
{
    public class HotelReservationDTO
    {

        public long Id { get; set; }
        public string BookingId { get; set; }
        public string CancelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string RoomNumber { get; set; }
        public string TypeRoom { get; set; }
        public decimal PriceRoom { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string CompanyName { get; set; }

    }
}