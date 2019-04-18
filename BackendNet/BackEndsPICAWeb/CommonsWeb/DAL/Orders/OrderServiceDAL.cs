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
                    ls_sql += " NVL(H.ID,-1) HOTELID, H.NAME HOTELNAME, H.ROOMNUMBER,";
                    ls_sql += " H.ADDRESS, H.COUNTRY, H.CITY, H.CHECKIN, H.CHECKOUT,";
                    ls_sql += " H.TYPEROOM, H.PRICEROOM, H.COMPANYNAME HCOMPANYNAME,";
                    ls_sql += " T.RESERVATIONCODE TBID, NVL(T.ID,-1) TRANSPORTID,";
                    ls_sql += " T.DEPARTURECOUNTRY, T.ARRIVALCOUNTRY, T.DEPARTURECITY,";
                    ls_sql += " T.ARRIVALCITY, T.SEAT, T.DEPARTDEPARTDATE, T.DEPARTARRIDATE,";
                    ls_sql += " T.ARRIVALDEPARTDATE, T.ARRIVALARRIDATE, T.PRICE,";
                    ls_sql += " T.COMPANYNAME TCOMPANYNAME";

                }

                ls_sql += " FROM ORDERS O";

                if (aod_order.FlagDetail)
                {
                    ls_sql += " LEFT JOIN HOTELRESERVATION H ON (H.IDORDER = O.ORDERCODE)";
                    ls_sql += " LEFT JOIN TRANSPORTRESERVATION T ON (T.IDORDER = O.ORDERCODE),";
                    ls_sql += " CUSTOMER C";
                }
                else
                    ls_sql += ", CUSTOMER C";

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

                            lo_order.EventCode = Convert.ToInt32(ldr_temp["EVENTCODE"]);
                            lo_order.EventName = Convert.ToString(ldr_temp["EVENTNAME"]);
                            lo_order.EventDescription = Convert.ToString(ldr_temp["EVENTDESCRIPTION"]);
                            lo_order.EventDate = Convert.ToDateTime(ldr_temp["EVENTDATE"]);
                            lo_order.EventPrice = Convert.ToDecimal(ldr_temp["EVENTPRICE"]);

                            if (Convert.ToInt32(ldr_temp["HOTELID"]) > 0)
                            {

                                lo_order.Hotel = new HotelReservationDTO();
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
                                lo_order.Hotel.CompanyName = Convert.ToString(ldr_temp["HCOMPANYNAME"]);

                            }

                            if (Convert.ToInt32(ldr_temp["TRANSPORTID"]) > 0)
                            {

                                lo_order.Transport = new TransportReservationDTO();
                                lo_order.Transport.BookingId = Convert.ToString(ldr_temp["TBID"]);
                                lo_order.Transport.Id = Convert.ToInt32(ldr_temp["TRANSPORTID"]);
                                lo_order.Transport.CountryFrom = Convert.ToString(ldr_temp["DEPARTURECOUNTRY"]);
                                lo_order.Transport.CountryTo = Convert.ToString(ldr_temp["ARRIVALCOUNTRY"]);
                                lo_order.Transport.CityFrom = Convert.ToString(ldr_temp["DEPARTURECITY"]);
                                lo_order.Transport.CityTo = Convert.ToString(ldr_temp["ARRIVALCITY"]);
                                lo_order.Transport.Seat = Convert.ToString(ldr_temp["SEAT"]);
                                lo_order.Transport.DepartureDepartDate = Convert.ToDateTime(ldr_temp["DEPARTDEPARTDATE"]);
                                lo_order.Transport.DepartureArrivingDate = Convert.ToDateTime(ldr_temp["DEPARTARRIDATE"]);
                                lo_order.Transport.ReturnDepartDate = Convert.ToDateTime(ldr_temp["ARRIVALDEPARTDATE"]);
                                lo_order.Transport.ReturnArrivingDate = Convert.ToDateTime(ldr_temp["ARRIVALARRIDATE"]);
                                lo_order.Transport.Price = Convert.ToDecimal(ldr_temp["PRICE"]);
                                lo_order.Transport.CompanyName = Convert.ToString(ldr_temp["TCOMPANYNAME"]);

                            }

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

        public long PostOrder(OrderDTO aod_order)
        {

            long ll_orderCode;

            ll_orderCode = 0;

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT NVL(MAX(ORDERCODE),0) + 1 ORDERCODE FROM ORDERS";
                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count == 1)
                {

                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                    {

                        long ll_affected;

                        ll_orderCode = Convert.ToInt32(ldr_temp["ORDERCODE"]);
                        ls_sql = "INSERT INTO ORDERS(ORDERCODE,ORDERDATE,ORDERSTATUS,ORDERVALUE,";
                        ls_sql += "IDCUSTOMER,EVENTCODE,EVENTNAME,EVENTDESCRIPTION,";
                        ls_sql += "EVENTDATE,EVENTPRICE) VALUES(" + ll_orderCode + ",SYSDATE,'A',";
                        ls_sql += aod_order.OrderValue.ToString().Replace(",", ".") + ",";
                        ls_sql += aod_order.IdUser + "," + aod_order.EventCode + ",";
                        ls_sql += "'" + aod_order.EventName + "','" + aod_order.EventDescription + "',";
                        ls_sql += "TO_DATE('" + aod_order.EventDate.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                        ls_sql += aod_order.EventPrice.ToString().Replace(",", ".") + ")";
                        ll_affected = losh_osh.ExecuteSql(ls_sql, new List<OracleParameter>());

                        if (ll_affected > 0)
                        {

                            if (aod_order.Hotel != null)
                            {

                                ls_sql = "INSERT INTO HOTELRESERVATION (RESERVATIONCODE,NAME,ADDRESS,";
                                ls_sql += "COUNTRY,CITY,PHONENUMBER,ROOMNUMBER,TYPEROOM,PRICEROOM,";
                                ls_sql += "CHECKIN,CHECKOUT,IDORDER,COMPANYNAME) VALUES ('" + aod_order.Hotel.BookingId + "',";
                                ls_sql += "'" + aod_order.Hotel.Name + "','" + aod_order.Hotel.Address + "',";
                                ls_sql += "'" + aod_order.Hotel.Country + "','" + aod_order.Hotel.City + "',";
                                ls_sql += "'" + aod_order.Hotel.PhoneNumber + "','" + aod_order.Hotel.RoomNumber + "',";
                                ls_sql += "'" + aod_order.Hotel.TypeRoom + "'," + aod_order.Hotel.PriceRoom.
                                    ToString().Replace(",", ".") + ",";
                                ls_sql += "TO_DATE('" + aod_order.Hotel.CheckIn.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                                ls_sql += "TO_DATE('" + aod_order.Hotel.CheckOut.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                                ls_sql += ll_orderCode + ",'" + aod_order.Hotel.CompanyName + "')";
                                ll_affected = losh_osh.ExecuteSql(ls_sql, new List<OracleParameter>());

                                if (ll_affected <= 0)
                                    Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error,
                                        "ERROR EN LA CAPA DE DATOS OrderService:PostOrder:Error" +
                                        " ingresando reserva hotel para order " + ll_orderCode);

                            }

                            if (aod_order.Transport != null)
                            {

                                ls_sql = "INSERT INTO TRANSPORTRESERVATION (RESERVATIONCODE,NAME,DEPARTURECOUNTRY,";
                                ls_sql += "DEPARTURECITY,ARRIVALCOUNTRY,ARRIVALCITY,DEPARTDEPARTDATE,DEPARTARRIDATE,";
                                ls_sql += "ARRIVALDEPARTDATE,ARRIVALARRIDATE,CLASS,SEAT,PRICE,IDORDER,COMPANYNAME) VALUES (";
                                ls_sql += "'" + aod_order.Transport.BookingId + "','" + aod_order.Transport.Name + "',";
                                ls_sql += "'" + aod_order.Transport.CountryFrom + "','" + aod_order.Transport.CityFrom + "',";
                                ls_sql += "'" + aod_order.Transport.CountryTo + "','" + aod_order.Transport.CityTo + "',";
                                ls_sql += "TO_DATE('" + aod_order.Transport.DepartureDepartDate.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                                ls_sql += "TO_DATE('" + aod_order.Transport.DepartureArrivingDate.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                                ls_sql += "TO_DATE('" + aod_order.Transport.ReturnDepartDate.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                                ls_sql += "TO_DATE('" + aod_order.Transport.ReturnArrivingDate.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                                ls_sql += "'" + aod_order.Transport.Class + "','" + aod_order.Transport.Seat + "',";
                                ls_sql += aod_order.Transport.Price.ToString().Replace(",", ".") + ",";
                                ls_sql += ll_orderCode + ",'" + aod_order.Transport.CompanyName + "')";
                                ll_affected = losh_osh.ExecuteSql(ls_sql, new List<OracleParameter>());

                                if (ll_affected <= 0)
                                    Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error,
                                        "ERROR EN LA CAPA DE DATOS OrderService:PostOrder:Error" +
                                        " ingresando reserva transporte para order " + ll_orderCode);


                            }

                        }
                        else
                            throw new Exception("Error ingresando orden");

                    }

                }
                else
                    throw new Exception("Error consultando id de la orden");

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                ll_orderCode = -1;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:PostOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return ll_orderCode;

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