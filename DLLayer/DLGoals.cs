using DllLayer.PgSqlHelper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLayer
{
    public class DLGoals
    {
        public object? GoalsInsert(DataTable referralData, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "insert_goal_achievement");

                MapGoalstDataToParameters(referralData, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("insert_goal_achievement", parameters, connection, "result");
                return result.Tables["result"] ?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object? BoostLog(DataTable referralData, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "insert_goal_achievement");

                MapBoostLogToParameters(referralData, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("insert_goal_achievement", parameters, connection, "result");
                return result.Tables["result"] ?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void MapBoostLogToParameters(DataTable data, NpgsqlParameter[] parameters)
        {

            foreach (DataRow row in data.Rows)
            {

                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_telegramid":
                            parameter.Value = data.Columns.Contains("telegramId") && row["telegramId"] != DBNull.Value ? row["telegramId"].ToString() : string.Empty;
                            break;

                        case "p_mode":
                            parameter.Value =  data.Columns.Contains("mode") && row["mode"] != DBNull.Value ? Convert.ToInt32(row["mode"]) : 10;
                            break;
                        case "p_goalid":
                            parameter.Value = data.Columns.Contains("goalid") && row["goalid"] != DBNull.Value ? Convert.ToInt32(row["goalid"]) : 0;
                            break;
                        case "p_goaltypeid":
                            parameter.Value = data.Columns.Contains("goaltypeid") && row["goaltypeid"] != DBNull.Value ? Convert.ToInt32(row["goaltypeid"]) : 0;
                            break;
                        case "p_goaltype":
                            parameter.Value = data.Columns.Contains("goaltype") && row["goaltype"] != DBNull.Value ? row["goaltype"].ToString() : "";
                            break;
                        case "p_time":
                            parameter.Value = data.Columns.Contains("time") && row["time"] != DBNull.Value ? row["time"].ToString() : DateTime.UtcNow.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        private void MapGoalstDataToParameters(DataTable data, NpgsqlParameter[] parameters)
        {

            foreach (DataRow row in data.Rows)
            {

                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_telegramid":
                            parameter.Value = data.Columns.Contains("telegramId") && row["telegramId"] != DBNull.Value ? row["telegramId"].ToString() : string.Empty;
                            break;
                        case "p_goalid":
                            parameter.Value = data.Columns.Contains("goalid") && row["goalid"] != DBNull.Value ? Convert.ToInt32(row["goalid"]) : 0;
                            break;
                        case "p_goaltypeid":
                            parameter.Value = data.Columns.Contains("goaltypeid") && row["goaltypeid"] != DBNull.Value ? Convert.ToInt32(row["goaltypeid"]) : 0;
                            break;
                        case "p_goaltype":
                            parameter.Value = data.Columns.Contains("goaltype") && row["goaltype"] != DBNull.Value ? row["goaltype"].ToString() : "";
                            break;
                        case "p_cost":
                            parameter.Value =data.Columns.Contains("cost") && row["cost"] != DBNull.Value ? Convert.ToDecimal(row["cost"]) : 0m;
                            break;
                        case "p_mode":
                            parameter.Value =  data.Columns.Contains("mode") && row["mode"] != DBNull.Value ? Convert.ToInt32(row["mode"]) : 10;
                            break;
                        case "p_time":
                            parameter.Value = data.Columns.Contains("time") && row["time"] != DBNull.Value ? (row["time"].ToString()) : DateTime.UtcNow.ToString();
                            break;
                        case "p_timezone":
                            parameter.Value = data.Columns.Contains("timezone") && row["timezone"] != DBNull.Value ? (row["timezone"].ToString()) :"";
                            break;
                        default:
                            break;
                    }
                }
            }
        }


    }
}
