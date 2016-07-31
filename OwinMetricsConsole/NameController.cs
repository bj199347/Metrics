
using OwinMetricsConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace AspNetSelfHostDemo
{
    public class NameController:ApiController
    {
        // GET api/name 
        public IEnumerable<string> Get()
        {
            return Program.Names;

        }
    }
}
