using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Chameleon.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace Chameleon.Controllers
{
	public class ApiController : Controller
    {
        private readonly Application _application;

        public ApiController(IOptions<Application> options)
        {
            _application = options.Value;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Duration = 0, VaryByHeader = "None")]
        public ActionResult ApplicationIDs()
        {
            return Content(JsonConvert.SerializeObject(_application.GetIDs()), "application/json");
        }

        [HttpGet]
        [Route("host/{app}")]
        public ActionResult GetHost(string app)
        {
            return Content(_application.GetUrl(app));
        }
    }
}