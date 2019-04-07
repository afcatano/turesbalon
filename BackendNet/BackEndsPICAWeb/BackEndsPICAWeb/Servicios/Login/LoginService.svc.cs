using BackEndsPICAWeb.Business;
using BackEndsPICAWeb.Business.Clientes;
using BackEndsPICAWeb.Business.Clientes.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BackEndsPICAWeb.Servicios.Login
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "LoginService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione LoginService.svc o LoginService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class LoginService : ILoginService
    {
        GetLoginResponse ILoginService.LoginResponse(GetLoginRequest prmLoginRequest)
        {
            GetLoginResponse loginResponse = new GetLoginResponse();

            try
            {
                ClientesDTO clientesDTO;
                ICustomerServiceBusiness iCSBusiness;

                clientesDTO = new ClientesDTO
                {
                    User = prmLoginRequest.Login.User,
                    Password = prmLoginRequest.Login.Password
                };

                iCSBusiness = new CustomerServicesBusiness();
                loginResponse = iCSBusiness.GetResultLoginDTOs(clientesDTO);
            }
            catch (Exception ex)
            {
                loginResponse.status.CodeResp = "01";
                loginResponse.status.MessageResp = "Error en el Servicio";
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN EL SERVICIO CustomerService:LoginCustomer " + ex.Message);
                throw ex;
            }


            return loginResponse;
        }
    }
}
