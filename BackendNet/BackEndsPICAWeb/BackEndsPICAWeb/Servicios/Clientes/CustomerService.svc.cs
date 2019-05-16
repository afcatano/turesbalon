using BackEndsPICAWeb.Business;
using BackEndsPICAWeb.Business.Clientes;
using BackEndsPICAWeb.Business.Clientes.DTO;
using System;

namespace BackEndsPICAWeb.Servicios.Clientes
{

    public class CustomerService : ICustomerService
    {

        GetCustomerResponse ICustomerService.GetCustomer(GetCustomerRequest prmcustomerRequest)
        {
            GetCustomerResponse customerResponse = new GetCustomerResponse();

            try
            {
                ClientesDTO clientesDTO;
                ICustomerServiceBusiness iCSBusiness;

                clientesDTO = new ClientesDTO
                {
                    CodTypeIdent = prmcustomerRequest.Customer.IdType,
                    CustID = prmcustomerRequest.Customer.IdNumber,
                    FName = prmcustomerRequest.Customer.FirstName,
                    LName = prmcustomerRequest.Customer.LastNames,
                    Email = prmcustomerRequest.Customer.Email,
                    PhoneNumber = prmcustomerRequest.Customer.PhoneNumber,
                    Address = prmcustomerRequest.Customer.Address,
                    City = prmcustomerRequest.Customer.City,
                    Country = prmcustomerRequest.Customer.Country,
                    User = prmcustomerRequest.Customer.User,
                    Status = prmcustomerRequest.Customer.StatusCustomer,
                    Pagina = prmcustomerRequest.Customer.Page,
                    RegsxPagina = prmcustomerRequest.Customer.RegsPerPage,
                    RegsTotales= prmcustomerRequest.Customer.TotalsRegs,
                    FechaIniFact = prmcustomerRequest.Customer.DateIniFact,
                    FechaFinFact = prmcustomerRequest.Customer.DateFinFact,
                    Evento  = prmcustomerRequest.Customer.EventType
                };

                if (clientesDTO.Pagina == 0)
                {
                    iCSBusiness = new CustomerServicesBusiness();
                    customerResponse = iCSBusiness.GetResultCustomerDTOs(clientesDTO);
                }
                else
                {
                    iCSBusiness = new CustomerServicesBusiness();
                    customerResponse = iCSBusiness.GetResultCustomerDTOsPaginado(clientesDTO);
                }
            }
            catch (Exception ex)
            {

                customerResponse.status.CodeResp = "";
                customerResponse.status.MessageResp = "";
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO CustomerService:GetCustomer " + ex.Message);
                throw ex;
            }

            return customerResponse;
        }

        PutCustomerResponse ICustomerService.PutCustomer(PutCustomerRequest prmcustomerRequest)
        {
            PutCustomerResponse putCustomer = new PutCustomerResponse();

            try
            {
                ClientesDTO clientesDTO;
                ICustomerServiceBusiness iCSBusiness;

                clientesDTO = new ClientesDTO
                {
                    CodTypeIdent = prmcustomerRequest.Customer.IdType,
                    CustID = prmcustomerRequest.Customer.IdNumber,
                    FName = prmcustomerRequest.Customer.FirstName,
                    LName = prmcustomerRequest.Customer.LastNames,
                    Email = prmcustomerRequest.Customer.Email,
                    PhoneNumber = prmcustomerRequest.Customer.PhoneNumber,
                    Address = prmcustomerRequest.Customer.Address,
                    City = prmcustomerRequest.Customer.City,
                    Country = prmcustomerRequest.Customer.Country,
                    User = prmcustomerRequest.Customer.User,
                    Status = prmcustomerRequest.Customer.StatusCustomer,
                    Password = prmcustomerRequest.Customer.Password
                };

                iCSBusiness = new CustomerServicesBusiness();
                putCustomer = iCSBusiness.PutCustomerRequest(clientesDTO);
            }
            catch (Exception ex)
            {

                putCustomer.status.CodeResp = "01";
                putCustomer.status.MessageResp = "Error en el Servicio";
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO CustomerService:PutCustomer " + ex.Message);
                throw ex;
            }

            return putCustomer;
        }

        public PostCustomerResponse PostCustomer(PostCustomerRequest apcr_request)
        {

            PostCustomerResponse lpcr_response;

            lpcr_response = new PostCustomerResponse();
            lpcr_response.status = new Status();

            try
            {

                ICustomerServiceBusiness licsb_icsb;

                licsb_icsb = new CustomerServicesBusiness();
                lpcr_response = licsb_icsb.PostCustomerRequest(apcr_request);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lpcr_response.status.CodeResp = "01";
                lpcr_response.status.MessageResp = ae_e.InnerException != null ? "Error en la ejecucion del servicio" : ae_e.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE SERVICIO CustomerService:PostCustomer");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);

            }

            return lpcr_response;

        }

    }
}
