using DllLayer.PgSqlHelper;
using Npgsql;
using System.Data;

namespace DLLayer
{
    public class DLReferral
    {
        public object?  SaveReferralIfNotExists(DataTable referralData, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "save_referral_if_not_exists");

                MapReferralDataToParameters(referralData, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("save_referral_if_not_exists", parameters, connection, "result");
                return result.Tables["result"]?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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
                            parameter.Value = data.Columns.Contains("userid") && row["userid"] != DBNull.Value
                                ? row["userid"].ToString()
                                : string.Empty;
                            break;

                        case "p_username":
                            parameter.Value = data.Columns.Contains("username") && row["username"] != DBNull.Value
                                ? row["username"].ToString()
                                : string.Empty;
                            break;

                        case "p_referred_id":
                            parameter.Value = data.Columns.Contains("referrerid") && row["referrerid"] != DBNull.Value
                                ? row["referrerid"].ToString()
                                : string.Empty;
                            break;

                        default:
                            // Optionally handle any unexpected parameters
                            break;
                    }
                }
            }
        }


    }
}
