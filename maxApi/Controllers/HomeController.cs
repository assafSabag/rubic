using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Oracle.ManagedDataAccess.Client;


namespace maxApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        readonly DB _DB;
        public HomeController(DB DB)
        {
            _DB = DB;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "max", "cal" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "max";
        }

        // GET api/values/db
        [HttpGet("db")]
        public ActionResult<string> GetDb()
        {
            return _DB.ExecuteSqlCommand("select first_name from employees where department_id = :id","id");
        }
    }
}
