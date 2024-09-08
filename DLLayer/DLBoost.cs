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
    public class DLBoost
    {
        public object? BoostCall(DataTable referralData, IDbConnection connection)
        {
            Console.WriteLine("HI");
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "user_boost");

                MapReferralDataToParameters(referralData, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("user_boost", parameters, connection, "result");
                return result.Tables["result"] ?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      



        //chnage paramter mapping
        private void MapReferralDataToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            // Iterate through each row in the DataTable
            foreach (DataRow row in data.Rows)
            {
                
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_mode":
                            parameter.Value = row["mode"];
                            break;
                        case "p_telegram_id":
                            parameter.Value = row["telegramId"];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
