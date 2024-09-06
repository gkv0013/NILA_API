using DLLayer;
using DllLayer.PgSqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer
{
    public class BLWallet
    {
        public object? Wallet(DataTable data)
        {
            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlWallet = new DLWallet();
                    return  dlWallet.Wallet(data, connection);
                   

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
