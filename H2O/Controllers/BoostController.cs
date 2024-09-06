
using BLLayer;
using H2O.Models;
using Hydrogen.Common;
using Microsoft.AspNetCore.Mvc;
using static H2O.Enumerators.CrudTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace H2O.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoostController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(Criteria Data)
        {
            object? Result = null;
            BLBoost objBLBoost = new BLBoost();
            switch (Data.CrudType)
            {
                case CrudType.Create:
                    if (Data.SaveData.Tables.Contains("collect"))
                    {
                        Result = objBLBoost.Create(Data.SaveData.Tables["collect"]);
                    }
                    else
                    {
                        Result = MessageLib.Error;
                    }
                    break;
                case CrudType.Read:
                    if (Data.Mode == 0)
                    {
                        Result = objBLBoost.FetchCoin(Data.FetchData);
                    }
                    else if (Data.Mode == 1)
                    {
                        Result = objBLBoost.Update(Data.FetchData);
                    }
                    break;
                default:
                    // Handle other CRUD operations if needed
                    break;
            }
            return Ok(Result);
        }
    }
}
