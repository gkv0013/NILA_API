
using BLLayer;
using H2O.Models;
using Hydrogen.Common;
using Microsoft.AspNetCore.Mvc;
using static H2O.Enumerators.CrudTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H2O.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post(Criteria Data)
        {
            object? Result = null;
            BLLogin objBLlogin = new BLLogin();
            switch (Data.CrudType)
            {
                case CrudType.Create:
                    if (Data.SaveData.Tables.Contains("collect"))
                    {
                        Result = objBLlogin.Collect(Data.SaveData.Tables["collect"]);
                    }
                    else
                    {
                        Result =MessageLib.Error;
                    }
                    break;
                case CrudType.Read:
                    if (Data.Mode==0)
                    {
                        Result = objBLlogin.Login(Data.FetchData);
                    }
                 else if (Data.Mode==1)
                    {
                        Result = objBLlogin.User(Data.FetchData);
                    }

                    else if (Data.Mode==2)
                    {
                        Result = objBLlogin.Image(Data.SaveData);
                    }
                    break;
                default:
                    // Handle other CRUD operations if needed
                    break;
            }
            return Ok(Result);
        }
        [HttpGet("welcome")]
        public IActionResult GetWelcomeMessage()
        {
            return Ok("Welcome to the Login API!!!");
        }

        // GET api/<LoginController>/status
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok("Login API is running.");
        }

    }
}
