using System;

namespace CommonsWeb.DTO
{
    public class OrderDTO
    {

        public long OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderValue { get; set; }
        public long IdType { get; set; }
        public long IdNumber { get; set; }
        public long EventCode { get; set; }
        public long HotelCode { get; set; }
        public long TransportCode { get; set; }

    }
}