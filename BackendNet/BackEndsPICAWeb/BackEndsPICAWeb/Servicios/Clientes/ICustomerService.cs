using System.ServiceModel;

namespace BackEndsPICAWeb.Servicios.Clientes
{

    [ServiceContract]
    interface ICustomerService
    {

        [XmlSerializerFormatAttribute()] //comando para el formato del WSDL
        [OperationContract]
        GetCustomerResponse GetCustomer(GetCustomerRequest customerRequest);

        [OperationContract]
        PostCustomerResponse PostCustomer(PostCustomerRequest customerRequest);

        [OperationContract]
        PutCustomerResponse PutCustomer(PutCustomerRequest apcr_pcr);
    }

}