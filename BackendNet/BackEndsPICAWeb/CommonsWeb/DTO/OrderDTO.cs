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

        public override bool Equals(object ao_obj)
        {

            OrderDTO lo_order;

            lo_order = ao_obj as OrderDTO;

            if (lo_order == null)
                return false;

            if (OrderCode != lo_order.OrderCode || OrderDate != lo_order.OrderDate ||
                OrderStatus != lo_order.OrderStatus || OrderValue != lo_order.OrderValue ||
                IdType != lo_order.IdType || IdNumber != lo_order.IdNumber ||
                EventCode != lo_order.EventCode || HotelCode != lo_order.HotelCode ||
                TransportCode != lo_order.TransportCode)
                return false;

            return true;
        }

    }
}