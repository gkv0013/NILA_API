
using BLLayer;
using H2O.Models;
using Hydrogen.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;
using static H2O.Enumerators.CrudTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace H2O.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoostController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(JObject data)
        {
            object? Result = null;
            BLBoost objBLBoost = new BLBoost();
            int mode = (int)data["mode"];
            string telegramId = data["telegramId"].ToString();

            DataTable referralData = new DataTable();
            referralData.Columns.Add("mode", typeof(int));
            referralData.Columns.Add("telegramId", typeof(string));

            var newRow = referralData.NewRow();
            newRow["mode"] = mode;
            newRow["telegramId"] = telegramId;  // Telegram ID of referred user
            referralData.Rows.Add(newRow);

            if (mode == 0 || mode == 1|| mode == 2|| mode == 3)
             {
                Result = objBLBoost.Operation(referralData);
            }
            
            return Ok(Result);
        }
    }
}
