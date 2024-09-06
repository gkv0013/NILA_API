using BLLayer;
using H2O.Models;
using Hydrogen.Common;
using Microsoft.AspNetCore.Mvc;
using static H2O.Enumerators.CrudTypes;

namespace NilaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(Criteria Data)
        {
            object? Result = null;
            BLWallet objBLWallet = new BLWallet();
            switch (Data.CrudType)
            {
                case CrudType.Create:

                    break;
                case CrudType.Read:
                    if (Data.Mode==0)
                    {
                        Result = objBLWallet.Wallet(Data.FetchData);
                    }
                    else if (Data.Mode==1)
                    {
                       
                    }

                    else if (Data.Mode==2)
                    {
                        
                    }
                    break;
                default:
                   
                    break;
            }
            return Ok(Result);
        }
    }
}
