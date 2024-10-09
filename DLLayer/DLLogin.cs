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

        public object? Collect(DataTable data,IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "user_login");
                MapCollectToParameters(data, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("user_login", parameters, connection, "result");
                return result.Tables["result"]?? null;
            } catch(Exception ex)
            {
                throw ex;
            }
 
        }
        private void MapCollectToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            foreach (DataRow row in data.Rows)
            {
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_telegramid":
                            parameter.Value = data.Columns.Contains("id") && row["id"] != DBNull.Value ? row["id"].ToString() : string.Empty;
                            break;
                        case "p_mode":
                            parameter.Value = data.Columns.Contains("mode") && row["mode"] != DBNull.Value ? Convert.ToInt32(row["mode"]) : 10;
                            break;
                        case "p_total_coins":
                            parameter.Value = data.Columns.Contains("claim") && row["claim"] != DBNull.Value ? Convert.ToInt64(row["claim"]) : 0;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public object? GetReferrer(DataTable data, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "get_referred_users");
                MapReferrerDataToParameters(data, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("get_referred_users", parameters, connection, "result");
                return result.Tables["result"]?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void MapDataToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            foreach (DataRow row in data.Rows)
            {
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_telegramid":
                            parameter.Value = data.Columns.Contains("id") && row["id"] != DBNull.Value ? row["id"].ToString() : string.Empty;
                            break;
                        case "p_mode":
                            parameter.Value = data.Columns.Contains("mode") && row["mode"] != DBNull.Value ? Convert.ToInt32(row["mode"]) : 10;
                            break;
                        case "p_username":
                            parameter.Value = data.Columns.Contains("username") && row["username"] != DBNull.Value ? row["username"].ToString() : string.Empty;
                            break;
                        case "p_firstname":
                            parameter.Value = data.Columns.Contains("firstname") && row["firstname"] != DBNull.Value ? row["firstname"].ToString() : "fa";
                            break;
                        case "p_lastname":
                            parameter.Value = data.Columns.Contains("lastname") && row["lastname"] != DBNull.Value ? row["lastname"].ToString() : "fa";
                            break;
                        case "p_lastlogin":
                            parameter.Value = data.Columns.Contains("lastLogin") && row["lastLogin"] != DBNull.Value ? Convert.ToDateTime(row["lastLogin"]) : DateTime.Now;
                            break;
                        case "p_profit_per_tap":
                            parameter.Value = data.Columns.Contains("profitPerTap") && row["profitPerTap"] != DBNull.Value ? Convert.ToDecimal(row["profitPerTap"]) : 0.0M;
                            break;
                        case "p_profit_per_hour":
                            parameter.Value = data.Columns.Contains("profitPerHour") && row["profitPerHour"] != DBNull.Value ? Convert.ToDecimal(row["profitPerHour"]) : 0.0M;
                            break;
                        case "p_total_coins":
                            parameter.Value = data.Columns.Contains("totalCoins") && row["totalCoins"] != DBNull.Value ? Convert.ToInt64(row["totalCoins"]) : 0;
                            break;
                        case "p_friends_invited":
                            parameter.Value = data.Columns.Contains("friendsInvited") && row["friendsInvited"] != DBNull.Value ? Convert.ToInt32(row["friendsInvited"]) : 0;
                            break;
                        case "p_referral_bonus":
                            parameter.Value = data.Columns.Contains("referralBonus") && row["referralBonus"] != DBNull.Value ? Convert.ToDecimal(row["referralBonus"]) : 0.0M;
                            break;
                        case "p_daily_login_streak":
                            parameter.Value = data.Columns.Contains("dailyLoginStreak") && row["dailyLoginStreak"] != DBNull.Value ? Convert.ToInt32(row["dailyLoginStreak"]) : 0;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void MapReferrerDataToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            foreach (DataRow row in data.Rows)
            {
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_mode":
                            parameter.Value = data.Columns.Contains("mode") && row["mode"] != DBNull.Value ? Convert.ToInt32(row["mode"]) : 10;
                            break;
                        case "p_referrer_id":
                            parameter.Value = data.Columns.Contains("telegramid") && row["telegramid"] != DBNull.Value ? row["telegramid"] : DBNull.Value;
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

        public DataTable GetSetttings(DataTable? dataTable, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "select_settings");
                MapSettingDataToParameters(dataTable, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("select_settings", parameters, connection, "result");
                return result.Tables["result"]?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void MapSettingDataToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            // Iterate through each row in the DataTable
            foreach (DataRow row in data.Rows)
            {
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_mode":
                            parameter.Value = data.Columns.Contains("mode") && row["mode"] != DBNull.Value ? Convert.ToInt32(row["mode"]) : 10;
                            break;
                        default:
                            break;
                    }
                }
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
