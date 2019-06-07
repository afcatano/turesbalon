using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonsWeb.DAL.ReservationsDann;
using CommonsWeb.DTO;

namespace ServiceFacadeDannCarlton.Business
{
    public class DannCarltonServiceBusiness : IDannCarltonServiceBusiness
    {
        GetBookedRoomsByBranchResponse IDannCarltonServiceBusiness.BookedRoomsByBranchResponse(GetBookedRoomsByBranchRequest getBookedRoomsByBranchRequest)
        {
            GetBookedRoomsByBranchResponse getBookedRoomsByBranchResponse = new GetBookedRoomsByBranchResponse();
            getBookedRoomsByBranchResponse.Status = new Status();
            List<GetBookedRoomsByBranchResult> lstgetBookedRoomsByBranchResults = new List<GetBookedRoomsByBranchResult>();

            try
            {
                ReservationsDTO reservationsDTO = new ReservationsDTO();

                if (getBookedRoomsByBranchRequest != null)
                {
                    if (getBookedRoomsByBranchRequest.BranchCode != null && getBookedRoomsByBranchRequest.BranchCode.Trim().Length > 0)
                    {
                        reservationsDTO.BranchCode = getBookedRoomsByBranchRequest.BranchCode;
                    }
                    else
                    {
                        throw new Exception("Branch Code Obligatorio");
                    }

                    if (getBookedRoomsByBranchRequest.RoomNumber != null && getBookedRoomsByBranchRequest.RoomNumber.Trim().Length > 0)
                    {
                        reservationsDTO.RoomNumber = getBookedRoomsByBranchRequest.RoomNumber;
                    }
                    else
                    {
                        throw new Exception("Room Number Obligatorio");
                    }
                    if (getBookedRoomsByBranchRequest.CheckIn != null && getBookedRoomsByBranchRequest.CheckIn != new DateTime())
                    {
                        reservationsDTO.CheckIn = getBookedRoomsByBranchRequest.CheckIn;
                    }
                    else
                    {
                        throw new Exception("Check In Obligatorio");
                    }
                    if (getBookedRoomsByBranchRequest.CheckOut != null && getBookedRoomsByBranchRequest.CheckOut != new DateTime())
                    {
                        reservationsDTO.CheckOut = getBookedRoomsByBranchRequest.CheckOut;
                    }
                    else
                    {
                        throw new Exception("Check Out Obligatorio");
                    }

                    ReservationsDAL reservationsDAL = new ReservationsDAL();
                    List<ReservationsDTO> lstreservationsDTOs;

                    lstreservationsDTOs = reservationsDAL.ObtenerReservas(reservationsDTO);

                    if (lstreservationsDTOs != null)
                    {
                        getBookedRoomsByBranchResponse.Status.ErrorDescription = "0";

                        if (lstreservationsDTOs.Count > 0)
                        {

                            foreach (ReservationsDTO FEreservationsDTO in lstreservationsDTOs)
                            {

                                GetBookedRoomsByBranchResult getBookedRoomsByBranchResult;
                                getBookedRoomsByBranchResult = new GetBookedRoomsByBranchResult
                                {
                                    BranchName = FEreservationsDTO.BranchName,
                                    Beds = FEreservationsDTO.Beds,
                                    BookDate = FEreservationsDTO.BookDate,
                                    BranchCode = FEreservationsDTO.BranchCode,
                                    Description = FEreservationsDTO.Description,
                                    Discount = FEreservationsDTO.Discount,
                                    Number = FEreservationsDTO.Number,
                                    Price = FEreservationsDTO.Price,
                                    RoomName = FEreservationsDTO.RoomName,
                                    Vat = FEreservationsDTO.Vat
                                };

                                lstgetBookedRoomsByBranchResults.Add(getBookedRoomsByBranchResult);

                            }

                            getBookedRoomsByBranchResponse.Status.ErrorCode = "0";
                            getBookedRoomsByBranchResponse.Status.ErrorDescription = "Proceso Satisfactorio";
                            getBookedRoomsByBranchResponse.GetBookedRoomsByBranchResult = lstgetBookedRoomsByBranchResults.ToArray();
                        }
                        else
                        {
                            getBookedRoomsByBranchResponse.Status.ErrorDescription = "No hay reservas disponibles con los parametros ingresados";
                        }
                    }
                    else
                    {
                        getBookedRoomsByBranchResponse.Status.ErrorCode = "01";
                        getBookedRoomsByBranchResponse.Status.ErrorDescription = "Error en la ejecución del servicio";
                    }
                }
                else
                {
                    throw new Exception("Parametros de entrada vacios");
                }
            }
            catch (Exception exc)
            {
                Exception le_e;

                le_e = exc.InnerException != null ? exc.InnerException : exc;
                getBookedRoomsByBranchResponse.Status.ErrorCode = "01";
                getBookedRoomsByBranchResponse.Status.ErrorDescription = exc.InnerException != null ? "Error en la ejecucion del servicio" : exc.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO DannCarltonService:GetBookedRoomsByBranch");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;
            }

            return getBookedRoomsByBranchResponse;
        }

