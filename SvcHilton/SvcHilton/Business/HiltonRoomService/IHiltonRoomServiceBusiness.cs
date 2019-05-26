using SvcHilton.Business.HiltonRoomService.DTO;
using System.Collections.Generic;

namespace SvcHilton.Business.HiltonRoomService
{
    interface IHiltonRoomServiceBusiness
    {

        List<HotelDTO> SearchAvailableRooms(DataSearchAvailableRoomsDTO adsar_dsar);

    }
}