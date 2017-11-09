using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/account")]
    public class AccountController : GenericCRUDController<Account>
    {
        public AccountController(IDataService dataService) : base(dataService, dataService.GetAccountRepository())
        {
        }
    }
}