        BookRoomResponse IDannCarltonServiceBusiness.BookRoomResponseDTOs(ReservationsDTO insertbookRoom)
        {
            BookRoomResponse bookRoomResponse = new BookRoomResponse();
            bookRoomResponse.Status = new Status();

            try
            {
                ReservationsDTO reservationsDTO = new ReservationsDTO();

                if (insertbookRoom != null)
                {
                    if (insertbookRoom.CheckIn != null && insertbookRoom.CheckIn != new DateTime())
                    {
                        reservationsDTO.CheckIn = insertbookRoom.CheckIn;
                        reservationsDTO.BookDate = insertbookRoom.CheckIn;
                    }
                    else
                    {
                        throw new Exception("CheckIN Obligatorio");
                    }
                    if (insertbookRoom.CheckOut != null && insertbookRoom.CheckOut != new DateTime())
                    {
                        reservationsDTO.CheckOut = insertbookRoom.CheckOut;
                    }
                    else
                    {
                        throw new Exception("Check OUT Obligatorio");
                    }

                    if (insertbookRoom.BranchId != 0)
                    {
                        reservationsDTO.BranchId = insertbookRoom.BranchId;
                    }
                    else
                    {
                        throw new Exception("Branch Id Obligatorio");
                    }
                    if (insertbookRoom.RoomId != 0)
                    {
                        reservationsDTO.RoomId = insertbookRoom.RoomId;
                    }
                    else
                    {
                        throw new Exception("Room Id Obligatorio");
                    }
                    if (insertbookRoom.GuestFullName != null && insertbookRoom.GuestFullName.Trim().Length > 0)
                    {
                        reservationsDTO.GuestFullName = insertbookRoom.GuestFullName;
                    }
                    else
                    {
                        throw new Exception("Guest Full Name Obligatorio");
                    }
                    if (insertbookRoom.GuestDocumentNumber != null && insertbookRoom.GuestDocumentNumber.Trim().Length > 0)
                    {
                        reservationsDTO.GuestDocumentNumber = insertbookRoom.GuestDocumentNumber;
                    }
                    else
                    {
                        throw new Exception("Guest Document Number Obligatorio");
                    }
                    if (insertbookRoom.GuestDocumentTypeId != 0)
                    {
                        reservationsDTO.GuestDocumentTypeId = insertbookRoom.GuestDocumentTypeId;
                    }
                    else
                    {
                        throw new Exception("Guest Document TypeId Obligatorio");
                    }
                    if (insertbookRoom.SourceCompanyCode != null && insertbookRoom.SourceCompanyCode.Trim().Length > 0)
                    {
                        reservationsDTO.SourceCompanyCode = insertbookRoom.SourceCompanyCode;
                    }
                    else
                    {
                        throw new Exception("Source Company Code Obligatorio");
                    }

                    ReservationsDAL reservationsDAL = new ReservationsDAL();
                    StatusDTO statusDTO = new StatusDTO();

                    while (reservationsDTO.BookDate <= reservationsDTO.CheckOut)
                    {
                        statusDTO = reservationsDAL.InsertarReserva(reservationsDTO);

                        if (statusDTO.ErrorCode != "0" && statusDTO.ErrorCode != "-1")
                        {
                            bookRoomResponse.Status.ErrorCode = statusDTO.ErrorCode;
                            bookRoomResponse.Status.ErrorDescription = statusDTO.ErrorDescription + " Date " + reservationsDTO.BookDate.ToString("yyyy-MM-dd"); 
                            break;
                        }
                        else
                        {
                            bookRoomResponse.Status.ErrorCode = statusDTO.ErrorCode;
                            bookRoomResponse.Status.ErrorDescription = statusDTO.ErrorDescription;
                        }

                        reservationsDTO.BookDate = reservationsDTO.BookDate.AddDays(1);
                    }
                }
                else
                {
                    throw new Exception("Parametros de entrada vacios");
                }

            }
            catch (Exception exc)
            {

                Exception le_e;

                le_e = exc.InnerException != null ? exc.InnerException : exc;
                bookRoomResponse.Status.ErrorCode = "01";
                bookRoomResponse.Status.ErrorDescription = exc.InnerException != null ? "Error en la ejecucion del servicio" : exc.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO DannCarltonService:BookRoom");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;

            }

            return bookRoomResponse;
        }

