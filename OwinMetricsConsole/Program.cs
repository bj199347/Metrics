using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;

namespace OwinMetricsConsole
{
    class Program
    {
        public static List<string> Names = new List<string>();

        static void Main(string[] args)
        {
            Names.Add("Martin");
            Names.Add("Lucy");

            using (WebApp.Start<Startup>("http://localhost:8080"))
            {
                Console.WriteLine("Web Server is running.");
                Console.WriteLine("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}
