using BackEndsPICAWeb.Business.Clientes.DTO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CommonsWeb.DAL.Clientes
{
    public class ClientesDAL
    {

        List<OracleParameter> Lstparameters = new List<OracleParameter>();
        string strWhere;
        string strSET;
        string strPLSQL;
        string strWhereUpdate;

        public List<ClientesDTO> GetClientes(ClientesDTO clientesDTO)
        {

            List<ClientesDTO> LstClientesDTOs = new List<ClientesDTO>();
            DataSet dsClientes;

            try
            {

                strWhere = ConfiguracionParametrosGet(clientesDTO);

                strPLSQL = "SELECT B.TYPEIDENT, A.CUSTID, A.FNAME, A.LNAME, A.EMAIL, A.PHONENUMBER, A.ADDRESS, A.CITY, A.COUNTRY, A.USUARIO, A.PASSWORD, D.STATUS, " +
                       "E.CREDITCARDTYPE, C.CREDITCARDNUMBER, C.CARDNAME, C.FVENCE, C.CODESECURITY " +
                       "FROM CUSTOMER A " +
                       "LEFT JOIN TypeIdent B ON A.IDTypeIdent = B.CodTypeIdent " +
                       "LEFT JOIN customercreditcards C ON A.CUSTID = C.CUSTID " +
                       "LEFT JOIN Status D ON A.IDSTATUS = D.CodStatus " +
                       "LEFT JOIN CreditCardType E ON C.IDCREDITCARDTYPE = E.CodCreditCardType " + strWhere;

                OracleServerHelper OrclConection = new OracleServerHelper();

                dsClientes = OrclConection.ExecuteSqlToDataSet(strPLSQL, Lstparameters);

                if (dsClientes != null && dsClientes.Tables.Count > 0)
                {
                    foreach (DataRow dataRowClientes in dsClientes.Tables[0].Rows)
                    {
                        ClientesDTO lclientesDTO;
                        CreditCardDTO creditCardDTO = new CreditCardDTO();

                        lclientesDTO = LstClientesDTOs.Find(x => x.CustID == Convert.ToInt32(dataRowClientes["CUSTID"]));

                        if (Convert.ToString(dataRowClientes["CREDITCARDNUMBER"]).Trim().Length > 0)
                        {
                            creditCardDTO = new CreditCardDTO
                            {
                                Type = Convert.ToString(dataRowClientes["CREDITCARDTYPE"]),
                                Number = Convert.ToString(dataRowClientes["CREDITCARDNUMBER"]),
                                CardName = Convert.ToString(dataRowClientes["CARDNAME"]),
                                ExpirationDate = Convert.ToDouble(dataRowClientes["FVENCE"]),
                                SecurityCode = Convert.ToString(dataRowClientes["CODESECURITY"]),
                                StatusCard = ""
                            };
                        }
                        else
                        {
                            creditCardDTO = null;
                        }
                        if (lclientesDTO == null)
                        {
                            lclientesDTO = new ClientesDTO
                            {
                                CustID = Convert.ToInt32(dataRowClientes["CUSTID"]),
                                FName = Convert.ToString(dataRowClientes["FNAME"]),
                                LName = Convert.ToString(dataRowClientes["LNAME"]),
                                CodTypeIdent = Convert.ToString(dataRowClientes["TYPEIDENT"]),
                                PhoneNumber = Convert.ToString(dataRowClientes["PHONENUMBER"]),
                                Email = Convert.ToString(dataRowClientes["EMAIL"]),
                                Address = Convert.ToString(dataRowClientes["ADDRESS"]),
                                Country = Convert.ToString(dataRowClientes["COUNTRY"]),
                                City = Convert.ToString(dataRowClientes["CITY"]),
                                User = Convert.ToString(dataRowClientes["USUARIO"]),
                                Password = Convert.ToString(dataRowClientes["PASSWORD"]),
                                Status = Convert.ToString(dataRowClientes["STATUS"]),
                                LCreditCard = new List<CreditCardDTO>()
                            };

                            if (creditCardDTO != null)
                            {
                                lclientesDTO.LCreditCard.Add(creditCardDTO);
                            }
                            LstClientesDTOs.Add(lclientesDTO);
                        }
                        else
                        {

                            if (!lclientesDTO.LCreditCard.Contains(creditCardDTO))
                                lclientesDTO.LCreditCard.Add(creditCardDTO);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LstClientesDTOs = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN DAL Clientes:");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ex.Message);
            }


            return LstClientesDTOs;
        }

        public int UpdateClientes(ClientesDTO clientesDTO)
        {
            int rsta;
            try
            {
                strSET = ConfiguracionParametrosUPDATE(clientesDTO);
                strPLSQL = "UPDATE CUSTOMER " +
                        strSET + " " + strWhereUpdate;

                OracleServerHelper OrclConection = new OracleServerHelper();

                rsta = OrclConection.ExecuteSql(strPLSQL, Lstparameters);
            }
            catch (Exception ex)
            {
                rsta = 0;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN DAL Clientes: UPDATE");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ex.Message);
            }

            return rsta;
        }

        public bool InsertarCliente(ClientesDTO ac_cliente)
        {

            bool lb_respuesta;

            lb_respuesta = false;

            try
            {

                string ls_sql;
                OracleServerHelper losh_conection;
                DataSet lds_datos;

                ls_sql = "SELECT * FROM CUSTOMER WHERE CUSTID = " + ac_cliente.CustID.ToString();
                losh_conection = new OracleServerHelper();
                lds_datos = losh_conection.ExecuteSqlToDataSet(strPLSQL, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables.Count > 0)
                {
                    throw new Exception("Ya existe un usuario con la información ingresada");
                }
                else
                {

                    long ll_affected;

                    ls_sql = "INSERT INTO CUSTOMER (CUSTID,FNAME,LNAME,PHONENUMBER,EMAIL,PASSWORD,IDSTATUS,";
                    ls_sql += "ADDRESS,CITY,COUNTRY,USUARIO,IDTYPEIDENT) VALUES(" + ac_cliente.CustID.ToString();
                    ls_sql += ",'" + ac_cliente.FName + "','" + ac_cliente.LName + "',";
                    ls_sql += ac_cliente.PhoneNumber == null ? "NULL," : "'" + ac_cliente.PhoneNumber + "',";
                    ls_sql += ac_cliente.Email == null ? "NULL," : "'" + ac_cliente.Email + "',";
                    ls_sql += "'" + ac_cliente.Email + "',1,'" + ac_cliente.Address + "',";
                    ls_sql += "'" + ac_cliente.City + "','" + ac_cliente.Country + "',";
                    ls_sql += "'" + ac_cliente.User + "'," + ac_cliente.CodTypeIdent + ")";
                    ll_affected = losh_conection.ExecuteSql(ls_sql, new List<OracleParameter>());

                    if (ll_affected > 0)
                    {

                        if (ac_cliente.LCreditCard != null)
                        {

                            if (ac_cliente.LCreditCard.Count > 0)
                            {

                                foreach (CreditCardDTO lcc_creditCard in ac_cliente.LCreditCard)
                                {

                                    ls_sql = "SELECT * FROM CUSTOMERCREDITCARDS WHERE CREDITCARDNUMBER = " + lcc_creditCard.Number;
                                    lds_datos = losh_conection.ExecuteSqlToDataSet(strPLSQL, new List<OracleParameter>());

                                    if (lds_datos != null && lds_datos.Tables.Count > 0)
                                    {

                                        throw new Exception("Ya existe una tarjeta de credito con los datos ingresados");
                                        //ls_sql = "UPDATE CUSTOMERCREDITCARDS SET";
                                        //ls_sql += " CUSTID = " + ac_cliente.CustID.ToString() + ",";
                                        //ls_sql += " IDCREDITCARDTYPE = " + lcc_creditCard.Type + ",";
                                        //ls_sql += " FVENCE = " + lcc_creditCard.ExpirationDate + ",";
                                        //ls_sql += " CODESECURITY = " + lcc_creditCard.SecurityCode + ",";
                                        //ls_sql += " CARDNAME = '" + lcc_creditCard.CardName + "'";
                                        //ls_sql += " WHERE CREDITCARDNUMBER = " + lcc_creditCard.Number;

                                    }
                                    else
                                    {

                                        ls_sql = "INSERT INTO CUSTOMERCREDITCARDS(CREDITCARDNUMBER,CUSTID,IDCREDITCARDTYPE,";
                                        ls_sql += "FVENCE,CODESECURITY,CARDNAME) VALUES(" + lcc_creditCard.Number + ",";
                                        ls_sql += ac_cliente.CustID.ToString() + "," + lcc_creditCard.Type + ",";
                                        ls_sql += lcc_creditCard.ExpirationDate + "," + lcc_creditCard.SecurityCode + ",";
                                        ls_sql += "'" + lcc_creditCard.CardName + "')";

                                        ll_affected = losh_conection.ExecuteSql(ls_sql, new List<OracleParameter>());

                                        if (ll_affected <= 0)
                                        {
                                            throw new Exception("Error ingresando tarjeta de credito");
                                        }

                                    }

                                }

                                lb_respuesta = true;

                            }
                            else
                                lb_respuesta = true;

                        }
                        else
                            lb_respuesta = true;

                    }


                }

            }
            catch (Exception ae_e)
            {
                lb_respuesta = false;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN DAL Clientes:InsertarCliente");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);
                throw ae_e;
            }

            return lb_respuesta;

        }


        private string ConfiguracionParametrosGet(ClientesDTO clientesDTO)
        {
            string strWhere = "WHERE 1=1";
            OracleParameter OrclParameters;

            if (clientesDTO.CodTypeIdent != null)
            {
                if (clientesDTO.CodTypeIdent.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("IdType", OracleDbType.Int32);
                    OrclParameters.Value = clientesDTO.CodTypeIdent;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND B.CodTypeIdent = :IdType";
                }
            }

            if (clientesDTO.CustID != 0)
            {
                OrclParameters = new OracleParameter("IdNumber", OracleDbType.Varchar2);
                OrclParameters.Value = clientesDTO.CustID;
                Lstparameters.Add(OrclParameters);
                strWhere = strWhere + " AND A.CUSTID = :IdNumber";
            }

            if (clientesDTO.FName != null)
            {
                if (clientesDTO.FName.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("FirstName", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.FName;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.FNAME = :FirstName";
                }
            }

            if (clientesDTO.LName != null)
            {
                if (clientesDTO.LName.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("LastNames", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.LName;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.LNAME = :LastNames";
                }
            }

            if (clientesDTO.Email != null)
            {
                if (clientesDTO.Email.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Email", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.Email;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.EMAIL = :Email";
                }
            }


            if (clientesDTO.PhoneNumber != null)
            {
                if (clientesDTO.PhoneNumber.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("PhoneNumber", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.PhoneNumber;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.PHONENUMBER = :PhoneNumber";
                }
            }

            if (clientesDTO.Address != null)
            {
                if (clientesDTO.Address.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Address", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.Address;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.ADDRESS = :Address";
                }
            }

            if (clientesDTO.City != null)
            {
                if (clientesDTO.City.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("City", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.City;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.CITY = :City";
                }
            }

            if (clientesDTO.Country != null)
            {
                if (clientesDTO.Country.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Country", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.Country;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.COUNTRY = :Country";
                }
            }

            if (clientesDTO.User != null)
            {
                if (clientesDTO.User.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Usuario", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.User;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.USUARIO = :Usuario";
                }
            }

            if (clientesDTO.Status != null)
            {
                if (clientesDTO.Status.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("StatusCustomer", OracleDbType.Int32);
                    OrclParameters.Value = clientesDTO.Status;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND D.CodStatus = :StatusCustomer";
                }
            }


            if (clientesDTO.Password != null)
            {
                if (clientesDTO.Password.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Password", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.Password;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.PASSWORD = :Password";
                }
            }
            return strWhere;
        }

        private string ConfiguracionParametrosUPDATE(ClientesDTO clientesDTO)
        {
            string strSET = "";
            OracleParameter OrclParameters;

            if (clientesDTO.CustID != 0)
            {
                OrclParameters = new OracleParameter("IdNumber", OracleDbType.Int32);
                OrclParameters.Value = clientesDTO.CustID;
                Lstparameters.Add(OrclParameters);
                strSET = "SET CUSTID = :IdNumber";
                strWhereUpdate = "WHERE CUSTID = :IdNumber";
            }

            if (clientesDTO.CodTypeIdent != null)
            {
                if (clientesDTO.CodTypeIdent.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("IdType", OracleDbType.Int32);
                    OrclParameters.Value = clientesDTO.CodTypeIdent;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , IDTYPEIDENT = :IdType";
                }
            }

            if (clientesDTO.FName != null)
            {
                if (clientesDTO.FName.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("FirstName", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.FName;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , FNAME = :FirstName";
                }
            }

            if (clientesDTO.LName != null)
            {
                if (clientesDTO.LName.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("LastNames", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.LName;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , LNAME = :LastNames";
                }
            }

            if (clientesDTO.Email != null)
            {
                if (clientesDTO.Email.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Email", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.Email;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , EMAIL = :Email";
                }
            }


            if (clientesDTO.PhoneNumber != null)
            {
                if (clientesDTO.PhoneNumber.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("PhoneNumber", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.PhoneNumber;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , PHONENUMBER = :PhoneNumber";
                }
            }

            if (clientesDTO.Address != null)
            {
                if (clientesDTO.Address.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Address", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.Address;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , ADDRESS = :Address";
                }
            }

            if (clientesDTO.City != null)
            {
                if (clientesDTO.City.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("City", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.City;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , CITY = :City";
                }
            }

            if (clientesDTO.Country != null)
            {
                if (clientesDTO.Country.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Country", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.Country;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , COUNTRY = :Country";
                }
            }

            if (clientesDTO.User != null)
            {
                if (clientesDTO.User.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Usuario", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.User;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , USUARIO = :Usuario";
                }
            }

            if (clientesDTO.Status != null)
            {
                if (clientesDTO.Status.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("StatusCustomer", OracleDbType.Int32);
                    OrclParameters.Value = clientesDTO.Status;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , IDSTATUS = :StatusCustomer";
                }
            }

            return strSET;
        }
    }
}