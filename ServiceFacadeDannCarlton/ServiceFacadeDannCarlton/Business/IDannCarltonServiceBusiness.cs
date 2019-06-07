using CommonsWeb.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFacadeDannCarlton.Business
{
    interface IDannCarltonServiceBusiness
    {
        BookRoomResponse BookRoomResponseDTOs(ReservationsDTO insertBookRoom);
        GetBookedRoomsByBranchResponse BookedRoomsByBranchResponse(GetBookedRoomsByBranchRequest getBookedRoomsByBranchRequest);
        GetDannBranchResponse GetDannBranch(GetDannBranchRequest prmgetDannBranchRequest);
        GetRoomsByBranchResponse GetRoomsByBranch(GetRoomsByBranchRequest prmgetRoomsByBranchRequest);
    }
}
