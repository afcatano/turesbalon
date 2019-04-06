using BackEndsPICAWeb.Business.Clientes.DTO;

namespace BackEndsPICAWeb.Business
{
    interface ICustomerServiceBusiness
    {
        GetCustomerResponse GetResultCustomerDTOs(ClientesDTO searchClientesDTO);
        PostCustomerResponse PostCustomerRequest(PostCustomerRequest apcr_pcr);
        PutCustomerResponse PutCustomerRequest(ClientesDTO UpdateClientesDTO);
    }
}
