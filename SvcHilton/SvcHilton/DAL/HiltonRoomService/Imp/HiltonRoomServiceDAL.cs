using DAL.Impl.SQLServer;
using SvcHilton.Business.HiltonRoomService.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SvcHilton.DAL.HiltonRoomService.Imp
{
    public class HiltonRoomServiceDAL : SQLDALAdapter, IHiltonRoomServiceDAL
    {
        public List<HotelDTO> SearchAvailableRooms(DataSearchAvailableRoomsDTO asar_sar)
        {

            List<HotelDTO> llh_llh;

            llh_llh = new List<HotelDTO>();

            try
            {

                SqlParameter lsp_parameter;
                List<SqlParameter> llsp_parameters;
                DataSet lds_data;

                lsp_parameter = new SqlParameter("@Ciudad", DbType.String);
                lsp_parameter.Value = asar_sar.City;
                llsp_parameters = new List<SqlParameter>();
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@Pais", DbType.String);
                lsp_parameter.Value = asar_sar.Country;
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@CheckIn", DbType.Date);
                lsp_parameter.Value = asar_sar.CheckIn;
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@CheckOut", DbType.Date);
                lsp_parameter.Value = asar_sar.CheckOut;
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@NroHabitaciones", DbType.Int16);
                lsp_parameter.Value = asar_sar.Rooms;
                llsp_parameters.Add(lsp_parameter);
                lsp_parameter = new SqlParameter("@TipoHabitacion", DbType.String);
                lsp_parameter.Value = asar_sar.Type;
                llsp_parameters.Add(lsp_parameter);
                lds_data = GetDataBaseHelper().ExecuteProcedureToDataSet("GetRooms", llsp_parameters);

                if (lds_data != null && lds_data.Tables.Count > 0)
                {

                    foreach (DataRow ldr_temp in lds_data.Tables[0].Rows)
                    {

                        HotelDTO lh_hotel;
                        RoomDTO lr_room;

                        lh_hotel = llh_llh.Find(x => x.CodigoHotel == Convert.ToInt16(ldr_temp["CodigoHotel"]));
                        lr_room = new RoomDTO
                        {
                            Number = Convert.ToInt16(ldr_temp["NroHabitacion"]),
                            Price = float.Parse(Convert.ToString(ldr_temp["PrecioHabitacion"])),
                            Type = Convert.ToString(ldr_temp["TipoHabitacion"])
                        };

                        if (lh_hotel == null)
                        {

                            lh_hotel = new HotelDTO
                            {
                                CodigoHotel = Convert.ToInt16(ldr_temp["CodigoHotel"]),
                                Name = Convert.ToString(ldr_temp["NombreHotel"]),
                                Address = Convert.ToString(ldr_temp["DireccionHotel"]),
                                City = Convert.ToString(ldr_temp["CiudadHotel"]),
                                Country = Convert.ToString(ldr_temp["PaisHotel"]),
                                Rooms = new List<RoomDTO>()
                            };

                            lh_hotel.Rooms.Add(lr_room);
                            llh_llh.Add(lh_hotel);

                        }
                        else
                        {

                            if (!lh_hotel.Rooms.Contains(lr_room))
                                lh_hotel.Rooms.Add(lr_room);

                        }

                    }
                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llh_llh = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO IHiltonRoomService Initiate:");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);

            }

            return llh_llh;

        }
    }
}