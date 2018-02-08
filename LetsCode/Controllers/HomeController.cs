using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LetsCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAzureClient _azureClient;

        public HomeController(IAzureClient azureClient)
        {
            _azureClient = azureClient;
        }

       [HttpGet("/api/virtualmachines")]
        public JsonResult Index()
       {
           var vms = _azureClient.GetClient().VirtualMachines.List().ToList().Select(s => s.Name);
           return Json(vms, new JsonSerializerSettings() {Formatting = Formatting.Indented});
       }
    }
}
