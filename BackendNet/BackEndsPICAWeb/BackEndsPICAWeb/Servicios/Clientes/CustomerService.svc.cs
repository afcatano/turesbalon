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
                    Status = prmcustomerRequest.Customer.StatusCustomer
                };

                iCSBusiness = new CustomerServicesBusiness();
                customerResponse = iCSBusiness.GetResultCustomerDTOs(clientesDTO);
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
            PutCustomerResponse postCustomer = new PutCustomerResponse();

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
                    Status = prmcustomerRequest.Customer.StatusCustomer
                };

                iCSBusiness = new CustomerServicesBusiness();
                postCustomer = iCSBusiness.PutCustomerRequest(clientesDTO);
            }
            catch (Exception ex)
            {

                postCustomer.status.CodeResp = "01";
                postCustomer.status.MessageResp = "Error en el Servicio";
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO CustomerService:PostCustomer " + ex.Message);
                throw ex;
            }

            return postCustomer;
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
