﻿using CommonsWeb.DAL.Orders;
using CommonsWeb.DTO;
using System;
using System.Collections.Generic;

namespace BackEndsPICAWeb.Business.Orders
{
    public class OrderServiceBusiness : IOrderServiceBusiness
    {
        public GetOrderResponse GetOrder(GetOrderRequest agor_gor)
        {

            GetOrderResponse lgor_response;

            lgor_response = new GetOrderResponse();
            lgor_response.status = new Status();

            try
            {

                if (agor_gor != null)
                {

                    if (agor_gor.Order != null)
                    {

                        OrderDTO lo_order;

                        lo_order = new OrderDTO();

                        if (agor_gor.Order.OrderCode != null)
                        {

                            if (agor_gor.Order.OrderCode.Trim().Length > 0)
                            {

                                long ll_result;

                                ll_result = 0;

                                if (!long.TryParse(agor_gor.Order.OrderCode, out ll_result))
                                    throw new Exception("El codigo de la orden debe ser numerico");
                                else if (long.Parse(agor_gor.Order.OrderCode) > 0)
                                    lo_order.OrderCode = long.Parse(agor_gor.Order.OrderCode);

                            }

                        }

                        if (agor_gor.Order.OrderDate != null)
                            lo_order.OrderDate = agor_gor.Order.OrderDate;

                        if (agor_gor.Order.OrderStatus != null)
                            if (agor_gor.Order.OrderStatus.Trim().Length > 0)
                                lo_order.OrderStatus = agor_gor.Order.OrderStatus;

                        if (agor_gor.Order.OrderValue > 0)
                            lo_order.OrderValue = agor_gor.Order.OrderValue;

                        if (agor_gor.Order.IdType != null)
                        {

                            if (agor_gor.Order.IdType.Trim().Length > 0)
                            {

                                long ll_result;

                                ll_result = 0;

                                if (!long.TryParse(agor_gor.Order.IdType, out ll_result))
                                    throw new Exception("El codigo del tipo documento debe ser numerico");
                                else
                                    lo_order.IdType = Convert.ToInt32(agor_gor.Order.IdType);

                            }

                            else
                                throw new Exception("El tipo de documento es obligatorio");

                        }
                        else
                            throw new Exception("El tipo de documento es obligatorio");

                        if (agor_gor.Order.IdNumber != null)
                        {

                            if (agor_gor.Order.IdNumber.Trim().Length > 0)
                            {

                                long ll_result;

                                ll_result = 0;

                                if (!long.TryParse(agor_gor.Order.IdNumber, out ll_result))
                                    throw new Exception("El documento debe ser numerico");
                                else if (long.Parse(agor_gor.Order.IdNumber) > 0)
                                    lo_order.IdNumber = long.Parse(agor_gor.Order.IdNumber);

                            }
                            else
                                throw new Exception("El documento es obligatorio");

                        }
                        else
                            throw new Exception("El documento es obligatorio");

                        if (agor_gor.Order.EventCode != null)
                        {

                            if (agor_gor.Order.EventCode.Trim().Length > 0)
                            {

                                long ll_result;

                                ll_result = 0;

                                if (!long.TryParse(agor_gor.Order.EventCode, out ll_result))
                                    throw new Exception("El codigo del evento debe ser numerico");
                                else if (long.Parse(agor_gor.Order.EventCode) > 0)
                                    lo_order.EventCode = long.Parse(agor_gor.Order.EventCode);

                            }

                        }

                        if (agor_gor.Order.HotelCode != null)
                        {

                            if (agor_gor.Order.HotelCode.Trim().Length > 0)
                            {

                                long ll_result;

                                ll_result = 0;

                                if (!long.TryParse(agor_gor.Order.HotelCode, out ll_result))
                                    throw new Exception("El codigo del evento debe ser numerico");
                                else if (long.Parse(agor_gor.Order.HotelCode) > 0)
                                    lo_order.HotelCode = long.Parse(agor_gor.Order.HotelCode);

                            }

                        }

                        if (agor_gor.Order.TransportCode != null)
                        {

                            if (agor_gor.Order.TransportCode.Trim().Length > 0)
                            {

                                long ll_result;

                                ll_result = 0;

                                if (!long.TryParse(agor_gor.Order.TransportCode, out ll_result))
                                    throw new Exception("El codigo del evento debe ser numerico");
                                else if (long.Parse(agor_gor.Order.TransportCode) > 0)
                                    lo_order.TransportCode = long.Parse(agor_gor.Order.TransportCode);

                            }

                        }

                        OrderServiceDAL losd_losDAL;
                        List<OrderDTO> llo_orders;

                        losd_losDAL = new OrderServiceDAL();
                        llo_orders = losd_losDAL.GetOrder(lo_order);

                        if (llo_orders != null)
                        {

                            if (llo_orders.Count > 0)
                            {

                                List<OrderInfo> llor_orders;

                                llor_orders = new List<OrderInfo>();

                                foreach (OrderDTO lo_orderTemp in llo_orders)
                                {

                                    OrderInfo loi_oi;

                                    loi_oi = new OrderInfo();
                                    loi_oi.OrderCode = lo_orderTemp.OrderCode.ToString();
                                    loi_oi.OrderDate = lo_orderTemp.OrderDate;
                                    loi_oi.OrderStatus = lo_orderTemp.OrderStatus;
                                    loi_oi.OrderValue = lo_orderTemp.OrderValue;
                                    loi_oi.IdType = lo_orderTemp.IdType.ToString();
                                    loi_oi.IdNumber = lo_orderTemp.IdNumber.ToString();
                                    loi_oi.EventCode = lo_orderTemp.EventCode.ToString();
                                    loi_oi.HotelCode = lo_orderTemp.HotelCode.ToString();
                                    loi_oi.TransportCode = lo_orderTemp.TransportCode.ToString();
                                    llor_orders.Add(loi_oi);

                                }

                                lgor_response.status.CodeResp = "0";
                                lgor_response.status.MessageResp = "";
                                lgor_response.result = llor_orders.ToArray();

                            }
                            else
                            {
                                throw new Exception("No se encontraron ordenes con los datos ingresados");
                            }

                        }
                        else
                        {
                            throw new Exception("Error consultando ordenes");
                        }

                    }
                    else
                    {
                        throw new Exception("Parametros de entrada vacios");
                    }


                }
                else
                {
                    throw new Exception("Parametros de entrada vacios");
                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lgor_response.status.CodeResp = "01";
                lgor_response.status.MessageResp = ae_e.InnerException != null ? "Error en la ejecucion del servicio" : ae_e.Message;
                lgor_response.result = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO OrderService:GetOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;

            }

            return lgor_response;

        }

        public PostOrderRequest PostOrder(PostOrderRequest apor_por)
        {
            throw new System.NotImplementedException();
        }

        public PutOrderResponse PutOrder(PutOrderRequest aprr_prr)
        {
            throw new System.NotImplementedException();
        }

        private void ValidateOrderRequest(object ao_request)
        {

            try
            {





            }
            catch (Exception ae_e)
            {
            }

        }

    }
}