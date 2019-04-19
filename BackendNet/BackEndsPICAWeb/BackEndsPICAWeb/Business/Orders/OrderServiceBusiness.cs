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

                            if (agor_gor.Order.HotelCompanyName != null)
                                if (agor_gor.Order.HotelCompanyName.Trim().Length > 0)
                                    lo_order.HotelCompanyName = agor_gor.Order.HotelCompanyName;

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

                            if (agor_gor.Order.TransportCompanyName != null)
                                if (agor_gor.Order.TransportCompanyName.Trim().Length > 0)
                                    lo_order.TransportCompanyName = agor_gor.Order.TransportCompanyName;

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
                                            loi_oi.Event.EventCode = lo_orderTemp.EventCode.ToString();
                                            loi_oi.Event.Name = lo_orderTemp.EventName;
                                            loi_oi.Event.Description = lo_orderTemp.EventDescription;
                                            loi_oi.Event.Date = lo_orderTemp.EventDate;
                                            loi_oi.Event.Value = lo_orderTemp.EventPrice;
                                            loi_oi.Event.Cantidad = lo_orderTemp.EventUnit.ToString();

                                            if (lo_orderTemp.Hotel != null)
                                            {

                                                loi_oi.Hotel = new GetHotel();
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
                                                loi_oi.Hotel.CompanyName = lo_orderTemp.Hotel.CompanyName;
                                                loi_oi.Hotel.Cantidad = lo_orderTemp.Hotel.Guests.ToString();

                                            }

                                            if (lo_orderTemp.Transport != null)
                                            {

                                                loi_oi.Transport = new GetTransport();
                                                loi_oi.Transport.BookingId = lo_orderTemp.Transport.BookingId;
                                                loi_oi.Transport.TransportCode = lo_orderTemp.Transport.Id.ToString();
                                                loi_oi.Transport.CountryFrom = lo_orderTemp.Transport.CountryFrom;
                                                loi_oi.Transport.CountryTo = lo_orderTemp.Transport.CountryTo;
                                                loi_oi.Transport.CityFrom = lo_orderTemp.Transport.CityFrom;
                                                loi_oi.Transport.CityTo = lo_orderTemp.Transport.CityTo;
                                                loi_oi.Transport.Chairs = lo_orderTemp.Transport.Seat;
                                                loi_oi.Transport.DepartureDepartDate = lo_orderTemp.Transport.DepartureDepartDate;
                                                loi_oi.Transport.DepartureArrivingDate = lo_orderTemp.Transport.DepartureArrivingDate;
                                                loi_oi.Transport.ReturnDepartDate = lo_orderTemp.Transport.ReturnDepartDate;
                                                loi_oi.Transport.ReturnArrivingDate = lo_orderTemp.Transport.ReturnArrivingDate;
                                                loi_oi.Transport.Value = lo_orderTemp.Transport.Price;
                                                loi_oi.Transport.CompanyName = lo_orderTemp.Transport.CompanyName;

                                            }

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

        public PostOrderResponse PostOrder(PostOrderRequest apor_por)
        {

            PostOrderResponse lpor_response;

            lpor_response = new PostOrderResponse();
            lpor_response.status = new Status();

            try
            {

                if (apor_por != null)
                {

                    if (apor_por.Order != null)
                    {

                        OrderDTO lo_order;
                        OrderServiceDAL losd_losDAL;

                        lo_order = new OrderDTO();
                        losd_losDAL = new OrderServiceDAL();

                        if (apor_por.Order.IdUser > 0)
                            lo_order.IdUser = apor_por.Order.IdUser;
                        else
                            throw new Exception("El codigo del usuario es obligatorio");

                        if (apor_por.Order.TotalValue > 0)
                            lo_order.OrderValue = apor_por.Order.TotalValue;
                        else
                            throw new Exception("El valor de la orden es obligatorio y debe ser mayor a 0");

                        if (apor_por.Order.Event != null)
                        {

                            if (apor_por.Order.Event.EventCode != null)
                            {

                                if (apor_por.Order.Event.EventCode.Trim().Length > 0)
                                {

                                    long ll_result;

                                    ll_result = 0;

                                    if (!long.TryParse(apor_por.Order.Event.EventCode, out ll_result))
                                        throw new Exception("El codigo del evento debe ser numerico");
                                    else if (long.Parse(apor_por.Order.Event.EventCode) <= 0)
                                        throw new Exception("El codigo del evento debe ser mayor a 0");
                                    else
                                        lo_order.EventCode = long.Parse(apor_por.Order.Event.EventCode);

                                }
                                else
                                    throw new Exception("El codigo del evento es obligatorio");

                            }
                            else
                                throw new Exception("El codigo del evento es obligatorio");

                            if (apor_por.Order.Event.Name != null)
                            {

                                if (apor_por.Order.Event.Name.Trim().Length > 0)
                                    lo_order.EventName = apor_por.Order.Event.Name;
                                else
                                    throw new Exception("El nombre del evento es obligatorio");

                            }
                            else
                                throw new Exception("El nombre del evento es obligatorio");

                            if (apor_por.Order.Event.Description != null)
                            {

                                if (apor_por.Order.Event.Description.Trim().Length > 0)
                                    lo_order.EventDescription = apor_por.Order.Event.Description;
                                else
                                    throw new Exception("La descripcion del evento es obligatoria");

                            }
                            else
                                throw new Exception("La descripcion del evento es obligatoria");

                            if (apor_por.Order.Event.Date != null)
                                if (apor_por.Order.Event.Date != new DateTime())
                                    lo_order.OrderDateFrom = apor_por.Order.Event.Date;
                                else
                                    throw new Exception("La fecha del evento es obligatoria");
                            else
                                throw new Exception("La fecha del evento es obligatoria");

                            if (apor_por.Order.Event.Value > 0)
                                lo_order.IdUser = apor_por.Order.IdUser;
                            else
                                throw new Exception("El valor del evento es obligatorio y debe ser mayor a 0");

                        }
                        else
                            throw new Exception("El evento es obligatorio");

                        if (apor_por.Order.Hotel != null)
                        {

                            lo_order.Hotel = new HotelReservationDTO();

                            if (apor_por.Order.Hotel.BookingId != null)
                                if (apor_por.Order.Hotel.BookingId.Trim().Length > 0)
                                    lo_order.Hotel.BookingId = apor_por.Order.Hotel.BookingId;
                                else
                                    throw new Exception("El codigo de la reserva del hotel es obligatorio");
                            else
                                throw new Exception("El codigo de la reserva del hotel es obligatorio");

                            if (apor_por.Order.Hotel.Name != null)
                                if (apor_por.Order.Hotel.Name.Trim().Length > 0)
                                    lo_order.Hotel.Name = apor_por.Order.Hotel.Name;
                                else
                                    throw new Exception("El nombre del hotel es obligatorio");
                            else
                                throw new Exception("El nombre del hotel es obligatorio");

                            if (apor_por.Order.Hotel.RoomNumber != null)
                                if (apor_por.Order.Hotel.RoomNumber.Trim().Length > 0)
                                    lo_order.Hotel.RoomNumber = apor_por.Order.Hotel.RoomNumber;
                                else
                                    throw new Exception("El numero de habitacion del hotel es obligatorio");
                            else
                                throw new Exception("El numero de habitacion del hotel es obligatorio");

                            if (apor_por.Order.Hotel.Address != null)
                                if (apor_por.Order.Hotel.Address.Trim().Length > 0)
                                    lo_order.Hotel.Address = apor_por.Order.Hotel.Address;
                                else
                                    throw new Exception("La direccion del hotel es obligatoria");
                            else
                                throw new Exception("La direccion del hotel es obligatoria");

                            if (apor_por.Order.Hotel.Country != null)
                                if (apor_por.Order.Hotel.Country.Trim().Length > 0)
                                    lo_order.Hotel.Country = apor_por.Order.Hotel.Country;
                                else
                                    throw new Exception("El pais del hotel es obligatorio");
                            else
                                throw new Exception("El pais del hotel es obligatorio");

                            if (apor_por.Order.Hotel.City != null)
                                if (apor_por.Order.Hotel.City.Trim().Length > 0)
                                    lo_order.Hotel.City = apor_por.Order.Hotel.City;
                                else
                                    throw new Exception("La ciudad del hotel es obligatoria");
                            else
                                throw new Exception("La ciudad del hotel es obligatoria");

                            if (apor_por.Order.Hotel.Checkin != null)
                                if (apor_por.Order.Hotel.Checkin != new DateTime())
                                    lo_order.Hotel.CheckIn = apor_por.Order.Hotel.Checkin;
                                else
                                    throw new Exception("El checkin del hotel es obligatorio");
                            else
                                throw new Exception("El checkin del hotel es obligatorio");

                            if (apor_por.Order.Hotel.Checkout != null)
                                if (apor_por.Order.Hotel.Checkout != new DateTime())
                                    lo_order.Hotel.CheckOut = apor_por.Order.Hotel.Checkout;
                                else
                                    throw new Exception("El checkout del hotel es obligatorio");
                            else
                                throw new Exception("El checkout del hotel es obligatorio");

                            if (apor_por.Order.Hotel.Type != null)
                                if (apor_por.Order.Hotel.Type.Trim().Length > 0)
                                    lo_order.Hotel.TypeRoom = apor_por.Order.Hotel.Type;
                                else
                                    throw new Exception("El tipo de habitacion del hotel es obligatorio");
                            else
                                throw new Exception("El tipo de habitacion del hotel es obligatorio");

                            if (apor_por.Order.Hotel.Value > 0)
                                lo_order.Hotel.PriceRoom = apor_por.Order.Hotel.Value;
                            else
                                throw new Exception("El valor de la reserva hotelera es obligatorio y debe ser mayor a 0");

                            if (apor_por.Order.Hotel.CompanyName != null)
                                if (apor_por.Order.Hotel.CompanyName.Trim().Length > 0)
                                    lo_order.Hotel.CompanyName = apor_por.Order.Hotel.CompanyName;
                                else
                                    throw new Exception("El nombre de la compañia del hotel es obligatorio");
                            else
                                throw new Exception("El nombre de la compañia del hotel es obligatorio");

                        }

                        if (apor_por.Order.Transport != null)
                        {

                            lo_order.Transport = new TransportReservationDTO();

                            if (apor_por.Order.Transport.BookingId != null)
                                if (apor_por.Order.Transport.BookingId.Trim().Length > 0)
                                    lo_order.Transport.BookingId = apor_por.Order.Transport.BookingId;
                                else
                                    throw new Exception("El codigo de la reserva del transporte es obligatorio");
                            else
                                throw new Exception("El codigo de la reserva del transporte es obligatorio");

                            if (apor_por.Order.Transport.CountryFrom != null)
                                if (apor_por.Order.Transport.CountryFrom.Trim().Length > 0)
                                    lo_order.Transport.CountryFrom = apor_por.Order.Transport.CountryFrom;
                                else
                                    throw new Exception("El pais de origen del transporte es obligatorio");
                            else
                                throw new Exception("El pais de origen del transporte es obligatorio");

                            if (apor_por.Order.Transport.CountryTo != null)
                                if (apor_por.Order.Transport.CountryTo.Trim().Length > 0)
                                    lo_order.Transport.CountryTo = apor_por.Order.Transport.CountryTo;
                                else
                                    throw new Exception("El pais de llegada del transporte es obligatorio");
                            else
                                throw new Exception("El pais de llegada del transporte es obligatorio");

                            if (apor_por.Order.Transport.CityFrom != null)
                                if (apor_por.Order.Transport.CityFrom.Trim().Length > 0)
                                    lo_order.Transport.CityFrom = apor_por.Order.Transport.CityFrom;
                                else
                                    throw new Exception("La ciudad de origen del transporte es obligatoria");
                            else
                                throw new Exception("La ciudad de origen del transporte es obligatoria");

                            if (apor_por.Order.Transport.CityTo != null)
                                if (apor_por.Order.Transport.CityTo.Trim().Length > 0)
                                    lo_order.Transport.CityTo = apor_por.Order.Transport.CityTo;
                                else
                                    throw new Exception("La ciudad de llegada del transporte es obligatoria");
                            else
                                throw new Exception("La ciudad de llegada del transporte es obligatoria");

                            if (apor_por.Order.Transport.Chairs != null)
                            {

                                if (apor_por.Order.Transport.Chairs.Trim().Length > 0)
                                {

                                    long ll_result;

                                    ll_result = 0;

                                    if (!long.TryParse(apor_por.Order.Transport.Chairs, out ll_result))
                                        throw new Exception("El numero de sillas de la reserva de transporte debe ser numerico");
                                    else if (long.Parse(apor_por.Order.Transport.Chairs) <= 0)
                                        throw new Exception("El numero de sillas de la reserva de transporte debe ser mayor a 0");
                                    else
                                        lo_order.Transport.Seat = apor_por.Order.Transport.Chairs;

                                }
                                else
                                    throw new Exception("El numero de sillas de la reserva de transporte es oblogatorio");

                            }
                            else
                                throw new Exception("El numero de sillas de la reserva de transporte es oblogatorio");


                            if (apor_por.Order.Transport.DepartureDepartDate != null)
                                if (apor_por.Order.Transport.DepartureDepartDate != new DateTime())
                                    lo_order.Transport.DepartureDepartDate = apor_por.Order.Transport.DepartureDepartDate;
                                else
                                    throw new Exception("La fecha de salida de ida de la reserva de transporte es obligatoria");
                            else
                                throw new Exception("La fecha de salida de ida de la reserva de transporte es obligatoria");

                            if (apor_por.Order.Transport.DepartureArrivingDate != null)
                                if (apor_por.Order.Transport.DepartureArrivingDate != new DateTime())
                                    lo_order.Transport.DepartureArrivingDate = apor_por.Order.Transport.DepartureArrivingDate;
                                else
                                    throw new Exception("La fecha de llegada de ida de la reserva de transporte es obligatoria");
                            else
                                throw new Exception("La fecha de llegada de ida de la reserva de transporte es obligatoria");

                            if (lo_order.Transport.DepartureArrivingDate < lo_order.Transport.DepartureDepartDate)
                                throw new Exception("La fecha de llegada de ida de la reserva de transporte no puede ser menor a la fecha de salida de ida");

                            if (apor_por.Order.Transport.ReturnDepartDate != null)
                                if (apor_por.Order.Transport.ReturnDepartDate != new DateTime())
                                    lo_order.Transport.ReturnDepartDate = apor_por.Order.Transport.ReturnDepartDate;
                                else
                                    throw new Exception("La fecha de salida de regreso de la reserva de transporte es obligatoria");
                            else
                                throw new Exception("La fecha de salida de regreso de la reserva de transporte es obligatoria");

                            if (apor_por.Order.Transport.ReturnArrivingDate != null)
                                if (apor_por.Order.Transport.ReturnArrivingDate != new DateTime())
                                    lo_order.Transport.ReturnArrivingDate = apor_por.Order.Transport.ReturnArrivingDate;
                                else
                                    throw new Exception("La fecha de llegada de regreso de la reserva de transporte es obligatoria");
                            else
                                throw new Exception("La fecha de llegada de regreso de la reserva de transporte es obligatoria");

                            if (lo_order.Transport.ReturnArrivingDate < lo_order.Transport.ReturnDepartDate)
                                throw new Exception("La fecha de llegada de regreso de la reserva de transporte no puede ser menor a la fecha de salida de regreso");

                            if (apor_por.Order.Transport.Value > 0)
                                lo_order.Transport.Price = apor_por.Order.Transport.Value;
                            else
                                throw new Exception("El valor de la reserva hotelera es obligatorio y debe ser mayor a 0");

                            if (apor_por.Order.Transport.CompanyName != null)
                                if (apor_por.Order.Transport.CompanyName.Trim().Length > 0)
                                    lo_order.Transport.CompanyName = apor_por.Order.Transport.CompanyName;
                                else
                                    throw new Exception("El nombre de la compañia del transporte es obligatorio");
                            else
                                throw new Exception("El nombre de la compañia del transporte es obligatorio");

                        }

                        lo_order.OrderCode = losd_losDAL.PostOrder(lo_order);

                        if (lo_order.OrderCode > 0)
                        {

                            CacheHandler lch_cache;
                            Dictionary<OrderDTO, GetOrderResponse> ld_data;

                            lch_cache = new CacheHandler();
                            ld_data = (Dictionary<OrderDTO, GetOrderResponse>)lch_cache.GetCache("GetOrder");

                            if (ld_data != null)
                                lch_cache.RemoveCache("GetOrder");

                            lpor_response.status.CodeResp = "0";
                            lpor_response.status.MessageResp = "";
                            lpor_response.result = new RegOrder();
                            lpor_response.result.OrderCode = lo_order.OrderCode.ToString();
                            lpor_response.result.OrderDate = DateTime.Now;
                            lpor_response.result.OrderStatus = "A";
                            lpor_response.result.OrderValue = lo_order.OrderValue;

                        }
                        else
                            throw new Exception("Error ingresando orden");

                    }
                    else
                        throw new Exception("Parametros de entrada vacios");

                }
                else
                    throw new Exception("Parametros de entrada vacios");

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lpor_response.status.CodeResp = "01";
                lpor_response.status.MessageResp = ae_e.InnerException != null ? "Error en la ejecucion del servicio" : ae_e.Message;
                lpor_response.result = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO OrderService:PostOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;

            }

            return lpor_response;

        }

        public PutOrderResponse PutOrder(PutOrderRequest aprr_prr)
        {

            PutOrderResponse lpor_response;

            lpor_response = new PutOrderResponse();
            lpor_response.status = new Status();

            try
            {

                if (aprr_prr != null)
                {

                    if (aprr_prr.Order != null)
                    {

                        OrderDTO lo_order;
                        OrderServiceDAL losd_losDAL;

                        lo_order = new OrderDTO();
                        losd_losDAL = new OrderServiceDAL();

                        if (aprr_prr.Order.OrderCode != null)
                        {

                            if (aprr_prr.Order.OrderCode.Trim().Length > 0)
                            {

                                long ll_result;

                                ll_result = 0;

                                if (!long.TryParse(aprr_prr.Order.OrderCode, out ll_result))
                                    throw new Exception("El numero de la orden debe ser numerico");
                                else
                                    lo_order.OrderCode = long.Parse(aprr_prr.Order.OrderCode);

                            }
                            else
                                throw new Exception("El numero de la orden es obligatorio");

                        }
                        else
                            throw new Exception("El numero de la orden es obligatorio");

                        if (aprr_prr.Order.Status != null)
                        {

                            if (aprr_prr.Order.Status.Trim().Length > 0)
                                lo_order.OrderStatus = aprr_prr.Order.Status;
                            else
                                throw new Exception("El estado de la orden es obligatorio");

                        }
                        else
                            throw new Exception("El estado de la orden es obligatorio");

                        if (losd_losDAL.PutOrder(lo_order))
                        {

                            CacheHandler lch_cache;
                            Dictionary<OrderDTO, GetOrderResponse> ld_data;

                            lch_cache = new CacheHandler();
                            ld_data = (Dictionary<OrderDTO, GetOrderResponse>)lch_cache.GetCache("GetOrder");

                            if (ld_data != null)
                                lch_cache.RemoveCache("GetOrder");

                            lpor_response.status.CodeResp = "0";
                            lpor_response.status.MessageResp = "";

                        }
                        else
                            throw new Exception("Error actualizando orden");

                    }
                    else
                        throw new Exception("Parametros de entrada vacios");

                }
                else
                    throw new Exception("Parametros de entrada vacios");

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lpor_response.status.CodeResp = "01";
                lpor_response.status.MessageResp = ae_e.InnerException != null ? "Error en la ejecucion del servicio" : ae_e.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO OrderService:PutOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;

            }

            return lpor_response;

        }

    }
}