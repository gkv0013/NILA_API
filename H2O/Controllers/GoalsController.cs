using BLLayer;
using H2O.Models;
using Microsoft.AspNetCore.Mvc;
using static H2O.Enumerators.CrudTypes;

namespace NilaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(Criteria Data)
        {
            object? Result = null;
            BLGoals objblGoals = new BLGoals();
            switch (Data.CrudType)
            {
                case CrudType.Create:
                    if (Data.Mode==0)
                    {
                        Result = objblGoals.GoalsSave(Data.SaveData.Tables["savegoals"]);

                    } 
                    break;
                case CrudType.Read:
                    if (Data.Mode==1)
                    {
                        Result = objblGoals.GoalsSave(Data.FetchData);
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
