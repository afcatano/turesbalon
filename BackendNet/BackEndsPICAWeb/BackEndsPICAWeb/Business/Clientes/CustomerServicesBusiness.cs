using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackEndsPICAWeb.Business.Clientes.DTO;
using CommonsWeb.DAL.Clientes;

namespace BackEndsPICAWeb.Business.Clientes
{
    public class CustomerServicesBusiness : ICustomerServiceBusiness
    {
        GetCustomerResponse ICustomerServiceBusiness.GetResultCustomerDTOs(ClientesDTO prmsearchClientesDTO)
        {
            GetCustomerResponse customerResponse = new GetCustomerResponse();
            customerResponse.status = new Status();

            try
            {
                //validar tipos de datos


                List<ClientesDTO> lstClientesDTO;
                lstClientesDTO = null;


                ClientesDAL ClientesDAL = new ClientesDAL();
                lstClientesDTO = ClientesDAL.GetClientes(prmsearchClientesDTO);

                if (lstClientesDTO != null)
                {
                    if (lstClientesDTO.Count > 0)
                    {
                        List<GetCustomerResult> GetCustomerResult = new List<GetCustomerResult>();

                        foreach (ClientesDTO clientesDTO in lstClientesDTO)
                        {

                            GetCustomerResult lCustomer;
                            List<CreditCardGet> lcreditCard = new List<CreditCardGet>();

                            lCustomer = new GetCustomerResult
                            {
                                IdType = clientesDTO.CodTypeIdent,
                                FirstName = clientesDTO.FName,
                                LastNames = clientesDTO.LName,
                                IdNumber = clientesDTO.CustID,
                                PhoneNumber = clientesDTO.PhoneNumber,
                                Email = clientesDTO.Email,
                                Address = clientesDTO.Address,
                                Country = clientesDTO.Country,
                                City = clientesDTO.City,
                                User = clientesDTO.User,
                                Password = clientesDTO.Password,
                                StatusCustomer = clientesDTO.Status
                            };

                            if (clientesDTO.LCreditCard != null)
                            {
                                foreach (CreditCardDTO creditCardDTO in clientesDTO.LCreditCard)
                                {

                                    CreditCardGet creditCard = new CreditCardGet
                                    {
                                        CardName = creditCardDTO.CardName,
                                        ExpirationDate = creditCardDTO.ExpirationDate,
                                        Type = creditCardDTO.Type,
                                        Number = creditCardDTO.Number,
                                        SecurityCode = creditCardDTO.SecurityCode,
                                        StatusCard = creditCardDTO.StatusCard
                                    };

                                    lcreditCard.Add(creditCard);
                                    lCustomer.CreditCard = creditCard;
                                }
                            }

                            GetCustomerResult.Add(lCustomer);
                        }

                        customerResponse.result = GetCustomerResult.ToArray();
                        customerResponse.status.CodeResp = "0";
                        customerResponse.status.MessageResp = "Proceso satisfactorio";
                    }
                    else
                    {
                        customerResponse.status.CodeResp = "01";
                        customerResponse.status.MessageResp = "no existen datos....";
                    }
                }
                else
                {

                    customerResponse.status.CodeResp = "01";
                    customerResponse.status.MessageResp = "error....";
                }
            }
            catch (Exception ex)
            {
                customerResponse.status.CodeResp = "01";
                customerResponse.status.MessageResp = "Error en la....";
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO CustomerService:GetCustomer");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ex.Message);
                throw ex;
            }

            return customerResponse;
        }

        PutCustomerResponse ICustomerServiceBusiness.PutCustomerRequest(ClientesDTO UpdateClientesDTO)
        {
            int rsta = 0;
            PutCustomerResponse PUTcustomerResponse = new PutCustomerResponse();
            PUTcustomerResponse.status = new Status();

            ClientesDAL ClientesDAL = new ClientesDAL();
            rsta = ClientesDAL.UpdateClientes(UpdateClientesDTO);

            if (rsta == 0)
            {
                PUTcustomerResponse.status.CodeResp = "01";
                PUTcustomerResponse.status.MessageResp = "Error en Actualizacion";
            }
            else
            {
                PUTcustomerResponse.status.CodeResp = "0";
                PUTcustomerResponse.status.MessageResp = "Proceso Satisfactorio";
            }

            return PUTcustomerResponse;
        }

