using System;

namespace SvcHilton.Business.HiltonRoomService.DTO
{
    public class DataSearchAvailableRoomsDTO
    {

        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Rooms { get; set; }
        public string Type { get; set; }

        public override bool Equals(object ao_obj)
        {

            DataSearchAvailableRoomsDTO ldsar_dsar;

            ldsar_dsar = ao_obj as DataSearchAvailableRoomsDTO;

            if (ldsar_dsar == null)
                return false;

            if (City != ldsar_dsar.City || Country != ldsar_dsar.Country ||
                CheckIn != ldsar_dsar.CheckIn || CheckOut != ldsar_dsar.CheckOut ||
                Rooms != ldsar_dsar.Rooms || Type != ldsar_dsar.Type)
                return false;

            return true;
        }

    }
}