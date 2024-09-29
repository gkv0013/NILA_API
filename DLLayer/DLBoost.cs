using DllLayer.PgSqlHelper;
using Npgsql;
using System.Data;

namespace DLLayer
{
    public class DLBoost
    {
        public object? BoostInsert(DataTable referralData, IDbConnection connection)
        {
            Console.WriteLine("HI");
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "insert_boost_data");

                MapBoostDataToParameters(referralData, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("insert_boost_data", parameters, connection, "result");
                return result.Tables["result"] ?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        private void MapBoostDataToParameters(DataTable data, NpgsqlParameter[] parameters)
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
                        case "p_boosttypeid":
                            parameter.Value = data.Columns.Contains("boostTypeId") && row["boostTypeId"] != DBNull.Value ? Convert.ToInt32(row["boostTypeId"]) : 0;
                            break;
                        case "p_boosttype":
                            parameter.Value = data.Columns.Contains("boostType") && row["boostType"] != DBNull.Value ? row["boostType"].ToString() : "";
                            break;
                        case "p_cost":
                            parameter.Value =data.Columns.Contains("cost") && row["cost"] != DBNull.Value ? Convert.ToDecimal(row["cost"]) : 0m; 
                            break;
                        case "p_mode":
                            parameter.Value =  data.Columns.Contains("mode") && row["mode"] != DBNull.Value ? Convert.ToInt32(row["mode"]) : 10;
                            break;
                        case "p_boost_timestamp":
                            parameter.Value = data.Columns.Contains("boostTimestamp") && row["boostTimestamp"] != DBNull.Value ? Convert.ToDateTime(row["boostTimestamp"]) : DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}


