using CommonsWeb.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;

namespace CommonsWeb.DAL.Orders
{
    public class OrderServiceDAL
    {

        public List<OrderDTO> GetOrder(OrderDTO aod_order)
        {

            List<OrderDTO> llo_orders;

            llo_orders = new List<OrderDTO>();

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT O.ORDERCODE, O.ORDERDATE, O.ORDERSTATUS,";
                ls_sql += " O.ORDERVALUE, C.ID IDUSER, C.CUSTID, C.IDTYPEIDENT";

                if (aod_order.FlagDetail)
                {

                    ls_sql += ", O.EVENTCODE, O.EVENTNAME, O.EVENTDESCRIPTION,";
                    ls_sql += " O.EVENTDATE, O.EVENTPRICE, H.RESERVATIONCODE HBID,";
                    ls_sql += " H.ID HOTELID, H.NAME HOTELNAME, H.ROOMNUMBER,";
                    ls_sql += " H.ADDRESS, H.COUNTRY, H.CITY, H.CHECKIN, H.CHECKOUT,";
                    ls_sql += " H.TYPEROOM, H.PRICEROOM, T.RESERVATIONCODE TBID,";
                    ls_sql += " T.ID TRANSPORTID, T.DEPARTURECOUNTRY, T.ARRIVALCOUNTRY,";
                    ls_sql += " T.DEPARTURECITY, T.ARRIVALCITY, T.SEAT, T.DEPARTUREDATE,";
                    ls_sql += " T.ARRIVALDATE, T.PRICE";

                }

                ls_sql += " FROM ORDERS O, CUSTOMER C";

                if (aod_order.FlagDetail)
                    ls_sql += ", HOTELRESERVATION H, TRANSPORTRESERVATION T";

                ls_sql += " WHERE C.ID = O.IDCUSTOMER";

                if (aod_order.OrderCode > 0)
                    ls_sql += " AND O.ORDERCODE = " + aod_order.OrderCode;

                if (aod_order.OrderDateFrom != null && aod_order.OrderDateFrom != new DateTime() &&
                    aod_order.OrderDateTo != null && aod_order.OrderDateTo != new DateTime())
                    ls_sql += " AND O.ORDERDATE BETWEEN TO_DATE('" +
                        aod_order.OrderDateFrom.ToString("yyyy/MM/dd") +
                        "','YYYY/MM/DD') AND TO_DATE('" +
                        aod_order.OrderDateTo.AddDays(1).ToString("yyyy/MM/dd") + "','YYYY/MM/DD')";
                else if (aod_order.OrderDateFrom != null && aod_order.OrderDateFrom != new DateTime())
                    ls_sql += " AND O.ORDERDATE >= TO_DATE('" +
                        aod_order.OrderDateFrom.ToString("yyyy/MM/dd") + "','YYYY/MM/DD')";
                else if (aod_order.OrderDateTo != null && aod_order.OrderDateTo != new DateTime())
                    ls_sql += " AND O.ORDERDATE <= TO_DATE('" +
                        aod_order.OrderDateTo.AddDays(1).ToString("yyyy/MM/dd") + "','YYYY/MM/DD')";

                if (aod_order.OrderStatus != null)
                    if (aod_order.OrderStatus.Trim().Length > 0)
                        ls_sql += " AND O.ORDERSTATUS = '" + aod_order.OrderStatus + "'";

                if (aod_order.OrderValue > 0)
                    ls_sql += " AND O.ORDERVALUE = " + aod_order.OrderValue.ToString().Replace(",", ".");

                if (aod_order.IdUser > 0)
                    ls_sql += " AND O.IDCUSTOMER = " + aod_order.IdUser;

                if (aod_order.IdNumber > 0)
                    ls_sql += " AND C.CUSTID = " + aod_order.IdNumber;

                if (aod_order.IdType > 0)
                    ls_sql += " AND C.IDTYPEIDENT = " + aod_order.IdType;

                if (aod_order.FlagDetail)
                {

                    ls_sql += " AND H.IDORDER = O.ORDERCODE AND T.IDORDER = O.ORDERCODE";

                    if (aod_order.EventCode > 0)
                        ls_sql += " AND O.EVENTCODE = " + aod_order.EventCode.ToString();

                    if (aod_order.HotelCode > 0)
                        ls_sql += " AND H.ID = " + aod_order.HotelCode.ToString();

                    if (aod_order.TransportCode > 0)
                        ls_sql += " AND O.ID = " + aod_order.TransportCode.ToString();

                }

                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                    {

                        OrderDTO lo_order;

                        lo_order = new OrderDTO();
                        lo_order.OrderCode = Convert.ToInt32(ldr_temp["ORDERCODE"]);
                        lo_order.OrderDateFrom = Convert.ToDateTime(ldr_temp["ORDERDATE"]);
                        lo_order.OrderStatus = Convert.ToString(ldr_temp["ORDERSTATUS"]);
                        lo_order.OrderValue = Convert.ToDecimal(ldr_temp["ORDERVALUE"]);
                        lo_order.IdUser = Convert.ToInt32(ldr_temp["IDUSER"]);
                        lo_order.IdNumber = Convert.ToInt32(ldr_temp["CUSTID"]);
                        lo_order.IdType = Convert.ToInt32(ldr_temp["IDTYPEIDENT"]);

                        if (aod_order.FlagDetail)
                        {

                            lo_order.Hotel = new HotelReservationDTO();
                            lo_order.Transport = new TransportReservationDTO();
                            lo_order.EventCode = Convert.ToInt32(ldr_temp["EVENTCODE"]);
                            lo_order.EventName = Convert.ToString(ldr_temp["EVENTNAME"]);
                            lo_order.EventDescription = Convert.ToString(ldr_temp["EVENTDESCRIPTION"]);
                            lo_order.EventDate = Convert.ToDateTime(ldr_temp["EVENTDATE"]);
                            lo_order.EventPrice = Convert.ToDecimal(ldr_temp["EVENTPRICE"]);
                            lo_order.Hotel.BookingId = Convert.ToString(ldr_temp["HBID"]);
                            lo_order.Hotel.Id = Convert.ToInt32(ldr_temp["HOTELID"]);
                            lo_order.Hotel.Name = Convert.ToString(ldr_temp["HOTELNAME"]);
                            lo_order.Hotel.RoomNumber = Convert.ToString(ldr_temp["ROOMNUMBER"]);
                            lo_order.Hotel.Address = Convert.ToString(ldr_temp["ADDRESS"]);
                            lo_order.Hotel.Country = Convert.ToString(ldr_temp["COUNTRY"]);
                            lo_order.Hotel.City = Convert.ToString(ldr_temp["CITY"]);
                            lo_order.Hotel.CheckIn = Convert.ToDateTime(ldr_temp["CHECKIN"]);
                            lo_order.Hotel.CheckOut = Convert.ToDateTime(ldr_temp["CHECKOUT"]);
                            lo_order.Hotel.TypeRoom = Convert.ToString(ldr_temp["TYPEROOM"]);
                            lo_order.Hotel.PriceRoom = Convert.ToDecimal(ldr_temp["PRICEROOM"]);
                            lo_order.Transport.BookingId = Convert.ToString(ldr_temp["TBID"]);
                            lo_order.Transport.Id = Convert.ToInt32(ldr_temp["TRANSPORTID"]);
                            lo_order.Transport.CountryFrom = Convert.ToString(ldr_temp["DEPARTURECOUNTRY"]);
                            lo_order.Transport.CountryTo = Convert.ToString(ldr_temp["ARRIVALCOUNTRY"]);
                            lo_order.Transport.CityFrom = Convert.ToString(ldr_temp["DEPARTURECITY"]);
                            lo_order.Transport.CityTo = Convert.ToString(ldr_temp["ARRIVALCITY"]);
                            lo_order.Transport.Seat = Convert.ToString(ldr_temp["SEAT"]);
                            lo_order.Transport.DepartDate = Convert.ToDateTime(ldr_temp["DEPARTUREDATE"]);
                            lo_order.Transport.ArrivingDate = Convert.ToDateTime(ldr_temp["ARRIVALDATE"]);
                            lo_order.Transport.Price = Convert.ToDecimal(ldr_temp["PRICE"]);

                        }

                        llo_orders.Add(lo_order);

                    }

                }
                else
                {
                    throw new Exception("Error consultando ordenes");
                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llo_orders = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return llo_orders;

        }

        public bool PutOrder(OrderDTO aod_order)
        {

            bool lb_retorno;

            lb_retorno = false;

            try
            {

                string ls_sql;
                long ll_affected;
                OracleServerHelper losh_conection;

                ls_sql = "UPDATE ORDERS SET ORDERSTATUS = '" + aod_order.OrderStatus + "'";
                ls_sql += " WHERE ORDERCODE = " + aod_order.OrderCode;
                losh_conection = new OracleServerHelper();
                ll_affected = losh_conection.ExecuteSql(ls_sql, new List<OracleParameter>());

                if (ll_affected > 0)
                    lb_retorno = true;
                
            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lb_retorno = false;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:PutOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return lb_retorno;

        }

    }
}