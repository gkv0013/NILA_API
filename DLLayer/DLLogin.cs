using DllLayer.PgSqlHelper;

using Npgsql;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DLLayer
{
    public class DLLogin
    {
        public DataTable? Login(DataTable data, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "user_login");
                MapDataToParameters(data, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("user_login", parameters, connection, "result");
                return result.Tables["result"];
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object? Register(DataTable data,IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "user_registration");
                MapDataToParameters(data, parameters);
                DataSet result = PgsqlHelper.ExecuteFunction("user_registration", parameters, connection, "result");
                return result.Tables["result"]?? null;
            } catch(Exception ex)
            {
                throw ex;
            }
 
        }
        public object? GetUsers(DataTable data, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "get_users_with_pagination");
                MapUserDataToParameters(data, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("get_users_with_pagination", parameters, connection, "result");
                return result.Tables["result"]?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void MapDataToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            // Iterate through each row in the DataTable
            foreach (DataRow row in data.Rows)
            {
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_telegramid":
                            parameter.Value = Convert.ToInt64(row["id"]);
                            break;
                        case "p_mode":
                            parameter.Value = Convert.ToInt32(row["mode"]);
                            break;
                        case "p_username":
                            parameter.Value = row["username"].ToString();
                            break;
                        case "p_firstname":
                            parameter.Value = row["firstname"].ToString();
                            break;
                        case "p_lastname":
                            parameter.Value = row["lastname"].ToString();
                            break;
                        case "p_lastlogin":
                            parameter.Value = Convert.ToDateTime(row["lastLogin"]);
                            break;
                        case "p_profit_per_tap":
                            parameter.Value = Convert.ToDecimal(row["profitPerTap"]);
                            break;
                        case "p_profit_per_hour":
                            parameter.Value = Convert.ToDecimal(row["profitPerHour"]);
                            break;
                        case "p_total_coins":
                            parameter.Value = Convert.ToInt64(row["totalCoins"]);
                            break;
                        case "p_friends_invited":
                            parameter.Value = Convert.ToInt32(row["friendsInvited"]);
                            break;
                        case "p_referral_bonus":
                            parameter.Value = Convert.ToDecimal(row["referralBonus"]);
                            break;
                        case "p_daily_login_streak":
                            parameter.Value = Convert.ToInt32(row["dailyLoginStreak"]);
                            break;
                        default:
                            // Handle or log any unexpected parameters if needed
                            break;
                    }
                }
            }
        }
        private void MapUserDataToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            // Iterate through each row in the DataTable
            foreach (DataRow row in data.Rows)
            {
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_pagenumber":
                            parameter.Value = Convert.ToInt32(row["pagenumber"]);
                            break;
                        case "p_pagesize":
                            parameter.Value = Convert.ToInt32(row["pagesize"]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public DataTable SaveImage(DataTable? dataTable, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "save_image");
                MapImageDataToParameters(dataTable, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("save_image", parameters, connection, "result");
                return result.Tables["result"]?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void MapImageDataToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            // Iterate through each row in the DataTable
            foreach (DataRow row in data.Rows)
            {
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_imagedata":
                            parameter.Value = row["imageblob"];
                            break;
                        case "p_type":
                            parameter.Value = Convert.ToInt32(row["type"]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
