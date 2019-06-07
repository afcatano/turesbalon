using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonsWeb.DTO
{
    public class ReservationsDTO
    {
        public DateTime BookDate { get; set; }
        public int BranchId { get; set; }
        public int RoomId { get; set; }
        public string GuestFullName { get; set; }
        public string GuestDocumentNumber { get; set; }
        public int GuestDocumentTypeId { get; set; }
        public string SourceCompanyCode { get; set; }
        public bool IsCancelprocess { get; set; }
        public string BranchCode { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string BranchName { get; set; }
        public string RoomName { get; set; }
        public string Number { get; set; }
        public int Beds { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public string CityName { get; set; }
        public string CiiuCode { get; set; }
        public int ID { get; set; }
        public decimal Stars { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }

    }
}