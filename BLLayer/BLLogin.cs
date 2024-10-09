using DLLayer;
using DllLayer.PgSqlHelper;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLLayer
{
    public class BLLogin
    {
        public object? Image(DataSet? saveData)
        {
            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlLogin = new DLLogin();

                    DataTable result = dlLogin.SaveImage(saveData.Tables["image"], connection);
                 
                    return result;

                }
                catch (Exception ex)
                {
                    // Exception handling logic here.
                    throw ex;
                }
                finally
                {
                    PgsqlHelper.CloseConnection(connection);
                }
            }
        }

        public object? Login(DataTable data)
        {
            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlLogin = new DLLogin();
                    Dictionary<string, object> res = new Dictionary<string, object>();
                    DataTable result= dlLogin.Login(data, connection);
                    res.Add("result", result);
                    if (result.Rows.Count>0)
                    {
                        res.Add("cointsettings", dlLogin.GetSetttings(data, connection));
                        if (data.Columns.Contains("mode"))
                        {
                            foreach (DataRow row in data.Rows)
                            {
                                row["mode"] = 1;
                            }
                            res.Add("goalsdata", dlLogin.GetSetttings(data, connection));
                        }
                     
                    }
                    return res;

                }
                catch (Exception ex)
                {
                    // Exception handling logic here.
                    throw ex;
                }
                finally
                {
                    PgsqlHelper.CloseConnection(connection);
                }
            }
        }

        public object? Collect(DataTable data)
        {
            // IDbConnection is instantiated within a using statement.
            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlLogin = new DLLogin();
                    return dlLogin.Collect(data, connection);
                   
                   

                    
                }
                catch (Exception ex)
                {
                    // Exception handling logic here.
                    throw ex;
                }
                finally
                {
                PgsqlHelper.CloseConnection(connection);
                }
            }
        }
        public object? GetReferrer(DataTable data)
        {
            // IDbConnection is instantiated within a using statement.
            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    var dlLogin = new DLLogin();
                    DataTable initialResult = dlLogin.GetReferrer(data, connection) as DataTable;
                    result.Add("friends", initialResult);
                    if (initialResult != null && initialResult.Rows.Count > 0)
                    {
                        if (data.Columns.Contains("mode"))
                        {
                            data.Rows[0]["mode"] = 2;
                            result.Add("Totalrewards", dlLogin.GetReferrer(data, connection));
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    // Exception handling logic here.
                    throw ex;
                }
                finally
                {
                    PgsqlHelper.CloseConnection(connection);
                }
            }
        }
    }

}
