﻿using DLLayer;
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
        public object? Operation(DataTable? data)
        {
            // IDbConnection is instantiated within a using statement.
            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    var dlBoost = new DLBoost();
                    return dlBoost.BoostCall(data, connection);
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
