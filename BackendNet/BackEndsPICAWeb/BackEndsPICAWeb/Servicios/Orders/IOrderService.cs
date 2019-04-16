using System.ServiceModel;

namespace BackEndsPICAWeb.Servicios.Orders
{

    [ServiceContract]
    interface IOrderService
    {

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        GetOrderResponse GetOrder(GetOrderRequest agor_gor);

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        PostOrderResponse PostOrder(PostOrderRequest apor_por);

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        PutOrderResponse PutOrder(PutOrderRequest aprr_prr);

    }

}