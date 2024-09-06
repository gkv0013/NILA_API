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
                    DataTable result= dlLogin.Login(data, connection);
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
                    var dlLogin = new DLLogin();
                    return dlLogin.GetReferrer(data, connection);
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
