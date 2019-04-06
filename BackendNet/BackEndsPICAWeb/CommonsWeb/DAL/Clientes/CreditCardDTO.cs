using System;

namespace CommonsWeb.DAL.Clientes
{
    public class CreditCardDTO
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public string CardName { get; set; }
        public double ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public string StatusCard { get; set; }
    }

}