        GetDannBranchResponse IDannCarltonServiceBusiness.GetDannBranch(GetDannBranchRequest prmgetDannBranchRequest)
        {
            GetDannBranchResponse getDannBranchResponse = new GetDannBranchResponse();
            getDannBranchResponse.Status = new Status();
            List<GetDannBranchResult> lstgetDannBranchResults = new List<GetDannBranchResult>();

            try
            {
                ReservationsDTO reservationsDTO = new ReservationsDTO();

                if (prmgetDannBranchRequest != null)
                {
                    ReservationsDAL reservationsDAL = new ReservationsDAL();
                    List<ReservationsDTO> lstreservationsDTOs;

                    lstreservationsDTOs = reservationsDAL.GetDannBranch(reservationsDTO);

                    if (lstreservationsDTOs != null)
                    {
                        getDannBranchResponse.Status.ErrorDescription = "0";

                        if (lstreservationsDTOs.Count > 0)
                        {
                            foreach (ReservationsDTO FEreservationsDTO in lstreservationsDTOs)
                            {

                                GetDannBranchResult getDannBranchResult;
                                getDannBranchResult = new GetDannBranchResult
                                {
                                    BranchName = FEreservationsDTO.BranchName,
                                    Id = FEreservationsDTO.ID,
                                    Address = FEreservationsDTO.Address,
                                    BranchCode = FEreservationsDTO.BranchCode,
                                    CiiuCode = FEreservationsDTO.CiiuCode,
                                    City = FEreservationsDTO.City,
                                    Phone = FEreservationsDTO.Phone,
                                    Stars = FEreservationsDTO.Stars
                                };

                                lstgetDannBranchResults.Add(getDannBranchResult);

                            }

                            getDannBranchResponse.Status.ErrorCode = "0";
                            getDannBranchResponse.Status.ErrorDescription = "Proceso Satisfactorio";
                            getDannBranchResponse.GetDannBranchResult = lstgetDannBranchResults.ToArray();
                        }
                        else
                        {
                            getDannBranchResponse.Status.ErrorDescription = "No hay Branch con los parametros ingresados";
                        }
                    }
                    else
                    {
                        getDannBranchResponse.Status.ErrorCode = "01";
                        getDannBranchResponse.Status.ErrorDescription = "Error en la ejecución del servicio";
                    }
                }
                else
                {
                    throw new Exception("Parametros de entrada vacios");
                }
            }
            catch (Exception exc)
            {
                Exception le_e;

                le_e = exc.InnerException != null ? exc.InnerException : exc;
                getDannBranchResponse.Status.ErrorCode = "01";
                getDannBranchResponse.Status.ErrorDescription = exc.InnerException != null ? "Error en la ejecucion del servicio" : exc.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO DannCarltonService:GetDannBranch");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;
            }


            return getDannBranchResponse;
        }

