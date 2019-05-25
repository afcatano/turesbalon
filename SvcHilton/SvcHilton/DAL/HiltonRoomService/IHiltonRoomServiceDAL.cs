using SvcHilton.Business.HiltonRoomService.DTO;
using System.Collections.Generic;

namespace SvcHilton.DAL.HiltonRoomService
{
    interface IHiltonRoomServiceDAL
    {

        List<HotelDTO> SearchAvailableRooms(DataSearchAvailableRoomsDTO asar_sar);

    }
}