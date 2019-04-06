using System.ServiceModel;

namespace BackEndsPICAWeb.Servicios.Orders
{

    [ServiceContract]
    interface IOrderService
    {

        [OperationContract]
        GetOrderResponse GetOrder(GetOrderRequest agor_gor);

        [OperationContract]
        PostOrderRequest PostOrder(PostOrderRequest apor_por);

        [OperationContract]
        PutOrderResponse PutOrder(PutOrderRequest aprr_prr);


    }

}