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

                ls_sql = "SELECT O.ORDERCODE, O.ORDERDATE, O.ORDERSTATUS, O.ORDERVALUE,";
                ls_sql += " C.CUSTID, C.IDTYPEIDENT, O.EVENTCODE, O.HOTELCODE, O.TRANSPORTCODE";
                ls_sql += " FROM CUSTOMER C, ORDERS O WHERE C.CUSTID = '" + aod_order.IdNumber + "'";
                ls_sql += " AND C.IDTYPEIDENT = " + aod_order.IdType.ToString() + " AND O.CUSTID = C.CUSTID";

                if (aod_order.OrderCode > 0)
                    ls_sql += " AND O.ORDERCODE = " + aod_order.OrderCode;

                if (aod_order.OrderDate != null)
                    ls_sql += " AND TO_CHAR(O.ORDERDATE,'DD/MM/YYYY') = '" + aod_order.OrderDate.ToString("dd/MM/yyyy") + "'";

                if (aod_order.OrderStatus != null)
                    if (aod_order.OrderStatus.Trim().Length > 0)
                        ls_sql += " AND O.ORDERSTATUS = '" + aod_order.OrderStatus + "'";

                if (aod_order.OrderValue > 0)
                    ls_sql += " AND O.ORDERVALUE = " + aod_order.OrderValue.ToString();

                if (aod_order.EventCode > 0)
                    ls_sql += " AND O.EVENTCODE = " + aod_order.EventCode.ToString();

                if (aod_order.HotelCode > 0)
                    ls_sql += " AND O.HOTELCODE = " + aod_order.HotelCode.ToString();

                if (aod_order.TransportCode > 0)
                    ls_sql += " AND O.TRANSPORTCODE = " + aod_order.TransportCode.ToString();

                losh_osh = new OracleServerHelper();
                lds_datos = losh_osh.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables.Count > 0)
                {

                    foreach (DataRow ldr_temp in lds_datos.Tables[0].Rows)
                    {

                        OrderDTO lo_order;

                        lo_order = new OrderDTO();
                        lo_order.OrderCode = Convert.ToInt32(ldr_temp["ORDERCODE"]);
                        lo_order.OrderDate = Convert.ToDateTime(ldr_temp["ORDERDATE"]);
                        lo_order.OrderStatus = Convert.ToString(ldr_temp["ORDERSTATUS"]);
                        lo_order.OrderValue = Convert.ToDecimal(ldr_temp["ORDERVALUE"]);
                        lo_order.IdNumber = Convert.ToInt32(ldr_temp["CUSTID"]);
                        lo_order.IdType = Convert.ToInt32(ldr_temp["IDTYPEIDENT"]);
                        lo_order.EventCode = Convert.ToInt32(ldr_temp["EVENTCODE"]);
                        lo_order.HotelCode = Convert.ToInt32(ldr_temp["HOTELCODE"]);
                        lo_order.TransportCode = Convert.ToInt32(ldr_temp["TRANSPORTCODE"]);
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
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;

            }

            return llo_orders;

        }

    }
}