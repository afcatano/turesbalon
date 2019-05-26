using DAL.Impl.SQLServer;
using SvcHilton.Business.HiltonBookingService.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SvcHilton.DAL.HiltonBookingService.Imp
{
    public class HiltonBookingServiceDAL : SQLDALAdapter, IHiltonBookingServiceDAL
    {
        public long BookRoom(RoomReservationDTO arr_arr)
        {

            long ll_bookingId;

            ll_bookingId = 0;

            try
            {

                SqlParameter lsp_parameter;
                List<SqlParameter> llsp_parameters;
                DataSet lds_data;

                lsp_parameter = new SqlParameter("@GuestName", DbType.String);
                lsp_parameter.Value = arr_arr.GuestName;
                llsp_parameters = new List<SqlParameter>();
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@roomNumer", DbType.Int16);
                lsp_parameter.Value = arr_arr.RoomNumber;
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@checkIN", DbType.Date);
                lsp_parameter.Value = arr_arr.CheckIn;
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@ChecOUT", DbType.Date);
                lsp_parameter.Value = arr_arr.CheckOut;
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@Hotel", DbType.String);
                lsp_parameter.Value = arr_arr.Hotel;
                llsp_parameters.Add(lsp_parameter);
                lds_data = GetDataBaseHelper().ExecuteProcedureToDataSet("BookRoom", llsp_parameters);

                if (lds_data != null && lds_data.Tables.Count > 0)
                {

                    foreach (DataRow ldr_temp in lds_data.Tables[0].Rows)
                    {

                        ll_bookingId = Convert.ToInt64(ldr_temp["CodReserva"]);

                    }
                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                ll_bookingId = -1;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO HiltonBookingServiceDAL:BookRoom");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);

            }

            return ll_bookingId;

        }

        public long CancelBooking(string as_bookingId)
        {

            long ll_cancelId;

            ll_cancelId = 0;

            try
            {

                SqlParameter lsp_parameter;
                List<SqlParameter> llsp_parameters;
                DataSet lds_data;

                lsp_parameter = new SqlParameter("@codigoReserva", DbType.Int16);
                lsp_parameter.Value = as_bookingId;
                llsp_parameters = new List<SqlParameter>();
                llsp_parameters.Add(lsp_parameter);

                lds_data = GetDataBaseHelper().ExecuteProcedureToDataSet("CancelBook", llsp_parameters);

                if (lds_data != null && lds_data.Tables.Count > 0)
                {

                    foreach (DataRow ldr_temp in lds_data.Tables[0].Rows)
                    {

                        ll_cancelId = Convert.ToInt16(ldr_temp["CodCancelacion"]);

                    }
                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                ll_cancelId = -1;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO HiltonBookingServiceDAL:CancelBooking");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);

            }

            return ll_cancelId;

        }
    }
}