using System.ServiceModel;

namespace SvcHilton.Services.HiltonRoomService
{

    [ServiceContract]
    public interface IHiltonRoomService
    {

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        initiateResponse Initiate(initiateRequest request);

    }
}