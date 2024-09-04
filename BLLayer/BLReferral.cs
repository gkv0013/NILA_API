using DLLayer;
using DllLayer.PgSqlHelper;
using System.Data;
using System.Threading.Tasks;

namespace BLLayer
{
    public class BLReferral
    {
        private readonly DLReferral _dlReferral;

        public BLReferral()
        {
            _dlReferral = new DLReferral();
        }

        public async Task<string> SaveReferralIfNotExists(DataTable referralData)
        {

            using (var connection = PgsqlHelper.GetOpenConnection())
            {
                try
                {
                    // Call the synchronous method
                    _dlReferral.SaveReferralIfNotExists(referralData, connection);
                    return "Referral saved successfully";
                }
                catch (Exception ex)
                {
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
