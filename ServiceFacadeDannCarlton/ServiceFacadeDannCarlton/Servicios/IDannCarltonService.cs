using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceFacadeDannCarlton.Servicios
{
    [ServiceContract]
    public interface IDannCarltonService
    {
        [XmlSerializerFormatAttribute()]
        [OperationContract]
        BookRoomResponse BookRoom(BookRoomRequest BookRoomRequest);

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        GetBookedRoomsByBranchResponse GetBookedRoomsByBranch(GetBookedRoomsByBranchRequest getBookedRoomsByBranchRequest);

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        GetDannBranchResponse GetDannBranch(GetDannBranchRequest prmgetDannBranchRequest);

        [XmlSerializerFormatAttribute()]
        [OperationContract]
        GetRoomsByBranchResponse GetRoomsByBranch(GetRoomsByBranchRequest prmgetRoomsByBranch);
    }
}
