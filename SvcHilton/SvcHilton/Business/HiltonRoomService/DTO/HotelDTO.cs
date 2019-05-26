using System.Collections.Generic;

namespace SvcHilton.Business.HiltonRoomService.DTO
{
    public class HotelDTO
    {

        public int CodigoHotel { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<RoomDTO> Rooms { get; set; }

    }
}