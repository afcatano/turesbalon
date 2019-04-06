using CommonsWeb.DAL.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndsPICAWeb.Business.Clientes.DTO
{
    public class ClientesDTO
    {
        public int CustID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string CodTypeIdent { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public List<CreditCardDTO>  LCreditCard { get; set; }

    }
}