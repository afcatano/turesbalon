using CommonsWeb.DAL.Orders;
using CommonsWeb.DTO;
using CommonsWeb.Util;
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
                        lo_order.FlagDetail = agor_gor.Order.FlagDetail;

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

                        if (agor_gor.Order.OrderDateStart != null && agor_gor.Order.OrderDateStart != new DateTime())
                            lo_order.OrderDateFrom = agor_gor.Order.OrderDateStart;

                        if (agor_gor.Order.OrderDateEnd != null && agor_gor.Order.OrderDateEnd != new DateTime())
                            lo_order.OrderDateTo = agor_gor.Order.OrderDateEnd;

                        if (lo_order.OrderDateFrom != null && lo_order.OrderDateFrom != new DateTime() &&
                            lo_order.OrderDateTo != null && lo_order.OrderDateTo != new DateTime())
                            if (lo_order.OrderDateTo < lo_order.OrderDateFrom)
                                throw new Exception("La fecha incial de la orden no puede ser mayor a la fecha final de la orden");

                        if (agor_gor.Order.OrderStatus != null)
                            if (agor_gor.Order.OrderStatus.Trim().Length > 0)
                                lo_order.OrderStatus = agor_gor.Order.OrderStatus;

                        if (agor_gor.Order.OrderValue > 0)
                            lo_order.OrderValue = agor_gor.Order.OrderValue;

                        if (agor_gor.Order.IdUser > 0)
                            lo_order.IdUser = agor_gor.Order.IdUser;

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

                        }

                        if (agor_gor.Order.IdNumber > 0)
                            lo_order.IdNumber = agor_gor.Order.IdNumber;

                        if (lo_order.FlagDetail)
                        {

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

                        }

                        CacheHandler lch_cache;
                        Dictionary<OrderDTO, GetOrderResponse> ld_data;
                        GetOrderResponse lgor_tempResponse;
                        bool lb_valCache;

                        lch_cache = new CacheHandler();
                        ld_data = (Dictionary<OrderDTO, GetOrderResponse>)lch_cache.GetCache("GetOrder");
                        lgor_tempResponse = null;
                        lb_valCache = false;

                        if (ld_data != null)
                        {

                            foreach (OrderDTO lo_key in ld_data.Keys)
                            {

                                if (lo_key.Equals(lo_order))
                                {

                                    lb_valCache = ld_data.TryGetValue(lo_key, out lgor_tempResponse);
                                    break;

                                }

                            }

                        }

                        if (lb_valCache)
                            lgor_response = lgor_tempResponse;
                        else
                        {

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
                                        loi_oi.OrderDate = lo_orderTemp.OrderDateFrom;
                                        loi_oi.OrderStatus = lo_orderTemp.OrderStatus;
                                        loi_oi.OrderValue = lo_orderTemp.OrderValue;
                                        loi_oi.IdUser = lo_orderTemp.IdUser;
                                        loi_oi.IdType = lo_orderTemp.IdType.ToString();
                                        loi_oi.IdNumber = lo_orderTemp.IdNumber;

                                        if (lo_order.FlagDetail)
                                        {

                                            loi_oi.Event = new GetEvent();
                                            loi_oi.Hotel = new GetHotel();
                                            loi_oi.Transport = new GetTransport();
                                            loi_oi.Event.EventCode = lo_orderTemp.EventCode.ToString();
                                            loi_oi.Event.Name = lo_orderTemp.EventName;
                                            loi_oi.Event.Description = lo_orderTemp.EventDescription;
                                            loi_oi.Event.Date = lo_orderTemp.EventDate;
                                            loi_oi.Event.Value = lo_orderTemp.EventPrice;
                                            loi_oi.Hotel.BookingId = lo_orderTemp.Hotel.BookingId;
                                            loi_oi.Hotel.HotelCode = lo_orderTemp.Hotel.Id.ToString();
                                            loi_oi.Hotel.Name = lo_orderTemp.Hotel.Name;
                                            loi_oi.Hotel.RoomNumber = lo_orderTemp.Hotel.RoomNumber;
                                            loi_oi.Hotel.Address = lo_orderTemp.Hotel.Address;
                                            loi_oi.Hotel.Country = lo_orderTemp.Hotel.Country;
                                            loi_oi.Hotel.City = lo_orderTemp.Hotel.City;
                                            loi_oi.Hotel.Checkin = lo_orderTemp.Hotel.CheckIn;
                                            loi_oi.Hotel.Checkout = lo_orderTemp.Hotel.CheckOut;
                                            loi_oi.Hotel.Type = lo_orderTemp.Hotel.TypeRoom;
                                            loi_oi.Hotel.Value = lo_orderTemp.Hotel.PriceRoom;
                                            loi_oi.Transport.BookingId = lo_orderTemp.Transport.BookingId;
                                            loi_oi.Transport.TransportCode = lo_orderTemp.Transport.Id.ToString();
                                            loi_oi.Transport.CountryFrom = lo_orderTemp.Transport.CountryFrom;
                                            loi_oi.Transport.CountryTo = lo_orderTemp.Transport.CountryTo;
                                            loi_oi.Transport.CityFrom = lo_orderTemp.Transport.CityFrom;
                                            loi_oi.Transport.CityTo = lo_orderTemp.Transport.CityTo;
                                            loi_oi.Transport.Chairs = lo_orderTemp.Transport.Seat;
                                            loi_oi.Transport.DepartDate = lo_orderTemp.Transport.DepartDate;
                                            loi_oi.Transport.ArrivingDate = lo_orderTemp.Transport.ArrivingDate;
                                            loi_oi.Transport.Value = lo_orderTemp.Transport.Price;

                                        }

                                        llor_orders.Add(loi_oi);

                                    }

                                    lgor_response.status.CodeResp = "0";
                                    lgor_response.status.MessageResp = "";
                                    lgor_response.result = llor_orders.ToArray();

                                    if (ld_data == null)
                                        ld_data = new Dictionary<OrderDTO, GetOrderResponse>();

                                    ld_data.Add(lo_order, lgor_response);
                                    lch_cache.AddCache("GetOrder", ld_data);

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