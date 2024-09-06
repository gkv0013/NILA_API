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
        public object? SaveBoostIfNotExists(DataTable referralData, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "save_boost_if_not_exists");

                MapReferralDataToParameters(referralData, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("save_boost_if_not_exists", parameters, connection, "result");
                return result.Tables["result"] ?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object? UpdateBoostData(DataTable referralData, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "update_boost");
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("update_boost", parameters, connection, "result");
                return result.Tables["result"] ?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object? FetchBoostData(DataTable referralData, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "fetch_boost");
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("fetch_boost", parameters, connection, "result");
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
                        case "p_referrer_id":
                            parameter.Value = row["userid"] != DBNull.Value ? row["userid"].ToString() : string.Empty;
                            break;
                        case "p_username":
                            parameter.Value = row["username"].ToString();
                            break;
                        case "p_referred_id":
                            parameter.Value = row["referrerid"] != DBNull.Value ? row["referrerid"].ToString() : string.Empty;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
