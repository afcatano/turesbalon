using CommonsWeb.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;

namespace CommonsWeb.DAL.Orders
{
    public class OrderServiceDAL
    {

        public List<OrderDTO> GetTopEvents(long al_top)
        {

            List<OrderDTO> llo_orders;

            llo_orders = new List<OrderDTO>();

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT X.* FROM (SELECT EVENTCODE, COUNT(0) CANT FROM ORDERS";
                ls_sql += " GROUP BY EVENTCODE ORDER BY CANT DESC) X WHERE ROWNUM";
                ls_sql += " BETWEEN 1 AND " + al_top.ToString();
                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                    {

                        OrderDTO lo_order;

                        lo_order = new OrderDTO();
                        lo_order.EventCode = Convert.ToInt32(ldr_temp["EVENTCODE"]);
                        lo_order.EventUnit = Convert.ToInt32(ldr_temp["CANT"]);
                        llo_orders.Add(lo_order);

                    }

                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llo_orders = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetTopOrder");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return llo_orders;

        }

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
                    ls_sql += " O.EVENTDATE, O.EVENTPRICE, O.EVENTUNIT,";
                    ls_sql += " H.RESERVATIONCODE HBID, NVL(H.ID,-1) HOTELID,";
                    ls_sql += " H.NAME HOTELNAME, H.ROOMNUMBER, H.ADDRESS,";
                    ls_sql += " H.COUNTRY, H.CITY, H.CHECKIN, H.CHECKOUT, H.TYPEROOM,";
                    ls_sql += " H.PRICEROOM, H.COMPANYNAME HCOMPANYNAME, H.GUESTS,";
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

                    if (aod_order.HotelCompanyName != null)
                        if (aod_order.HotelCompanyName.Trim().Length > 0)
                            ls_sql += " AND H.COMPANYNAME = '" + aod_order.HotelCompanyName + "'";

                    if (aod_order.TransportCode > 0)
                        ls_sql += " AND T.ID = " + aod_order.TransportCode.ToString();

                    if (aod_order.TransportCompanyName != null)
                        if (aod_order.TransportCompanyName.Trim().Length > 0)
                            ls_sql += " AND T.COMPANYNAME = '" + aod_order.TransportCompanyName + "'";

                }

                ls_sql += " ORDER BY O.ORDERCODE DESC";
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
                            lo_order.EventUnit = Convert.ToInt32(ldr_temp["EVENTUNIT"]);

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
                                lo_order.Hotel.Guests = Convert.ToInt32(ldr_temp["GUESTS"]);

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

