using System;

namespace CommonsWeb.DTO
{
    public class TransportReservationDTO
    {

        public long Id { get; set; }
        public string BookingId { get; set; }
        public string CancelId { get; set; }
        public string CountryFrom { get; set; }
        public string CityFrom { get; set; }
        public string Name { get; set; }
        public DateTime DepartDate { get; set; }
        public string CountryTo { get; set; }
        public string CityTo { get; set; }
        public DateTime ArrivingDate { get; set; }
        public string Class { get; set; }
        public string Seat { get; set; }
        public decimal Price { get; set; }

    }
}