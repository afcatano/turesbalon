using BackEndsPICAWeb.Business.Clientes.DTO;

namespace BackEndsPICAWeb.Business
{
    interface ICustomerServiceBusiness
    {
        GetCustomerResponse GetResultCustomerDTOs(ClientesDTO searchClientesDTO);
        GetCustomerResponse GetResultCustomerDTOsPaginado(ClientesDTO prmsearchClientesDTO);
        PostCustomerResponse PostCustomerRequest(PostCustomerRequest apcr_pcr);
        PutCustomerResponse PutCustomerRequest(ClientesDTO UpdateClientesDTO);
        GetLoginResponse GetResultLoginDTOs(ClientesDTO loginClientesDTO);
    }
}
