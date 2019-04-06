namespace BackEndsPICAWeb.Business.Orders
{
    interface IOrderServiceBusiness
    {

        //puto el que lo lea

		//danilo tiene hambre
        GetOrderResponse GetOrder(GetOrderRequest agor_gor);

        PostOrderRequest PostOrder(PostOrderRequest apor_por);

        PutOrderResponse PutOrder(PutOrderRequest aprr_prr);

    }
}