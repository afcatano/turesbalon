namespace BackEndsPICAWeb.Business.Orders
{
    interface IOrderServiceBusiness
    {

        GetOrderResponse GetOrder(GetOrderRequest agor_gor);

        PostOrderResponse PostOrder(PostOrderRequest apor_por);

        PutOrderResponse PutOrder(PutOrderRequest aprr_prr);

    }
}