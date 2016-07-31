
using OwinMetricsConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace AspNetSelfHostDemo
{
    /// <summary>
    /// VersionController
    /// </summary>
    public class VersionController:ApiController
    {
        /// <summary>
        /// GET Current version of the ExecutingAssembly Name
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            AssemblyName name = Assembly.GetExecutingAssembly().GetName();
            return name.Name.ToString() + ": " + name.Version.ToString();
        }
        
    }
}
