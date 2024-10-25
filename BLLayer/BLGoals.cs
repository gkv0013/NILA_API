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
    public class BLGoals
    {
        public object? GoalsSave(DataTable? data)
        {

            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlBoost = new DLGoals();
                    return dlBoost.GoalsInsert(data, connection);
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
