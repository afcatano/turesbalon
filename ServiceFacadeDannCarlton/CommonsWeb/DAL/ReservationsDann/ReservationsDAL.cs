using CommonsWeb.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CommonsWeb.DAL.ReservationsDann
{
    public class ReservationsDAL
    {
        public StatusDTO InsertarReserva(ReservationsDTO reservationsDTO)
        {
            StatusDTO statusDTO = new StatusDTO();

            try
            {
                SqlParameter sqlParameter;
                List<SqlParameter> lstsqlParameter = new List<SqlParameter>();
                DataSet dsrsta;

                sqlParameter = new SqlParameter("@BookDate", DbType.Date);
                sqlParameter.Value = reservationsDTO.BookDate;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@BranchId", DbType.Int32);
                sqlParameter.Value = reservationsDTO.BranchId;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@RoomId", DbType.Int32);
                sqlParameter.Value = reservationsDTO.RoomId;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@GuestFullName", DbType.String);
                sqlParameter.Value = reservationsDTO.GuestFullName;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@GuestDocumentNumber", DbType.String);
                sqlParameter.Value = reservationsDTO.GuestDocumentNumber;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@GuestDocumentTypeId", DbType.Int32);
                sqlParameter.Value = reservationsDTO.GuestDocumentTypeId;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@SourceCompanyCode", DbType.String);
                sqlParameter.Value = reservationsDTO.SourceCompanyCode;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@IsCancelprocess", DbType.Boolean);
                sqlParameter.Value = reservationsDTO.IsCancelprocess;
                lstsqlParameter.Add(sqlParameter);

                SqlServerHelper SQLConection = new SqlServerHelper();
                dsrsta = SQLConection.ExecuteProcedureToDataSet("BookRoom", lstsqlParameter);

                if (dsrsta != null && dsrsta.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow drTemp in dsrsta.Tables[0].Rows)
                    {

                        statusDTO.ErrorCode = drTemp["ErrorCode"].ToString();
                        statusDTO.ErrorDescription = drTemp["ErrorDescription"].ToString();
                    }
                }

            }
            catch (Exception ae_e)
            {

                statusDTO = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO ReservationsDAL:BookRoom");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);

            }


            return statusDTO;
        }
        
        public List<ReservationsDTO> ObtenerReservas(ReservationsDTO reservationsDTO)
        {
            List<ReservationsDTO> lstreservationsDTOs = new List<ReservationsDTO>();

            try
            {
                SqlParameter sqlParameter;
                List<SqlParameter> lstsqlParameter = new List<SqlParameter>();
                DataSet dsrsta;

                sqlParameter = new SqlParameter("@BranchCode", DbType.String);
                sqlParameter.Value = reservationsDTO.BranchCode;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@RoomNumber", DbType.String);
                sqlParameter.Value = reservationsDTO.RoomNumber;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@CheckIn", DbType.Date);
                sqlParameter.Value = reservationsDTO.CheckIn;
                lstsqlParameter.Add(sqlParameter);
                sqlParameter = new SqlParameter("@CheckOut", DbType.Date);
                sqlParameter.Value = reservationsDTO.CheckOut;
                lstsqlParameter.Add(sqlParameter);

                SqlServerHelper SQLConection = new SqlServerHelper();
                dsrsta = SQLConection.ExecuteProcedureToDataSet("GetBookedRoomsByBranch", lstsqlParameter);

                if (dsrsta != null && dsrsta.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow drTemp in dsrsta.Tables[0].Rows)
                    {
                        ReservationsDTO reservationsDTOs;
                        reservationsDTOs = new ReservationsDTO
                        {
                            BookDate = Convert.ToDateTime(drTemp["BookDate"]),
                            BranchName = Convert.ToString(drTemp["BranchName"]),
                            BranchCode = Convert.ToString(drTemp["BranchCode"]),
                            RoomName = Convert.ToString(drTemp["RoomName"]),
                            Number = Convert.ToString(drTemp["Number"]),
                            Beds = Convert.ToInt32(drTemp["Beds"]),
                            Description = Convert.ToString(drTemp["Description"]),
                            Price = Convert.ToDecimal(drTemp["Price"]),
                            Vat = Convert.ToDecimal(drTemp["Vat"]),
                            Discount = Convert.ToDecimal(drTemp["Discount"])
                        };

                        lstreservationsDTOs.Add(reservationsDTOs);
                        reservationsDTOs = null;
                    }
                }

            }
            catch (Exception ae_e)
            {

                lstreservationsDTOs = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO ReservationsDAL:GetBookedRoomsByBranch");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);
            }

            return lstreservationsDTOs;
        }

        public List<ReservationsDTO> GetDannBranch(ReservationsDTO reservationsDTO)
        {
           List<ReservationsDTO> lstreservationsDTOs = new List<ReservationsDTO>();

        try
        {
            SqlParameter sqlParameter;
            List<SqlParameter> lstsqlParameter = new List<SqlParameter>();
            DataSet dsrsta;

            sqlParameter = new SqlParameter("@BranchCode", DbType.String);
            sqlParameter.Value = reservationsDTO.BranchCode;
            lstsqlParameter.Add(sqlParameter);
            sqlParameter = new SqlParameter("@BranchName", DbType.String);
            sqlParameter.Value = reservationsDTO.BranchName;
            lstsqlParameter.Add(sqlParameter);
            sqlParameter = new SqlParameter("@CityName", DbType.String);
            sqlParameter.Value = reservationsDTO.CityName;
            lstsqlParameter.Add(sqlParameter);
            sqlParameter = new SqlParameter("@CiiuCode", DbType.String);
            sqlParameter.Value = reservationsDTO.CiiuCode;
            lstsqlParameter.Add(sqlParameter);

            SqlServerHelper SQLConection = new SqlServerHelper();
            dsrsta = SQLConection.ExecuteProcedureToDataSet("GetDannBranch", lstsqlParameter);

            if (dsrsta != null && dsrsta.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow drTemp in dsrsta.Tables[0].Rows)
                {
                    ReservationsDTO reservationsDTOs;
                    reservationsDTOs = new ReservationsDTO
                    {
                        ID = Convert.ToInt32(drTemp["Id"]),
                        BranchName = Convert.ToString(drTemp["BranchName"]),
                        BranchCode = Convert.ToString(drTemp["BranchCode"]),
                        Stars = Convert.ToDecimal(drTemp["Stars"]),
                        Address = Convert.ToString(drTemp["Address"]),
                        Phone = Convert.ToString(drTemp["Phone"]),
                        City = Convert.ToString(drTemp["City"]),
                        CiiuCode = Convert.ToString(drTemp["CiiuCode"])
                    };

                    lstreservationsDTOs.Add(reservationsDTOs);
                    reservationsDTOs = null;
                }
            }

        }
        catch (Exception ae_e)
        {

            lstreservationsDTOs = null;
            Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO ReservationsDAL:GetDannBranch");
            Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);
        }

        return lstreservationsDTOs; 
    }

        public List<ReservationsDTO> GetRoomsByBranch(ReservationsDTO reservationsDTO)
        {
            List<ReservationsDTO> lstreservationsDTOs = new List<ReservationsDTO>();

            try
            {
                SqlParameter sqlParameter;
                List<SqlParameter> lstsqlParameter = new List<SqlParameter>();
                DataSet dsrsta;

                sqlParameter = new SqlParameter("@BranchCode", DbType.String);
                sqlParameter.Value = reservationsDTO.BranchCode;
                lstsqlParameter.Add(sqlParameter);

                SqlServerHelper SQLConection = new SqlServerHelper();
                dsrsta = SQLConection.ExecuteProcedureToDataSet("GetRoomsByBranch", lstsqlParameter);

                if (dsrsta != null && dsrsta.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow drTemp in dsrsta.Tables[0].Rows)
                    {
                        ReservationsDTO reservationsDTOs;
                        reservationsDTOs = new ReservationsDTO
                        {
                            ID = Convert.ToInt32(drTemp["Id"]),
                            RoomName = Convert.ToString(drTemp["RoomName"]),
                            Number = Convert.ToString(drTemp["Number"]),
                            Beds = Convert.ToInt32(drTemp["Beds"]),
                            Description = Convert.ToString(drTemp["Description"]),
                            BranchId = Convert.ToInt32(drTemp["BranchId"]),
                            Price = Convert.ToDecimal(drTemp["Price"]),
                            Vat = Convert.ToDecimal(drTemp["Vat"]),
                            Discount = Convert.ToDecimal(drTemp["Discount"])
                        };

                        lstreservationsDTOs.Add(reservationsDTOs);
                        reservationsDTOs = null;
                    }
                }

            }
            catch (Exception ae_e)
            {

                lstreservationsDTOs = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO ReservationsDAL:GetRoomsByBranch");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);
            }

            return lstreservationsDTOs;
        }
    }
}