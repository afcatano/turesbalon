using BackEndsPICAWeb.Business.Orders;
using System;

namespace BackEndsPICAWeb.Servicios.Orders
{

    public class OrderService : IOrderService
    {

        public GetOrderResponse GetOrder(GetOrderRequest agor_gor)
        {

            GetOrderResponse lgor_response;

            lgor_response = new GetOrderResponse();
            lgor_response.status = new Status();

            try
            {

                IOrderServiceBusiness liosb_iosb;

                liosb_iosb = new OrderServiceBusiness();
                lgor_response = liosb_iosb.GetOrder(agor_gor);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lgor_response.status.CodeResp = "01";
                lgor_response.status.MessageResp = ae_e.InnerException != null ? "Error en la ejecucion del servicio" : ae_e.Message;
                lgor_response.result = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE SERVICIO OrderService:GetOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);

            }

            return lgor_response;

        }

        public PostOrderRequest PostOrder(PostOrderRequest apor_por)
        {
            throw new NotImplementedException();
        }

        public PutOrderResponse PutOrder(PutOrderRequest aprr_prr)
        {

            PutOrderResponse lpor_response;

            lpor_response = new PutOrderResponse();
            lpor_response.status = new Status();

            try
            {

                IOrderServiceBusiness liosb_iosb;

                liosb_iosb = new OrderServiceBusiness();
                lpor_response = liosb_iosb.PutOrder(aprr_prr);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lpor_response.status.CodeResp = "01";
                lpor_response.status.MessageResp = ae_e.InnerException != null ? "Error en la ejecucion del servicio" : ae_e.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE SERVICIO OrderService:PutOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);

            }

            return lpor_response;

        }
    }
}
