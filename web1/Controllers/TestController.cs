using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI2.Modules;
using MySql.Data.MySqlClient;

namespace WebAPI2.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {

        [Route("api/Select")]
        [HttpPost]
        public ActionResult<ArrayList> Select()
        {   
            return Query.GetSelect();
        }

        [Route("api/Insert")]
        [HttpPost]
        public ActionResult<ArrayList> Insert([FromForm] Test test)
        {   
            //Console.WriteLine("Insert POST 진입");
            return Query.GetInsert(test);
        }

        [Route("api/Update")]
        [HttpPost]
        public ActionResult<ArrayList> Update([FromForm] Test test)
        {   
            //Console.WriteLine("Update POST 진입");
            return Query.GetUpdate(test);
        }

        [Route("api/Delete")]
        [HttpPost]
        public ActionResult<ArrayList> Delete([FromForm] Test test)
        {   
            return Query.GetDelete(test);
        }

    }
}
