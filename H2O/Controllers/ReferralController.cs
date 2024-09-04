using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BLLayer;
using Newtonsoft.Json.Linq;
using System.Data;

namespace H2O.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralController : Controller
    {
        private readonly BLReferral _blReferral;

        public ReferralController()
        {
            _blReferral = new BLReferral();
        }

        [HttpPost("telegram")]
        //public async Task<IActionResult> SaveReferral([FromBody] ReferralRequest request)
        //{
        //    Validate the input data
        //    if (request.UserId <= 0 || request.ReferrerId <= 0)
        //        return BadRequest("Invalid UserId or ReferrerId");

        //    Save referral if not exists
        //   await _blReferral.SaveReferralIfNotExists(request.UserId, request.Username, request.ReferrerId);

        //    return Ok("Referral saved successfully.");
        //}
        public async Task<IActionResult> SaveReferral([FromBody] JObject update)
        {
            var message = update["message"];
            if (message == null)
                return BadRequest();

            var userId = message["from"]["id"].ToString();  // referredId (new user)
            var username = message["from"]["username"]?.ToString() ?? "Unknown";  // username of referred user
            var text = message["text"]?.ToString();

            string referrerId = null;

            // Extract referrerId from the start command if it's a referral
            if (text != null && text.StartsWith("/start referral_"))
            {
                referrerId = text.Replace("/start referral_", "").Trim();  // referrerId from the referral link
            }

            if (referrerId != null)
            {
                // Save the referral in the database
                DataTable referralData = new DataTable();
                referralData.Columns.Add("userid", typeof(long));
                referralData.Columns.Add("referrerid", typeof(long));
                referralData.Columns.Add("username", typeof(string));

                var newRow = referralData.NewRow();
                newRow["userid"] = long.Parse(referrerId);
                newRow["referrerid"] = long.Parse(userId);  // Telegram ID of referred user
                newRow["username"] = username;
                referralData.Rows.Add(newRow);

                var result = await _blReferral.SaveReferralIfNotExists(referralData);  // Save the referral
                return Ok(result);
            }

            return BadRequest("Invalid referral data.");
        }
    }

    public class ReferralRequest
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public long ReferrerId { get; set; }
    }
}