        PostCustomerResponse ICustomerServiceBusiness.PostCustomerRequest(PostCustomerRequest apcr_request)
        {

            PostCustomerResponse lpcr_response;

            lpcr_response = new PostCustomerResponse();
            lpcr_response.status = new Status();

            try
            {

                if (apcr_request != null)
                {

                    if (apcr_request.Customer != null)
                    {


                        ClientesDTO lc_cliente;

                        lc_cliente = new ClientesDTO();

                        if (apcr_request.Customer.IdType != null)
                        {

                            if (apcr_request.Customer.IdType.Trim().Length > 0)
                                lc_cliente.CodTypeIdent = apcr_request.Customer.IdType;
                            else
                                throw new Exception("El tipo de identificación es obligatorio");

                        }
                        else
                            throw new Exception("El tipo de identificación es obligatorio");

                        if (apcr_request.Customer.IdNumber <= 0)
                            throw new Exception("Numero de indentificación vacio o no valido");
                        else
                            lc_cliente.CustID = apcr_request.Customer.IdNumber;

                        if (apcr_request.Customer.FirstName != null)
                        {

                            if (apcr_request.Customer.FirstName.Trim().Length > 0)
                                lc_cliente.FName = apcr_request.Customer.FirstName;
                            else
                                throw new Exception("El nombre del cliente es obligatorio");

                        }

                        else
                            throw new Exception("El nombre del cliente es obligatorio");

                        if (apcr_request.Customer.LastNames != null)
                        {

                            if (apcr_request.Customer.LastNames.Trim().Length > 0)
                                lc_cliente.LName = apcr_request.Customer.LastNames;
                            else
                                throw new Exception("El apellido del cliente es obligatorio");

                        }
                        else
                            throw new Exception("El apellido del cliente es obligatorio");

                        if (apcr_request.Customer.Email != null)
                            if (apcr_request.Customer.Email.Trim().Length > 0)
                                lc_cliente.Email = apcr_request.Customer.Email;

                        if (apcr_request.Customer.PhoneNumber != null)
                            if (apcr_request.Customer.PhoneNumber.Trim().Length > 0)
                                lc_cliente.PhoneNumber = apcr_request.Customer.PhoneNumber;

                        if (apcr_request.Customer.Address != null)
                        {

                            if (apcr_request.Customer.Address.Trim().Length > 0)
                                lc_cliente.Address = apcr_request.Customer.Address;
                            else
                                throw new Exception("La dirección del cliente es obligatoria");

                        }
                        else
                            throw new Exception("La dirección del cliente es obligatoria");

                        if (apcr_request.Customer.City != null)
                        {

                            if (apcr_request.Customer.City.Trim().Length > 0)
                                lc_cliente.City = apcr_request.Customer.City;
                            else
                                throw new Exception("La ciudad del cliente es obligatoria");

                        }
                        else
                            throw new Exception("La ciudad del cliente es obligatoria");

                        if (apcr_request.Customer.Country != null)
                        {

                            if (apcr_request.Customer.Country.Trim().Length > 0)
                                lc_cliente.Country = apcr_request.Customer.Country;
                            else
                                throw new Exception("El país del cliente es obligatorio");

                        }
                        else
                            throw new Exception("El país del cliente es obligatorio");

                        if (apcr_request.Customer.User != null)
                        {

                            if (apcr_request.Customer.User.Trim().Length > 0)
                                lc_cliente.User = apcr_request.Customer.User;
                            else
                                throw new Exception("El username del cliente es obligatorio");

                        }
                        else
                            throw new Exception("El username del cliente es obligatorio");

                        if (apcr_request.Customer.Password != null)
                        {

                            if (apcr_request.Customer.Password.Trim().Length > 0)
                                lc_cliente.Password = apcr_request.Customer.Password;
                            else
                                throw new Exception("El password del cliente es obligatorio");

                        }
                        else
                            throw new Exception("El password del cliente es obligatorio");

                        if (apcr_request.Customer.CreditCard != null)
                        {

                            CreditCardDTO lcc_creditCard;
                            DateTime ldt_fechaVencimiento;

                            lcc_creditCard = new CreditCardDTO();
                            ldt_fechaVencimiento = new DateTime();
                            lc_cliente.LCreditCard = new List<CreditCardDTO>();

                            if (apcr_request.Customer.CreditCard.Type != null)
                            {

                                if (apcr_request.Customer.CreditCard.Type.Trim().Length > 0)
                                    lcc_creditCard.Type = apcr_request.Customer.CreditCard.Type;
                                else
                                    throw new Exception("El tipo de tarjeta de credito es obligatorio");

                            }
                            else
                                throw new Exception("El tipo de tarjeta de credito es obligatorio");

                            if (apcr_request.Customer.CreditCard.Number != null)
                            {

                                if (apcr_request.Customer.CreditCard.Number.Trim().Length > 0)
                                    lcc_creditCard.Number = apcr_request.Customer.CreditCard.Number;
                                else
                                    throw new Exception("El numero de tarjeta de credito es obligatorio");

                            }
                            else
                                throw new Exception("El numero de tarjeta de credito es obligatorio");

                            if (apcr_request.Customer.CreditCard.CardName != null)
                            {

                                if (apcr_request.Customer.CreditCard.CardName.Trim().Length > 0)
                                    lcc_creditCard.CardName = apcr_request.Customer.CreditCard.CardName;
                                else
                                    throw new Exception("El nombre en la tarjeta de credito es obligatorio");

                            }
                            else
                                throw new Exception("El nombre en la tarjeta de credito es obligatorio");

                            if (apcr_request.Customer.CreditCard.ExpirationDate <= 0)
                                throw new Exception("La fecha de vencimiento en la tarjeta de credito es obligatorio");
                            else if (!DateTime.TryParseExact("01" + apcr_request.Customer.CreditCard.ExpirationDate.ToString(),
                                "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out ldt_fechaVencimiento))
                                throw new Exception("La fecha de vencimiento en la tarjeta de credito es obligatorio");
                            else
                                lcc_creditCard.ExpirationDate = apcr_request.Customer.CreditCard.ExpirationDate;

                            if (apcr_request.Customer.CreditCard.SecurityCode != null)
                            {

                                if (apcr_request.Customer.CreditCard.SecurityCode.Trim().Length > 0)
                                    lcc_creditCard.SecurityCode = apcr_request.Customer.CreditCard.SecurityCode;
                                else
                                    throw new Exception("El codigo de seguridad en la tarjeta de credito es obligatorio");

                            }
                            else
                                throw new Exception("El codigo de seguridad en la tarjeta de credito es obligatorio");

                            lc_cliente.LCreditCard.Add(lcc_creditCard);

                        }

                        ClientesDAL lcd_clientesDAL;

                        lcd_clientesDAL = new ClientesDAL();

                        if (lcd_clientesDAL.InsertarCliente(lc_cliente))
                        {
                            lpcr_response.status.CodeResp = "0";
                            lpcr_response.status.MessageResp = "";
                        }
                        else
                        {
                            lpcr_response.status.CodeResp = "01";
                            lpcr_response.status.MessageResp = "Error ingresando datos";
                        }

                    }
                    else
                    {
                        throw new Exception("Parametros de entrada vacios");
                    }

                }
                else
                {
                    throw new Exception("Parametros de entrada vacios");
                }


            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lpcr_response.status.CodeResp = "01";
                lpcr_response.status.MessageResp = ae_e.InnerException != null ? "Error en la ejecucion del servicio" : ae_e.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO OrderService:GetOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;

            }

            return lpcr_response;
        }

    }
}