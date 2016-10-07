using System;
using System.Web.Http;

namespace AspNetSelfHostDemo
{
    public class ControlController:ApiController
    {
        //public string Get()
        //{
        //    return "Service Started...";
        //}

        public string Get(string command)
        {
            switch (command.ToLower())
            {
                case "stop":
                    return Stop();
                case "start":
                    return Start();
                default:
                    return "unsupported command";
            }
            
        }

        private string Start()
        {
            return "Service started...";
        }

        private string Stop()
        {
            return "Service stopped...";
        }

        // PUT api/demo/5 
        public string Put(string command, [FromBody]string value)
        {
            switch (command.ToLower())
            {
                case "lastpolledid":
                    Int64 lastPolledId;
                    if (Int64.TryParse(value, out lastPolledId))
                        return UpdateLastPolledId(lastPolledId);
                    else
                        return "Invalid LastPolledId";                  
                default:
                    return "unsupported command";
            }
        }

        private string UpdateLastPolledId(Int64 newValue)
        {
            return "LastPolledId updated to " + newValue.ToString();
        }
    }
}
