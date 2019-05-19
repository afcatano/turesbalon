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
        string strWherePag;
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

                strPLSQL = "SELECT A.ID, A.IDTypeIdent, A.CUSTID, A.FNAME, A.LNAME, A.EMAIL, A.PHONENUMBER, A.ADDRESS, A.CITY, A.COUNTRY, A.USUARIO, A.PASSWORD, D.STATUS, " +
                       "E.CREDITCARDTYPE, C.CREDITCARDNUMBER, C.CARDNAME, C.FVENCE, C.CODESECURITY " +
                       "FROM CUSTOMER A " +
                       "LEFT JOIN customercreditcards C ON A.ID = C.IDCUSTOMER " +
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
                                ExpirationDate = Convert.ToString(dataRowClientes["FVENCE"]),
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
                                ID = Convert.ToInt32(dataRowClientes["ID"]),
                                CustID = Convert.ToInt32(dataRowClientes["CUSTID"]),
                                FName = Convert.ToString(dataRowClientes["FNAME"]),
                                LName = Convert.ToString(dataRowClientes["LNAME"]),
                                CodTypeIdent = Convert.ToString(dataRowClientes["IDTYPEIDENT"]),
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

        public long InsertarCliente(ClientesDTO ac_cliente)
        {

            long lb_respuesta;
            lb_respuesta = 0;

            try
            {

                string ls_sql;
                OracleServerHelper losh_conection;
                DataSet lds_datos;

                ls_sql = "SELECT * FROM CUSTOMER WHERE USUARIO = '" + ac_cliente.User.ToString() + "'";
                losh_conection = new OracleServerHelper();
                lds_datos = losh_conection.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                if (lds_datos != null && lds_datos.Tables[0].Rows.Count > 0)
                {
                    throw new Exception("Ya existe un usuario con el LOGIN ingresado");
                }
                else
                {

                    DataSet dsAffected;
                    long tmpID = 0;
                    int ll_affected;
                    int tmprsta;

                    ls_sql = "INSERT INTO CUSTOMER (CUSTID,FNAME,LNAME,PHONENUMBER,EMAIL,PASSWORD,IDSTATUS,";
                    ls_sql += "ADDRESS,CITY,COUNTRY,USUARIO,IDTYPEIDENT) VALUES(" + ac_cliente.CustID.ToString();
                    ls_sql += ",'" + ac_cliente.FName + "','" + ac_cliente.LName + "',";
                    ls_sql += ac_cliente.PhoneNumber == null ? "NULL," : "'" + ac_cliente.PhoneNumber + "',";
                    ls_sql += ac_cliente.Email == null ? "NULL," : "'" + ac_cliente.Email + "',";
                    ls_sql += "'" + ac_cliente.Password + "',1,'" + ac_cliente.Address + "',";
                    ls_sql += "'" + ac_cliente.City + "','" + ac_cliente.Country + "',";
                    ls_sql += "'" + ac_cliente.User + "'," + ac_cliente.CodTypeIdent + ")";

                    tmprsta = losh_conection.ExecuteSql(ls_sql, new List<OracleParameter>());

                    if (tmprsta > 0)
                    {
                        ls_sql = "SELECT MAX(ID) FROM Customer";
                        dsAffected = losh_conection.ExecuteSqlToDataSet(ls_sql, new List<OracleParameter>());

                        if (dsAffected != null && dsAffected.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRowClientes in dsAffected.Tables[0].Rows)
                            {
                                tmpID = Convert.ToInt32(dataRowClientes["MAX(ID)"]);
                            }
                        }
                        else
                        {
                            tmpID = 0;
                        }

                        if (tmpID != 0)
                        {

                            if (ac_cliente.LCreditCard != null)
                            {

                                if (ac_cliente.LCreditCard.Count > 0)
                                {

                                    foreach (CreditCardDTO lcc_creditCard in ac_cliente.LCreditCard)
                                    {
                                        ls_sql = "INSERT INTO CUSTOMERCREDITCARDS(CREDITCARDNUMBER,IDCUSTOMER,IDCREDITCARDTYPE,";
                                        ls_sql += "FVENCE,CODESECURITY,CARDNAME) VALUES(" + lcc_creditCard.Number + ",";
                                        ls_sql += tmpID.ToString() + "," + lcc_creditCard.Type + ",";
                                        ls_sql += "'" + lcc_creditCard.ExpirationDate + "'," + lcc_creditCard.SecurityCode + ",";
                                        ls_sql += "'" + lcc_creditCard.CardName + "')";

                                        ll_affected = losh_conection.ExecuteSql(ls_sql, new List<OracleParameter>());

                                            if (ll_affected <= 0)
                                            {
                                                throw new Exception("Error ingresando tarjeta de credito");
                                            }
                                    }

                                    lb_respuesta = tmpID;

                                }
                                else
                                    lb_respuesta = tmpID;

                            }
                            else
                                lb_respuesta = tmpID;
                        }
                        else
                        {
                            lb_respuesta = tmpID;
                        }
                    }
                    else
                    {
                        lb_respuesta = tmpID;
                    }
                }
            }
            catch (Exception ae_e)
            {
                lb_respuesta = 0;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN DAL Clientes:InsertarCliente");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ae_e.Message);
                throw ae_e;
            }

            return lb_respuesta;

        }

        public int LoginClientes(ClientesDTO clientesDTO)
        {
            int rsta;
            DataSet dsLogin;
            try
            {
                strPLSQL = "SELECT CUSTID FROM CUSTOMER WHERE USUARIO = '" + clientesDTO.User.ToString() + "' AND PASSWORD = '" + clientesDTO.Password.ToString() + "'";
                OracleServerHelper OrclConection = new OracleServerHelper();

                dsLogin = OrclConection.ExecuteSqlToDataSet(strPLSQL, new List<OracleParameter>());

                if (dsLogin != null && dsLogin.Tables[0].Rows.Count > 0)
                {
                    rsta = 1;
                }
                else
                {
                    rsta = 0;
                }
            }
            catch (Exception ex)
            {
                rsta = 0;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN DAL Clientes: LOGIN");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ex.Message);
            }

            return rsta;
        }

        public List<ClientesDTO> GetClientesPaginado(ClientesDTO clientesDTO)
        {
            string strPLSQL2;
            List<ClientesDTO> LstClientesDTOs = new List<ClientesDTO>();
            DataSet dsClientes;
            DataSet dsCuenta;
            int tmpRegsTotales = 0;
            OracleServerHelper OrclConection = new OracleServerHelper();
            try
            {
                strWhere = ConfiguracionParametrosGet(clientesDTO);
                strWherePag = ConfiguracionParametrosGetPaginado(clientesDTO);

                strPLSQL = "SELECT AX.* FROM( " +
                        "SELECT rownum registro, X.* FROM( " +
                        "SELECT A.ID, E.TypeIdent, A.CUSTID, A.FNAME, A.LNAME, A.EMAIL, A.PHONENUMBER, A.ADDRESS, A.CITY, A.COUNTRY, D.STATUS " +
                        "FROM CUSTOMER A " +
                        "INNER JOIN TypeIdent E ON A.IDTypeIdent = E.CodTypeIdent " +
                        "LEFT JOIN Status D ON A.IDSTATUS = D.CodStatus " + strWhere +
                        " ORDER BY ID) X ) AX " + strWherePag;

                if (clientesDTO.RegsTotales == 0)
                {
                    strPLSQL2 = "SELECT COUNT(*) RegsTotales " +
                        "FROM CUSTOMER A " +
                        "INNER JOIN TypeIdent E ON A.IDTypeIdent = E.CodTypeIdent " +
                        "LEFT JOIN Status D ON A.IDSTATUS = D.CodStatus " + strWhere;

                    dsCuenta = OrclConection.ExecuteSqlToDataSet(strPLSQL2, Lstparameters);

                    if (dsCuenta != null && dsCuenta.Tables[0].Rows.Count == 1)
                    {
                        foreach (DataRow ldr_temp in dsCuenta.Tables[0].Rows)
                        {
                            tmpRegsTotales = Convert.ToInt32(ldr_temp["RegsTotales"]);
                        }
                    }
                }

                dsClientes = OrclConection.ExecuteSqlToDataSet(strPLSQL, Lstparameters);
                if (dsClientes != null && dsClientes.Tables.Count > 0)
                {
                    foreach (DataRow dataRowClientes in dsClientes.Tables[0].Rows)
                    {
                        ClientesDTO lclientesDTO;
                        CreditCardDTO creditCardDTO = new CreditCardDTO();
                        creditCardDTO = null;
                        
                        lclientesDTO = new ClientesDTO
                        {
                            ID = Convert.ToInt32(dataRowClientes["ID"]),
                            CustID = Convert.ToInt32(dataRowClientes["CUSTID"]),
                            FName = Convert.ToString(dataRowClientes["FNAME"]),
                            LName = Convert.ToString(dataRowClientes["LNAME"]),
                            CodTypeIdent = Convert.ToString(dataRowClientes["TYPEIDENT"]),
                            PhoneNumber = Convert.ToString(dataRowClientes["PHONENUMBER"]),
                            Email = Convert.ToString(dataRowClientes["EMAIL"]),
                            Address = Convert.ToString(dataRowClientes["ADDRESS"]),
                            Country = Convert.ToString(dataRowClientes["COUNTRY"]),
                            City = Convert.ToString(dataRowClientes["CITY"]),
                            //User = Convert.ToString(dataRowClientes["USUARIO"]),
                            Status = Convert.ToString(dataRowClientes["STATUS"]),
                            RegsTotales = tmpRegsTotales==0 ? clientesDTO.RegsTotales : tmpRegsTotales,
                            LCreditCard = new List<CreditCardDTO>()
                        };
                        LstClientesDTOs.Add(lclientesDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                LstClientesDTOs = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN DAL Clientes: GetClientesPaginado");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ex.Message);
            }


            return LstClientesDTOs;
        }

        public List<ClientesDTO> GetClientesPaginadoxEvento(ClientesDTO clientesDTO)
        {
            string strPLSQL2;
            List<ClientesDTO> LstClientesDTOs = new List<ClientesDTO>();
            DataSet dsClientes;
            DataSet dsCuenta;
            int tmpRegsTotales = 0;
            OracleServerHelper OrclConection = new OracleServerHelper();
            try
            {
                //strWhere = "WHERE O.EVENTCODE LIKE '%" + clientesDTO.Evento.Trim() + "% ' OR O.EVENTNAME LIKE '%" + clientesDTO.Evento.Trim() + "%'";
                strWhere = "WHERE O.EVENTCODE = '" + clientesDTO.Evento.Trim() + "'";
                strWherePag = ConfiguracionParametrosGetPaginado(clientesDTO);

                strPLSQL = "SELECT AX.* FROM( " +
                        "SELECT ROWNUM registro, X.* FROM( " +
                        "SELECT DISTINCT A.ID, E.TypeIdent, A.CUSTID, A.FNAME, A.LNAME, A.EMAIL, A.PHONENUMBER, A.ADDRESS, A.CITY, A.COUNTRY, D.STATUS " +
                        "FROM ORDERS O " +
                        "INNER JOIN CUSTOMER A ON O.IDCustomer = A.ID " +
                        "INNER JOIN TypeIdent E ON A.IDTypeIdent = E.CodTypeIdent " +
                        "LEFT JOIN Status D ON A.IDSTATUS = D.CodStatus " + strWhere +
                        " ORDER BY ID) X ) AX " + strWherePag;

                if (clientesDTO.RegsTotales == 0)
                {
                    strPLSQL2 = "SELECT COUNT(DISTINCT A.ID) RegsTotales " +
                        "FROM ORDERS O " +
                        "INNER JOIN CUSTOMER A ON O.IDCustomer = A.ID " +
                        "INNER JOIN TypeIdent E ON A.IDTypeIdent = E.CodTypeIdent " +
                        "LEFT JOIN Status D ON A.IDSTATUS = D.CodStatus " + strWhere;

                    dsCuenta = OrclConection.ExecuteSqlToDataSet(strPLSQL2, new List<OracleParameter>());

                    if (dsCuenta != null && dsCuenta.Tables[0].Rows.Count == 1)
                    {
                        foreach (DataRow ldr_temp in dsCuenta.Tables[0].Rows)
                        {
                            tmpRegsTotales = Convert.ToInt32(ldr_temp["RegsTotales"]);
                        }
                    }
                }

                dsClientes = OrclConection.ExecuteSqlToDataSet(strPLSQL, Lstparameters);
                if (dsClientes != null && dsClientes.Tables.Count > 0)
                {
                    foreach (DataRow dataRowClientes in dsClientes.Tables[0].Rows)
                    {
                        ClientesDTO lclientesDTO;
                        CreditCardDTO creditCardDTO = new CreditCardDTO();
                        creditCardDTO = null;

                        lclientesDTO = new ClientesDTO
                        {
                            ID = Convert.ToInt32(dataRowClientes["ID"]),
                            CustID = Convert.ToInt32(dataRowClientes["CUSTID"]),
                            FName = Convert.ToString(dataRowClientes["FNAME"]),
                            LName = Convert.ToString(dataRowClientes["LNAME"]),
                            CodTypeIdent = Convert.ToString(dataRowClientes["TYPEIDENT"]),
                            PhoneNumber = Convert.ToString(dataRowClientes["PHONENUMBER"]),
                            Email = Convert.ToString(dataRowClientes["EMAIL"]),
                            Address = Convert.ToString(dataRowClientes["ADDRESS"]),
                            Country = Convert.ToString(dataRowClientes["COUNTRY"]),
                            City = Convert.ToString(dataRowClientes["CITY"]),
                            //User = Convert.ToString(dataRowClientes["USUARIO"]),
                            Status = Convert.ToString(dataRowClientes["STATUS"]),
                            RegsTotales = tmpRegsTotales == 0 ? clientesDTO.RegsTotales : tmpRegsTotales,
                            LCreditCard = new List<CreditCardDTO>()
                        };
                        LstClientesDTOs.Add(lclientesDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                LstClientesDTOs = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN DAL Clientes: GetClientesPaginadoxEvento");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ex.Message);
            }


            return LstClientesDTOs;
        }

        public List<ClientesDTO> GetClientesPaginadoxFechaFact(ClientesDTO clientesDTO)
        {
            string strPLSQL2;
            List<ClientesDTO> LstClientesDTOs = new List<ClientesDTO>();
            DataSet dsClientes;
            DataSet dsCuenta;
            int tmpRegsTotales = 0;
            OracleServerHelper OrclConection = new OracleServerHelper();
            try
            {
                strWhere = "WHERE O.ORDERSTATUS = 'PA' AND O.ORDERDATE BETWEEN TO_DATE ('" + clientesDTO.FechaIniFact.ToString("yyyy-MM-dd") + "', 'yyyy/mm/dd') AND TO_DATE ('" + clientesDTO.FechaFinFact.ToString("yyyy-MM-dd") + "', 'yyyy/mm/dd')";
                strWherePag = ConfiguracionParametrosGetPaginado(clientesDTO);

                strPLSQL = "SELECT AX.* FROM( " +
                        "SELECT ROWNUM registro, X.* FROM( " +
                        "SELECT SUM(O.ORDERVALUE) ORDERVALUE, A.ID, E.TypeIdent, A.CUSTID, A.FNAME, A.LNAME, A.EMAIL, A.PHONENUMBER, A.ADDRESS, A.CITY, A.COUNTRY, D.STATUS " +
                        "FROM ORDERS O " +
                        "INNER JOIN CUSTOMER A ON O.IDCustomer = A.ID " +
                        "INNER JOIN TypeIdent E ON A.IDTypeIdent = E.CodTypeIdent " +
                        "LEFT JOIN Status D ON A.IDSTATUS = D.CodStatus " +
                        strWhere + " GROUP BY  A.ID, E.TypeIdent, A.CUSTID, A.FNAME, A.LNAME, A.EMAIL, A.PHONENUMBER, A.ADDRESS, A.CITY, A.COUNTRY, D.STATUS) X) AX " + strWherePag +
                        " ORDER BY AX.ORDERVALUE DESC";

                if (clientesDTO.RegsTotales == 0)
                {
                    strPLSQL2 = "SELECT COUNT(SUM(O.ORDERVALUE)) RegsTotales " +
                        "FROM ORDERS O " +
                        "INNER JOIN CUSTOMER A ON O.IDCustomer = A.ID " +
                        "INNER JOIN TypeIdent E ON A.IDTypeIdent = E.CodTypeIdent " +
                        "LEFT JOIN Status D ON A.IDSTATUS = D.CodStatus " + strWhere +
                        " GROUP BY  A.ID, E.TypeIdent, A.CUSTID, A.FNAME, A.LNAME, A.EMAIL, A.PHONENUMBER, A.ADDRESS, A.CITY, A.COUNTRY, D.STATUS";
                                
                    dsCuenta = OrclConection.ExecuteSqlToDataSet(strPLSQL2, Lstparameters);

                    if (dsCuenta != null && dsCuenta.Tables[0].Rows.Count == 1)
                    {
                        foreach (DataRow ldr_temp in dsCuenta.Tables[0].Rows)
                        {
                            tmpRegsTotales = Convert.ToInt32(ldr_temp["RegsTotales"]);
                        }
                    }
                }

                dsClientes = OrclConection.ExecuteSqlToDataSet(strPLSQL, Lstparameters);
                if (dsClientes != null && dsClientes.Tables.Count > 0)
                {
                    foreach (DataRow dataRowClientes in dsClientes.Tables[0].Rows)
                    {
                        ClientesDTO lclientesDTO;
                        CreditCardDTO creditCardDTO = new CreditCardDTO();
                        creditCardDTO = null;

                        lclientesDTO = new ClientesDTO
                        {
                            ID = Convert.ToInt32(dataRowClientes["ID"]),
                            CustID = Convert.ToInt32(dataRowClientes["CUSTID"]),
                            FName = Convert.ToString(dataRowClientes["FNAME"]),
                            LName = Convert.ToString(dataRowClientes["LNAME"]),
                            CodTypeIdent = Convert.ToString(dataRowClientes["TYPEIDENT"]),
                            PhoneNumber = Convert.ToString(dataRowClientes["PHONENUMBER"]),
                            Email = Convert.ToString(dataRowClientes["EMAIL"]),
                            Address = Convert.ToString(dataRowClientes["ADDRESS"]),
                            Country = Convert.ToString(dataRowClientes["COUNTRY"]),
                            City = Convert.ToString(dataRowClientes["CITY"]),
                            //User = Convert.ToString(dataRowClientes["USUARIO"]),
                            Status = Convert.ToString(dataRowClientes["STATUS"]),
                            RegsTotales = tmpRegsTotales == 0 ? clientesDTO.RegsTotales : tmpRegsTotales,
                            TotalFacturado = Convert.ToDecimal(dataRowClientes["ORDERVALUE"]),
                            LCreditCard = new List<CreditCardDTO>()
                        };
                        LstClientesDTOs.Add(lclientesDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                LstClientesDTOs = null;
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, "ERROR EN DAL Clientes: GetClientesPaginadoxFechaFact");
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Error, " :: " + ex.Message);
            }

            return LstClientesDTOs;
        }

        private string ConfiguracionParametrosGet(ClientesDTO clientesDTO)
        {
            string strWhere = "WHERE 1=1";
            OracleParameter OrclParameters;

            if (clientesDTO.ID != 0)
            {
                OrclParameters = new OracleParameter("ID", OracleDbType.Int32);
                OrclParameters.Value = clientesDTO.ID;
                Lstparameters.Add(OrclParameters);
                strWhere = strWhere + " AND A.ID = :ID";
            }

            if (clientesDTO.CodTypeIdent != null)
            {
                if (clientesDTO.CodTypeIdent.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("IdType", OracleDbType.Int32);
                    OrclParameters.Value = clientesDTO.CodTypeIdent;
                    Lstparameters.Add(OrclParameters);
                    strWhere = strWhere + " AND A.IDTypeIdent = :IdType";
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

        private string ConfiguracionParametrosGetPaginado(ClientesDTO clientesDTO)
        {
            string strWhere = "WHERE AX.registro BETWEEN";

            if (clientesDTO.Pagina != 0 && clientesDTO.RegsxPagina != 0)
            {
                strWhere = strWhere + "(" + clientesDTO.Pagina.ToString() + " - 1) * " + clientesDTO.RegsxPagina.ToString() + " + 1 AND "+ clientesDTO.Pagina.ToString() + " * " + clientesDTO.RegsxPagina.ToString();
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

            if (clientesDTO.Password != null)
            {
                if (clientesDTO.User.Trim().Length > 0)
                {
                    OrclParameters = new OracleParameter("Password", OracleDbType.Varchar2);
                    OrclParameters.Value = clientesDTO.User;
                    Lstparameters.Add(OrclParameters);
                    strSET = strSET + " , PASSWORD = :Password";
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