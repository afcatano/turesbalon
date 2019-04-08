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
        public List<CreditCardDTO> LCreditCard { get; set; }

        public override bool Equals(object obj)
        {

            ClientesDTO lc_cliente;

            lc_cliente = obj as ClientesDTO;

            if (lc_cliente == null)
                return false;

            if (CustID != lc_cliente.CustID || FName != lc_cliente.FName || LName != lc_cliente.LName ||
                Email != lc_cliente.Email || PhoneNumber != lc_cliente.PhoneNumber ||
                Address != lc_cliente.Address || City != lc_cliente.City || Country != lc_cliente.Country ||
                User != lc_cliente.User || Status != lc_cliente.Status || Password != lc_cliente.Password)
                return false;

            return true;
        }


    }
}