using System.ServiceModel;

namespace SvcHilton.Services.HiltonBookingService
{
    [ServiceContract]
    public interface IHiltonBookingService
    {

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        bookRoomResponse BookRoom(bookRoomRequest abrr_brr);

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        cancelBookingResponse CancelBooking(cancelBookingRequest acbr_cbr);

    }
}