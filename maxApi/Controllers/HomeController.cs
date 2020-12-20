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
            
            //Create a connection to Oracle			
            string conString = "User Id=hr; Password=<password>; Data Source=<service name>;";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    string result = "";

                    try
                    {
                        con.Open();
                        cmd.BindByName = true;                            

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select first_name from employees where department_id = :id";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", 50);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = result + reader.GetString(0) ;
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }

                    return result;
                }
            }
        }
    }
}
