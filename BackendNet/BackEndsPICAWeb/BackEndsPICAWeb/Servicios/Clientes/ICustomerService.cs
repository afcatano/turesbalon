using System.ServiceModel;

namespace BackEndsPICAWeb.Servicios.Clientes
{

    [ServiceContract]
    interface ICustomerService
    {

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        GetCustomerResponse GetCustomer(GetCustomerRequest customerRequest);

        [OperationContract]
        PostCustomerResponse PostCustomer(PostCustomerRequest customerRequest);

        [OperationContract]
        PutCustomerResponse PutCustomer(PutCustomerRequest apcr_pcr);
    }

}