
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
        public IActionResult Post(Criteria Data)
        {
            object? Result = null;
            BLBoost objblBoost=new BLBoost();
            switch (Data.CrudType)
            {
                case CrudType.Create:
                    if (Data.SaveData.Tables.Contains("activatemultitap") || Data.SaveData.Tables.Contains("activateenergyboost")|| Data.SaveData.Tables.Contains("activatefullenergy"))
                    {
                        if (Data.SaveData.Tables.Contains("activatemultitap"))
                        {
                            Result = objblBoost.BoostSave(Data.SaveData.Tables["activatemultitap"]);
                        }
                        else if (Data.SaveData.Tables.Contains("activateenergyboost"))
                        {
                            Result = objblBoost.BoostSave(Data.SaveData.Tables["activateenergyboost"]);
                        }
                        else if (Data.SaveData.Tables.Contains("activatefullenergy"))
                        {
                            Result = objblBoost.BoostSave(Data.SaveData.Tables["activatefullenergy"]);
                        }
                    }
                    break;
                case CrudType.Read:
                    if (Data.Mode==0)
                    {
                        Result = objblBoost.GetBoostData(Data.FetchData);
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
