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
    public class DLWallet
    {
        public object? Wallet(DataTable data, IDbConnection connection)
        {
            try
            {
                NpgsqlParameter[] parameters = PgsqlHelper.GetSpParameterSet(connection, "get_total_coins");
                MapWalletToParameters(data, parameters);
                DataSet result = PgsqlHelper.ExecuteFunctionWithTransaction("get_total_coins", parameters, connection, "result");
                return result.Tables["result"]?? null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void MapWalletToParameters(DataTable data, NpgsqlParameter[] parameters)
        {
            // Iterate through each row in the DataTable
            foreach (DataRow row in data.Rows)
            {
                foreach (var parameter in parameters)
                {
                    switch (parameter.ParameterName)
                    {
                        case "p_telegramid":
                            parameter.Value = data.Columns.Contains("id") && row["id"] != DBNull.Value
                                ? row["id"].ToString()
                                : string.Empty;
                            break;

                        case "p_mode":
                            parameter.Value = data.Columns.Contains("mode") && row["mode"] != DBNull.Value
                                ? Convert.ToInt32(row["mode"])
                                : 10;
                            break;

                        case "p_total_coins":
                            parameter.Value = data.Columns.Contains("claim") && row["claim"] != DBNull.Value
                                ? Convert.ToInt64(row["claim"])
                                : 0;
                            break;

                        default:
                            // Keep the original value or handle unexpected parameters
                            break;
                    }
                }
            }
        }

    }
}