        GetRoomsByBranchResponse IDannCarltonServiceBusiness.GetRoomsByBranch(GetRoomsByBranchRequest prmgetRoomsByBranchRequest)
        {
            GetRoomsByBranchResponse getRoomsByBranchResponse = new GetRoomsByBranchResponse();
            getRoomsByBranchResponse.Status = new Status();
            List<GetRoomsByBranchResult> lstgetRoomsByBranchResults = new List<GetRoomsByBranchResult>();

            try
            {
                ReservationsDTO reservationsDTO = new ReservationsDTO();

                if (prmgetRoomsByBranchRequest != null)
                {
                    if (prmgetRoomsByBranchRequest.BranchCode != null && prmgetRoomsByBranchRequest.BranchCode.Trim().Length > 0)
                    {
                        reservationsDTO.BranchCode = prmgetRoomsByBranchRequest.BranchCode;
                    }
                    else
                    {
                        throw new Exception("Branch Code Obligatorio");
                    }

                    ReservationsDAL reservationsDAL = new ReservationsDAL();
                    List<ReservationsDTO> lstreservationsDTOs;

                    lstreservationsDTOs = reservationsDAL.GetRoomsByBranch(reservationsDTO);

                    if (lstreservationsDTOs != null)
                    {
                        getRoomsByBranchResponse.Status.ErrorDescription = "0";

                        if (lstreservationsDTOs.Count > 0)
                        {
                            foreach (ReservationsDTO FEreservationsDTO in lstreservationsDTOs)
                            {

                                GetRoomsByBranchResult getRoomsByBranchResult;
                                getRoomsByBranchResult = new GetRoomsByBranchResult
                                {
                                    Beds = FEreservationsDTO.Beds,
                                    Id = FEreservationsDTO.ID,
                                    BranchId = FEreservationsDTO.BranchId,
                                    Description = FEreservationsDTO.Description,
                                    Discount = FEreservationsDTO.Discount,
                                    Number = FEreservationsDTO.Number,
                                    Price = FEreservationsDTO.Price,
                                    RoomName = FEreservationsDTO.RoomName,
                                    Vat = FEreservationsDTO.Vat
                                };

                                lstgetRoomsByBranchResults.Add(getRoomsByBranchResult);

                            }

                            getRoomsByBranchResponse.Status.ErrorCode = "0";
                            getRoomsByBranchResponse.Status.ErrorDescription = "Proceso Satisfactorio";
                            getRoomsByBranchResponse.GetRoomsByBranchResult = lstgetRoomsByBranchResults.ToArray();
                        }
                        else
                        {
                            getRoomsByBranchResponse.Status.ErrorDescription = "No hay Rooms por el Branch ingresado";
                        }
                    }
                    else
                    {
                        getRoomsByBranchResponse.Status.ErrorCode = "01";
                        getRoomsByBranchResponse.Status.ErrorDescription = "Error en la ejecución del servicio";
                    }
                }
                else
                {
                    throw new Exception("Parametros de entrada vacios");
                }
            }
            catch (Exception exc)
            {
                Exception le_e;

                le_e = exc.InnerException != null ? exc.InnerException : exc;
                getRoomsByBranchResponse.Status.ErrorCode = "01";
                getRoomsByBranchResponse.Status.ErrorDescription = exc.InnerException != null ? "Error en la ejecucion del servicio" : exc.Message;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN LA CAPA DE NEGOCIO DannCarltonService:GetRoomsByBranch");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + le_e.Message);
                throw le_e;
            }

            return getRoomsByBranchResponse;
        }
    }
}