        public List<OrderDTO> GetPagedOrders(OrderDTO aod_order)
        {

            List<OrderDTO> llo_orders;

            llo_orders = new List<OrderDTO>();

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT B.* FROM (SELECT ROWNUM REGISTRO, A.* FROM (";
                ls_sql += " SELECT O.ORDERCODE, O.ORDERDATE, O.ORDERSTATUS,";
                ls_sql += " O.ORDERVALUE, C.ID IDUSER, C.CUSTID, C.IDTYPEIDENT";

                if (aod_order.FlagDetail)
                {

                    ls_sql += ", O.EVENTCODE, O.EVENTNAME, O.EVENTDESCRIPTION,";
                    ls_sql += " O.EVENTDATE, O.EVENTPRICE, O.EVENTUNIT,";
                    ls_sql += " H.RESERVATIONCODE HBID, NVL(H.ID,-1) HOTELID,";
                    ls_sql += " H.NAME HOTELNAME, H.ROOMNUMBER, H.ADDRESS,";
                    ls_sql += " H.COUNTRY, H.CITY, H.CHECKIN, H.CHECKOUT, H.TYPEROOM,";
                    ls_sql += " H.PRICEROOM, H.COMPANYNAME HCOMPANYNAME, H.GUESTS,";
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

                    if (aod_order.HotelCompanyName != null)
                        if (aod_order.HotelCompanyName.Trim().Length > 0)
                            ls_sql += " AND H.COMPANYNAME = '" + aod_order.HotelCompanyName + "'";

                    if (aod_order.TransportCode > 0)
                        ls_sql += " AND T.ID = " + aod_order.TransportCode.ToString();

                    if (aod_order.TransportCompanyName != null)
                        if (aod_order.TransportCompanyName.Trim().Length > 0)
                            ls_sql += " AND T.COMPANYNAME = '" + aod_order.TransportCompanyName + "'";

                }

                ls_sql += " ORDER BY O.ORDERCODE DESC) A) B WHERE B.REGISTRO";
                ls_sql += " BETWEEN (" + aod_order.Page + " - 1) * ";
                ls_sql += aod_order.RowsPerPage + " + 1 AND ";
                ls_sql += aod_order.Page + " * " + aod_order.RowsPerPage;
                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                {

                    aod_order.TotalRows = aod_order.TotalRows == 0 ? GetTotalPagedOrders(aod_order) : aod_order.TotalRows;

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
                        lo_order.TotalRows = aod_order.TotalRows;

                        if (aod_order.FlagDetail)
                        {

                            lo_order.EventCode = Convert.ToInt32(ldr_temp["EVENTCODE"]);
                            lo_order.EventName = Convert.ToString(ldr_temp["EVENTNAME"]);
                            lo_order.EventDescription = Convert.ToString(ldr_temp["EVENTDESCRIPTION"]);
                            lo_order.EventDate = Convert.ToDateTime(ldr_temp["EVENTDATE"]);
                            lo_order.EventPrice = Convert.ToDecimal(ldr_temp["EVENTPRICE"]);
                            lo_order.EventUnit = Convert.ToInt32(ldr_temp["EVENTUNIT"]);

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
                                lo_order.Hotel.Guests = Convert.ToInt32(ldr_temp["GUESTS"]);

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

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llo_orders = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetPagedOrders");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return llo_orders;

        }

        public long GetTotalPagedOrders(OrderDTO aod_order)
        {

            long ll_retorno;

            ll_retorno = 0;

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT COUNT(0) TOTALROWS FROM ORDERS O";

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

                    if (aod_order.HotelCompanyName != null)
                        if (aod_order.HotelCompanyName.Trim().Length > 0)
                            ls_sql += " AND H.COMPANYNAME = '" + aod_order.HotelCompanyName + "'";

                    if (aod_order.TransportCode > 0)
                        ls_sql += " AND T.ID = " + aod_order.TransportCode.ToString();

                    if (aod_order.TransportCompanyName != null)
                        if (aod_order.TransportCompanyName.Trim().Length > 0)
                            ls_sql += " AND T.COMPANYNAME = '" + aod_order.TransportCompanyName + "'";

                }

                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                        ll_retorno = Convert.ToInt32(ldr_temp["TOTALROWS"]);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetTotalPagedOrders");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return ll_retorno;

        }

        public List<OrderDTO> GetMostSoldProducts(OrderDTO aod_order)
        {

            List<OrderDTO> llo_orders;

            llo_orders = new List<OrderDTO>();

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT B.* FROM (SELECT ROWNUM REGISTRO, A.* FROM (";
                ls_sql += "SELECT O.EVENTCODE, O.EVENTNAME, O.EVENTDESCRIPTION,";
                ls_sql += " O.EVENTDATE, COUNT(0) CANTIDAD FROM ORDERS O WHERE";
                ls_sql += " O.ORDERDATE BETWEEN TO_DATE('";
                ls_sql += aod_order.OrderDateFrom.ToString("yyyy/MM/dd") + "',";
                ls_sql += "'YYYY/MM/DD') AND TO_DATE('";
                ls_sql += aod_order.OrderDateTo.ToString("yyyy/MM/dd") + "','YYYY/MM/DD')";
                ls_sql += " AND O.ORDERSTATUS = 'PA' GROUP BY O.EVENTCODE, O.EVENTNAME,";
                ls_sql += " O.EVENTDESCRIPTION, O.EVENTDATE ORDER BY CANTIDAD DESC) A) B";
                ls_sql += " WHERE B.REGISTRO BETWEEN (" + aod_order.Page + " - 1) * ";
                ls_sql += aod_order.RowsPerPage + " + 1 AND " + aod_order.Page + " * ";
                ls_sql += aod_order.RowsPerPage;
                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                {

                    aod_order.TotalRows = aod_order.TotalRows == 0 ? GetTotalMostSoldProducts(aod_order) : aod_order.TotalRows;

                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                    {

                        OrderDTO lo_order;

                        lo_order = new OrderDTO();
                        lo_order.EventCode = Convert.ToInt32(ldr_temp["EVENTCODE"]);
                        lo_order.EventName = Convert.ToString(ldr_temp["EVENTNAME"]);
                        lo_order.EventDescription = Convert.ToString(ldr_temp["EVENTDESCRIPTION"]);
                        lo_order.EventDate = Convert.ToDateTime(ldr_temp["EVENTDATE"]);
                        lo_order.EventUnit = Convert.ToInt32(ldr_temp["CANTIDAD"]);
                        lo_order.TotalRows = aod_order.TotalRows;
                        llo_orders.Add(lo_order);

                    }

                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llo_orders = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetMostSoldProducts");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return llo_orders;

        }

        public long GetTotalMostSoldProducts(OrderDTO aod_order)
        {

            long ll_retorno;

            ll_retorno = 0;

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT COUNT(0) TOTALROWS FROM (";
                ls_sql += "SELECT O.EVENTCODE, O.EVENTNAME, O.EVENTDESCRIPTION,";
                ls_sql += " O.EVENTDATE, COUNT(0) CANTIDAD FROM ORDERS O WHERE";
                ls_sql += " O.ORDERDATE BETWEEN TO_DATE('";
                ls_sql += aod_order.OrderDateFrom.ToString("yyyy/MM/dd") + "',";
                ls_sql += "'YYYY/MM/DD') AND TO_DATE('";
                ls_sql += aod_order.OrderDateTo.ToString("yyyy/MM/dd") + "','YYYY/MM/DD')";
                ls_sql += " AND O.ORDERSTATUS = 'PA' GROUP BY O.EVENTCODE, O.EVENTNAME,";
                ls_sql += " O.EVENTDESCRIPTION, O.EVENTDATE ORDER BY CANTIDAD DESC)";
                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                        ll_retorno = Convert.ToInt32(ldr_temp["TOTALROWS"]);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetTotalMostSoldProducts");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return ll_retorno;

        }

        public List<OrderDTO> GetClosedOrdersInvoicedPerMonth(OrderDTO aod_order)
        {

            List<OrderDTO> llo_orders;

            llo_orders = new List<OrderDTO>();

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT COUNT(0) TOTALORDERS, SUM(O.ORDERVALUE) TOTALINVOICED";
                ls_sql += " FROM ORDERS O WHERE O.ORDERSTATUS = '" + aod_order.OrderStatus + "'";
                ls_sql += " AND EXTRACT(MONTH FROM O.ORDERDATE) = " + aod_order.OrderDateFrom.Month;
                ls_sql += " AND EXTRACT(YEAR FROM O.ORDERDATE) = " + aod_order.OrderDateFrom.Year;
                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                {

                    aod_order.TotalRows = aod_order.TotalRows == 0 ? GetTotalMostSoldProducts(aod_order) : aod_order.TotalRows;

                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                    {

                        OrderDTO lo_order;

                        lo_order = new OrderDTO();
                        lo_order.TotalRows = Convert.ToInt32(ldr_temp["TOTALORDERS"]);
                        lo_order.OrderValue = Convert.ToDecimal(ldr_temp["TOTALINVOICED"]);
                        llo_orders.Add(lo_order);

                    }

                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llo_orders = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetClosedOrdersInvoicedPerMonth");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return llo_orders;

        }

        public List<OrderDTO> GetMostOpenedOrders(OrderDTO aod_order)
        {

            List<OrderDTO> llo_orders;

            llo_orders = new List<OrderDTO>();

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT B.* FROM (SELECT ROWNUM REGISTRO, A.* FROM (";
                ls_sql += " SELECT O.ORDERCODE, O.ORDERDATE, O.ORDERSTATUS,";
                ls_sql += " O.ORDERVALUE, C.ID IDUSER, C.CUSTID, C.IDTYPEIDENT";

                if (aod_order.FlagDetail)
                {

                    ls_sql += ", O.EVENTCODE, O.EVENTNAME, O.EVENTDESCRIPTION,";
                    ls_sql += " O.EVENTDATE, O.EVENTPRICE, O.EVENTUNIT,";
                    ls_sql += " H.RESERVATIONCODE HBID, NVL(H.ID,-1) HOTELID,";
                    ls_sql += " H.NAME HOTELNAME, H.ROOMNUMBER, H.ADDRESS,";
                    ls_sql += " H.COUNTRY, H.CITY, H.CHECKIN, H.CHECKOUT, H.TYPEROOM,";
                    ls_sql += " H.PRICEROOM, H.COMPANYNAME HCOMPANYNAME, H.GUESTS,";
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

                ls_sql += " WHERE O.ORDERSTATUS NOT IN ('C','PA','I') AND C.ID = O.IDCUSTOMER";

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

                    if (aod_order.HotelCompanyName != null)
                        if (aod_order.HotelCompanyName.Trim().Length > 0)
                            ls_sql += " AND H.COMPANYNAME = '" + aod_order.HotelCompanyName + "'";

                    if (aod_order.TransportCode > 0)
                        ls_sql += " AND T.ID = " + aod_order.TransportCode.ToString();

                    if (aod_order.TransportCompanyName != null)
                        if (aod_order.TransportCompanyName.Trim().Length > 0)
                            ls_sql += " AND T.COMPANYNAME = '" + aod_order.TransportCompanyName + "'";

                }

                ls_sql += " ORDER BY O.ORDERDATE ASC) A) B WHERE B.REGISTRO";
                ls_sql += " BETWEEN (" + aod_order.Page + " - 1) * ";
                ls_sql += aod_order.RowsPerPage + " + 1 AND ";
                ls_sql += aod_order.Page + " * " + aod_order.RowsPerPage;
                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                {

                    aod_order.TotalRows = aod_order.TotalRows == 0 ? GetTotalMostOpenedOrders(aod_order) : aod_order.TotalRows;

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
                        lo_order.TotalRows = aod_order.TotalRows;

                        if (aod_order.FlagDetail)
                        {

                            lo_order.EventCode = Convert.ToInt32(ldr_temp["EVENTCODE"]);
                            lo_order.EventName = Convert.ToString(ldr_temp["EVENTNAME"]);
                            lo_order.EventDescription = Convert.ToString(ldr_temp["EVENTDESCRIPTION"]);
                            lo_order.EventDate = Convert.ToDateTime(ldr_temp["EVENTDATE"]);
                            lo_order.EventPrice = Convert.ToDecimal(ldr_temp["EVENTPRICE"]);
                            lo_order.EventUnit = Convert.ToInt32(ldr_temp["EVENTUNIT"]);

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
                                lo_order.Hotel.Guests = Convert.ToInt32(ldr_temp["GUESTS"]);

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

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llo_orders = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetMostOpenedOrders");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return llo_orders;

        }

        public long GetTotalMostOpenedOrders(OrderDTO aod_order)
        {

            long ll_retorno;

            ll_retorno = 0;

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT COUNT(0) TOTALROWS FROM ORDERS O";

                if (aod_order.FlagDetail)
                {
                    ls_sql += " LEFT JOIN HOTELRESERVATION H ON (H.IDORDER = O.ORDERCODE)";
                    ls_sql += " LEFT JOIN TRANSPORTRESERVATION T ON (T.IDORDER = O.ORDERCODE),";
                    ls_sql += " CUSTOMER C";
                }
                else
                    ls_sql += ", CUSTOMER C";

                ls_sql += " WHERE O.ORDERSTATUS NOT IN ('C','PA','I') AND C.ID = O.IDCUSTOMER";

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

                    if (aod_order.HotelCompanyName != null)
                        if (aod_order.HotelCompanyName.Trim().Length > 0)
                            ls_sql += " AND H.COMPANYNAME = '" + aod_order.HotelCompanyName + "'";

                    if (aod_order.TransportCode > 0)
                        ls_sql += " AND T.ID = " + aod_order.TransportCode.ToString();

                    if (aod_order.TransportCompanyName != null)
                        if (aod_order.TransportCompanyName.Trim().Length > 0)
                            ls_sql += " AND T.COMPANYNAME = '" + aod_order.TransportCompanyName + "'";

                }

                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                        ll_retorno = Convert.ToInt32(ldr_temp["TOTALROWS"]);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetTotalMostOpenedOrders");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return ll_retorno;

        }

        public List<OrderDTO> GetMostClosedOrdersInvoiced(OrderDTO aod_order)
        {

            List<OrderDTO> llo_orders;

            llo_orders = new List<OrderDTO>();

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = "SELECT B.* FROM (SELECT ROWNUM REGISTRO, A.* FROM (";
                ls_sql += " SELECT O.ORDERCODE, O.ORDERDATE, O.ORDERSTATUS,";
                ls_sql += " O.ORDERVALUE, C.ID IDUSER, C.CUSTID, C.IDTYPEIDENT";

                if (aod_order.FlagDetail)
                {

                    ls_sql += ", O.EVENTCODE, O.EVENTNAME, O.EVENTDESCRIPTION,";
                    ls_sql += " O.EVENTDATE, O.EVENTPRICE, O.EVENTUNIT,";
                    ls_sql += " H.RESERVATIONCODE HBID, NVL(H.ID,-1) HOTELID,";
                    ls_sql += " H.NAME HOTELNAME, H.ROOMNUMBER, H.ADDRESS,";
                    ls_sql += " H.COUNTRY, H.CITY, H.CHECKIN, H.CHECKOUT, H.TYPEROOM,";
                    ls_sql += " H.PRICEROOM, H.COMPANYNAME HCOMPANYNAME, H.GUESTS,";
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

                ls_sql += " WHERE O.ORDERSTATUS = 'PA' AND C.ID = O.IDCUSTOMER";

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

                ls_sql += " ORDER BY O.ORDERVALUE DESC) A) B WHERE B.REGISTRO";
                ls_sql += " BETWEEN (" + aod_order.Page + " - 1) * ";
                ls_sql += aod_order.RowsPerPage + " + 1 AND ";
                ls_sql += aod_order.Page + " * " + aod_order.RowsPerPage;
                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                {

                    aod_order.TotalRows = aod_order.TotalRows == 0 ? GetTotalMostClosedOrdersInvoiced(aod_order) : aod_order.TotalRows;

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
                        lo_order.TotalRows = aod_order.TotalRows;

                        if (aod_order.FlagDetail)
                        {

                            lo_order.EventCode = Convert.ToInt32(ldr_temp["EVENTCODE"]);
                            lo_order.EventName = Convert.ToString(ldr_temp["EVENTNAME"]);
                            lo_order.EventDescription = Convert.ToString(ldr_temp["EVENTDESCRIPTION"]);
                            lo_order.EventDate = Convert.ToDateTime(ldr_temp["EVENTDATE"]);
                            lo_order.EventPrice = Convert.ToDecimal(ldr_temp["EVENTPRICE"]);
                            lo_order.EventUnit = Convert.ToInt32(ldr_temp["EVENTUNIT"]);

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
                                lo_order.Hotel.Guests = Convert.ToInt32(ldr_temp["GUESTS"]);

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

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llo_orders = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetMostClosedOrdersInvoiced");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return llo_orders;

        }

        public long GetTotalMostClosedOrdersInvoiced(OrderDTO aod_order)
        {

            long ll_retorno;

            ll_retorno = 0;

            try
            {

                string ls_sql;
                OracleServerHelper losh_osh;
                DataSet lds_datos;

                ls_sql = " SELECT COUNT(0) TOTALROWS FROM ORDERS O";

                if (aod_order.FlagDetail)
                {
                    ls_sql += " LEFT JOIN HOTELRESERVATION H ON (H.IDORDER = O.ORDERCODE)";
                    ls_sql += " LEFT JOIN TRANSPORTRESERVATION T ON (T.IDORDER = O.ORDERCODE),";
                    ls_sql += " CUSTOMER C";
                }
                else
                    ls_sql += ", CUSTOMER C";

                ls_sql += " WHERE O.ORDERSTATUS = 'PA' AND C.ID = O.IDCUSTOMER";

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

                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                        ll_retorno = Convert.ToInt32(ldr_temp["TOTALROWS"]);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE DATOS OrderService:GetTotalMostClosedOrdersInvoiced");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " : " + le_e.Message);

            }

            return ll_retorno;

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
                        ls_sql += "EVENTDATE,EVENTPRICE,EVENTUNIT) VALUES(" + ll_orderCode + ",SYSDATE,'A',";
                        ls_sql += aod_order.OrderValue.ToString().Replace(",", ".") + ",";
                        ls_sql += aod_order.IdUser + "," + aod_order.EventCode + ",";
                        ls_sql += "'" + aod_order.EventName + "','" + aod_order.EventDescription + "',";
                        ls_sql += "TO_DATE('" + aod_order.EventDate.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                        ls_sql += aod_order.EventPrice.ToString().Replace(",", ".") + "," + aod_order.EventUnit + ")";
                        ll_affected = losh_osh.ExecuteSql(ls_sql, new List<OracleParameter>());

                        if (ll_affected > 0)
                        {

                            if (aod_order.Hotel != null)
                            {

                                ls_sql = "INSERT INTO HOTELRESERVATION (RESERVATIONCODE,NAME,ADDRESS,";
                                ls_sql += "COUNTRY,CITY,PHONENUMBER,ROOMNUMBER,TYPEROOM,PRICEROOM,";
                                ls_sql += "CHECKIN,CHECKOUT,IDORDER,COMPANYNAME,GUESTS) VALUES ('" + aod_order.Hotel.BookingId + "',";
                                ls_sql += "'" + aod_order.Hotel.Name + "','" + aod_order.Hotel.Address + "',";
                                ls_sql += "'" + aod_order.Hotel.Country + "','" + aod_order.Hotel.City + "',";
                                ls_sql += "'" + aod_order.Hotel.PhoneNumber + "','" + aod_order.Hotel.RoomNumber + "',";
                                ls_sql += "'" + aod_order.Hotel.TypeRoom + "'," + aod_order.Hotel.PriceRoom.
                                    ToString().Replace(",", ".") + ",";
                                ls_sql += "TO_DATE('" + aod_order.Hotel.CheckIn.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                                ls_sql += "TO_DATE('" + aod_order.Hotel.CheckOut.ToString("yyyy-MM-dd") + "','YYYY-MM-DD'),";
                                ls_sql += ll_orderCode + ",'" + aod_order.Hotel.CompanyName + "'," + aod_order.Hotel.Guests + ")";
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

                ls_sql = "";
                losh_conection = new OracleServerHelper();
                ll_affected = 1;

                if (aod_order.OrderStatus != null)
                    if (aod_order.OrderStatus.Trim().Length > 0)
                    {

                        ls_sql = "UPDATE ORDERS SET ORDERSTATUS = '" + aod_order.OrderStatus + "'";
                        ls_sql += " WHERE ORDERCODE = " + aod_order.OrderCode;
                        ll_affected = losh_conection.ExecuteSql(ls_sql, new List<OracleParameter>());

                    }

                if (ll_affected > 0)
                {

                    lb_retorno = true;

                    if (aod_order.Hotel != null)
                        if (aod_order.Hotel.BookingId != null || aod_order.Hotel.CancelId != null)
                        {

                            ls_sql = "UPDATE HOTELRESERVATION SET";

                            if (aod_order.Hotel.BookingId != null)
                                if (aod_order.Hotel.BookingId.Trim().Length > 0)
                                    ls_sql += " RESERVATIONCODE = '" + aod_order.Hotel.BookingId + "'";

                            if (aod_order.Hotel.CancelId != null)
                                if (aod_order.Hotel.CancelId.Trim().Length > 0)
                                    if (aod_order.Hotel.BookingId != null)
                                        if (aod_order.Hotel.BookingId.Trim().Length > 0)
                                            ls_sql += " ,CANCELATIONCODE = '" + aod_order.Hotel.CancelId + "'";
                                        else
                                            ls_sql += " CANCELATIONCODE = '" + aod_order.Hotel.CancelId + "'";
                                    else
                                        ls_sql += " CANCELATIONCODE = '" + aod_order.Hotel.CancelId + "'";

                            ls_sql += " WHERE IDORDER = " + aod_order.OrderCode;
                            ll_affected = losh_conection.ExecuteSql(ls_sql, new List<OracleParameter>());

                            if (ll_affected <= 0)
                                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "Error actualizando reserva hotelera, codigo de reserva (" +
                                    aod_order.Hotel.BookingId + "), codigo de cancelacion(" + aod_order.Hotel.CancelId + ").");

                        }

                    if (aod_order.Transport != null)
                        if (aod_order.Transport.BookingId != null || aod_order.Transport.CancelId != null)
                        {

                            ls_sql = "UPDATE TRANSPORTRESERVATION SET";

                            if (aod_order.Transport.BookingId != null)
                                if (aod_order.Transport.BookingId.Trim().Length > 0)
                                    ls_sql += " RESERVATIONCODE = '" + aod_order.Transport.BookingId + "'";

                            if (aod_order.Transport.CancelId != null)
                                if (aod_order.Transport.CancelId.Trim().Length > 0)
                                    if (aod_order.Transport.BookingId != null)
                                        if (aod_order.Transport.BookingId.Trim().Length > 0)
                                            ls_sql += " ,CANCELATIONCODE = '" + aod_order.Transport.CancelId + "'";
                                        else
                                            ls_sql += " CANCELATIONCODE = '" + aod_order.Transport.CancelId + "'";
                                    else
                                        ls_sql += " CANCELATIONCODE = '" + aod_order.Transport.CancelId + "'";

                            ls_sql += " WHERE IDORDER = " + aod_order.OrderCode;
                            ll_affected = losh_conection.ExecuteSql(ls_sql, new List<OracleParameter>());

                            if (ll_affected <= 0)
                                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "Error actualizando reserva de transporte, codigo de reserva (" +
                                    aod_order.Transport.BookingId + "), codigo de cancelacion(" + aod_order.Transport.CancelId + ").");

                        }

                }

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