using System;

namespace CommonsWeb.DTO
{
    public class OrderDTO
    {

        public long OrderCode { get; set; }
        public DateTime OrderDateFrom { get; set; }
        public DateTime OrderDateTo { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderValue { get; set; }
        public long IdUser { get; set; }
        public long IdType { get; set; }
        public long IdNumber { get; set; }
        public long EventCode { get; set; }
        public long HotelCode { get; set; }
        public long TransportCode { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string EventCity { get; set; }
        public string EventCountry { get; set; }
        public string EventType { get; set; }
        public decimal EventPrice { get; set; }
        public bool FlagDetail { get; set; }
        public HotelReservationDTO Hotel { get; set; }
        public TransportReservationDTO Transport { get; set; }


        public override bool Equals(object ao_obj)
        {

            OrderDTO lo_order;

            lo_order = ao_obj as OrderDTO;

            if (lo_order == null)
                return false;

            if (OrderCode != lo_order.OrderCode || OrderDateFrom != lo_order.OrderDateFrom ||
                OrderDateTo != lo_order.OrderDateTo || OrderStatus != lo_order.OrderStatus ||
                OrderValue != lo_order.OrderValue || IdUser != lo_order.IdUser ||
                IdType != lo_order.IdType || IdNumber != lo_order.IdNumber ||
                EventCode != lo_order.EventCode || HotelCode != lo_order.HotelCode ||
                TransportCode != lo_order.TransportCode || FlagDetail != lo_order.FlagDetail)
                return false;

            return true;
        }

    }
}