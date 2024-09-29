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
    public class BLBoost
    {
        public object? BoostSave(DataTable? data)
        {
       
            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlBoost = new DLBoost();
                    return dlBoost.BoostInsert(data, connection);
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
        public object? GetBoostData(DataTable? data)
        {

            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlBoost = new DLBoost();
                    return dlBoost.BoostInsert(data, connection);
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
        public object? GetBoostLog(DataTable? data)
        {

            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlBoost = new DLBoost();
                    return dlBoost.BoostLog(data, connection);
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
