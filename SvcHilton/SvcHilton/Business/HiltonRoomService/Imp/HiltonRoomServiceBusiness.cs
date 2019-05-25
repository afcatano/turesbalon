using System;
using System.Collections.Generic;
using CommonsWeb.Util;
using SvcHilton.Business.HiltonRoomService.DTO;
using SvcHilton.DAL.HiltonRoomService;
using SvcHilton.DAL.HiltonRoomService.Imp;

namespace SvcHilton.Business.HiltonRoomService.Imp
{
    public class HiltonRoomServiceBusiness : IHiltonRoomServiceBusiness
    {
        public List<HotelDTO> SearchAvailableRooms(DataSearchAvailableRoomsDTO adsar_dsar)
        {

            List<HotelDTO> llh_hoteles;

            llh_hoteles = null;

            try
            {

                if (adsar_dsar.City == null || adsar_dsar.City.Trim().Length == 0)
                    throw new Exception("La ciudad es obligatoria");

                if (adsar_dsar.Country == null || adsar_dsar.Country.Trim().Length == 0)
                    throw new Exception("El país es obligatorio");

                if (adsar_dsar.CheckIn == null)
                    throw new Exception("El check-in es obligatorio");

                if (adsar_dsar.CheckOut == null)
                    throw new Exception("El check-out es obligatorio");

                if (adsar_dsar.Rooms == 0)
                    throw new Exception("El numero de habitaciones debe ser mayor a 0");

                if (adsar_dsar.Type == null || adsar_dsar.Type.Trim().Length == 0)
                    throw new Exception("El tipo de habitacion es obligatorio");

                IHiltonRoomServiceDAL lhrsDAL_hrsDAL;

                lhrsDAL_hrsDAL = new HiltonRoomServiceDAL();
                llh_hoteles = lhrsDAL_hrsDAL.SearchAvailableRooms(adsar_dsar);

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                llh_hoteles = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO HiltonRoomService:Initiate");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);
                throw ae_e;

            }

            return llh_hoteles;

        }
    }
}