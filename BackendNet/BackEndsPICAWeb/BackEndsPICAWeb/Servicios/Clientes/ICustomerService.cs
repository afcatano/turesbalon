using System.ServiceModel;

namespace BackEndsPICAWeb.Servicios.Clientes
{

    [ServiceContract]
    interface ICustomerService
    {

        [XmlSerializerFormat()] //comando para el formato del WSDL
        [OperationContract]
        GetCustomerResponse GetCustomer(GetCustomerRequest customerRequest);

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        PostCustomerResponse PostCustomer(PostCustomerRequest customerRequest);

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        PutCustomerResponse PutCustomer(PutCustomerRequest apcr_pcr);
    }

}