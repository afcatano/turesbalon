using SvcHilton.Business.HiltonRoomService.DTO;
using SvcHilton.Business.HiltonRoomService;
using SvcHilton.Business.HiltonRoomService.Imp;
using System.Collections.Generic;
using System;

namespace SvcHilton.Services.HiltonRoomService
{

    public class HiltonRoomService : IHiltonRoomService
    {
        initiateResponse IHiltonRoomService.Initiate(initiateRequest air_request)
        {

            initiateResponse lir_response;

            lir_response = new initiateResponse
            {
                HiltonRoomServiceProcessResponse = new HiltonRoomServiceProcessResponse()
            };
            lir_response.HiltonRoomServiceProcessResponse.status = new Status();

            try
            {

                DataSearchAvailableRoomsDTO ldsar_dsar;
                IHiltonRoomServiceBusiness lhrsb_hrsb;
                List<HotelDTO> llh_hotelesTemp;

                ldsar_dsar = new DataSearchAvailableRoomsDTO
                {
                    City = air_request.HiltonRoomServiceProcessRequest.City,
                    Country = air_request.HiltonRoomServiceProcessRequest.Country,
                    CheckIn = air_request.HiltonRoomServiceProcessRequest.CheckIn,
                    CheckOut = air_request.HiltonRoomServiceProcessRequest.CheckOut,
                    Rooms = air_request.HiltonRoomServiceProcessRequest.Rooms,
                    Type = air_request.HiltonRoomServiceProcessRequest.Type
                };
                lhrsb_hrsb = new HiltonRoomServiceBusiness();
                llh_hotelesTemp = lhrsb_hrsb.SearchAvailableRooms(ldsar_dsar);

                if (llh_hotelesTemp != null)
                {

                    if (llh_hotelesTemp.Count > 0)
                    {

                        List<Hotel> llh_hoteles;

                        llh_hoteles = new List<Hotel>();

                        foreach (HotelDTO lh_hotelTemp in llh_hotelesTemp)
                        {

                            Hotel lh_hotel;
                            List<Room> llr_rooms;

                            lh_hotel = new Hotel
                            {
                                Name = lh_hotelTemp.Name,
                                Address = lh_hotelTemp.Address,
                                City = lh_hotelTemp.City,
                                Country = lh_hotelTemp.Country
                            };
                            llr_rooms = new List<Room>();

                            foreach (RoomDTO lr_roomTemp in lh_hotelTemp.Rooms)
                            {

                                Room lr_room;

                                lr_room = new Room
                                {
                                    Number = lr_roomTemp.Number,
                                    Price = lr_roomTemp.Price,
                                    Type = lr_roomTemp.Type
                                };

                                llr_rooms.Add(lr_room);

                            }

                            lh_hotel.Rooms = llr_rooms.ToArray();
                            llh_hoteles.Add(lh_hotel);

                        }

                        lir_response.HiltonRoomServiceProcessResponse.status.code = "00";
                        lir_response.HiltonRoomServiceProcessResponse.status.error = "";
                        lir_response.HiltonRoomServiceProcessResponse.result = llh_hoteles.ToArray();

                    }
                    else
                    {
                        lir_response.HiltonRoomServiceProcessResponse.status.code = "01";
                        lir_response.HiltonRoomServiceProcessResponse.status.error = "No hay habitaciones disponibles con los parametros ingresados";
                    }

                }
                else
                {

                    lir_response.HiltonRoomServiceProcessResponse.status.code = "01";
                    lir_response.HiltonRoomServiceProcessResponse.status.error = "Error en la ejecución del servicio";

                }

            }
            catch (Exception ae_e)
            {

                Exception le_e;

                le_e = ae_e.InnerException != null ? ae_e.InnerException : ae_e;
                lir_response.HiltonRoomServiceProcessResponse.status.code = "01";
                lir_response.HiltonRoomServiceProcessResponse.status.error = "Error en la ejecución del servicio:" + ae_e.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO IHiltonRoomService Initiate:");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);

            }

            return lir_response;

        }
    }
}
