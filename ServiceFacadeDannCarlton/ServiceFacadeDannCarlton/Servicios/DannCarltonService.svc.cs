using CommonsWeb.DTO;
using ServiceFacadeDannCarlton.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceFacadeDannCarlton.Servicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "DannCarltonService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione DannCarltonService.svc o DannCarltonService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class DannCarltonService : IDannCarltonService
    {
        GetBookedRoomsByBranchResponse IDannCarltonService.GetBookedRoomsByBranch(GetBookedRoomsByBranchRequest getBookedRoomsByBranchRequest)
        {
            GetBookedRoomsByBranchResponse getBookedRoomsByBranchResponse = new GetBookedRoomsByBranchResponse();
            getBookedRoomsByBranchResponse.Status = new Status();

            try
            {
                IDannCarltonServiceBusiness iDannCarlonSBusiness;

                iDannCarlonSBusiness = new DannCarltonServiceBusiness();
                getBookedRoomsByBranchResponse = iDannCarlonSBusiness.BookedRoomsByBranchResponse(getBookedRoomsByBranchRequest);
            }
            catch (Exception ex)
            {
                getBookedRoomsByBranchResponse.Status.ErrorCode = "01";
                getBookedRoomsByBranchResponse.Status.ErrorDescription = "Error en el Servicio";
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO DannCarltonService: GetBookedRoomsByBranch" + ex.Message);
                throw ex;
            }

            return getBookedRoomsByBranchResponse;
        }

        BookRoomResponse IDannCarltonService.BookRoom(BookRoomRequest BookRoomRequest)
        {
            BookRoomResponse bookRoomResponse = new BookRoomResponse();
            bookRoomResponse.Status = new Status();

            try
            {
                ReservationsDTO reservationsDTO;
                IDannCarltonServiceBusiness iDannCarlonSBusiness;

                reservationsDTO = new ReservationsDTO
                {
                    CheckIn = BookRoomRequest.CheckIn,
                    CheckOut = BookRoomRequest.CheckOut,
                    BranchId = BookRoomRequest.BranchId,
                    GuestDocumentNumber = BookRoomRequest.GuestDocumentNumber,
                    GuestDocumentTypeId = BookRoomRequest.GuestDocumentTypeId,
                    GuestFullName = BookRoomRequest.GuestFullName,
                    IsCancelprocess = BookRoomRequest.IsCancelprocess,
                    RoomId = BookRoomRequest.RoomId,
                    SourceCompanyCode = BookRoomRequest.SourceCompanyCode
                };

                iDannCarlonSBusiness = new DannCarltonServiceBusiness();
                bookRoomResponse = iDannCarlonSBusiness.BookRoomResponseDTOs(reservationsDTO);
            }
            catch (Exception ex)
            {
                bookRoomResponse.Status.ErrorCode = "01";
                bookRoomResponse.Status.ErrorDescription = "Error en el Servicio " + ex.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO DannCarltonService:BookRoom " + ex.Message);
                //throw ex;
            }

            return bookRoomResponse;
        }

        GetDannBranchResponse IDannCarltonService.GetDannBranch(GetDannBranchRequest prmgetDannBranchRequest)
        {
            GetDannBranchResponse getDannBranchResponse = new GetDannBranchResponse();
            getDannBranchResponse.Status = new Status();

            try
            {
                IDannCarltonServiceBusiness iDannCarlonSBusiness;

                iDannCarlonSBusiness = new DannCarltonServiceBusiness();
                getDannBranchResponse = iDannCarlonSBusiness.GetDannBranch(prmgetDannBranchRequest);
            }
            catch (Exception ex)
            {
                getDannBranchResponse.Status.ErrorCode = "01";
                getDannBranchResponse.Status.ErrorDescription = "Error en el Servicio";
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO DannCarltonService: GetDannBranch" + ex.Message);
                throw ex;
            }
            return getDannBranchResponse;
        }

        GetRoomsByBranchResponse IDannCarltonService.GetRoomsByBranch(GetRoomsByBranchRequest prmgetRoomsByBranch)
        {
            GetRoomsByBranchResponse getRoomsByBranchResponse = new GetRoomsByBranchResponse();
            getRoomsByBranchResponse.Status = new Status();

            try
            {
                IDannCarltonServiceBusiness iDannCarlonSBusiness;

                iDannCarlonSBusiness = new DannCarltonServiceBusiness();
                getRoomsByBranchResponse = iDannCarlonSBusiness.GetRoomsByBranch(prmgetRoomsByBranch);
            }
            catch (Exception ex)
            {
                getRoomsByBranchResponse.Status.ErrorCode = "01";
                getRoomsByBranchResponse.Status.ErrorDescription = "Error en el Servicio";
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO DannCarltonService: GetRoomsByBranch" + ex.Message);
                throw ex;
            }
            return getRoomsByBranchResponse;
        }
    }
}
