

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/history")]
    public class HistoryController : GenericCRUDController<History>
    {
        public HistoryController(IDataService dataService) : base(dataService, dataService.GetHistoryRepository())
        {
        }
    }
}
