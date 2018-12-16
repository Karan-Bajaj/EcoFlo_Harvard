using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using EdgeJs;
using HHAPI.Infrastructure;

namespace HHAPI.Controllers
{
    public class ValuesController : ApiController
    {
        Int64 amount = 1000;

        string paymentPointer = "$spsp.strata-ilsp-3.com:8084";
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "Execution", "Complete" };
        }

        [Route("api/getTotal")]
        public Int64 getTotal() {
            return API.ReadTotal();
        }

        [Route("api/getRate")]
        public Int64 getRate()
        {
            return API.ReadRate();
        }

        [HttpPost]
        [Route("api/resetTotal")]
        public bool resetTotal()
        {
            return API.ResetTotal();
        }

        //[HttpPost]
        //[Route("api/Values/Register")]
        //public bool Register([FromBody]registorInfo Info)


        // GET api/values/5
        public string Get(Int64 id)
        {
            API.InsertRate(id);

            return id.ToString();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {

            
        }

        private static void RunMethodInSeparateThread(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }


















    }
}
