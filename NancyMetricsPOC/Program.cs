using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Metrics;
using Nancy.Bootstrapper;

namespace NancyMetricsPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        public static int Method1(string input)
        {
            //... do something
            return 0;
        }

        public static int Method2(string input)
        {
            //... do something different
            return 1;
        }

        public static bool RunTheMethod(Func<string, int> myMethodName)
        {
            //... do stuff
            int i = myMethodName("My String");
            //... do more stuff
            return true;
        }

        public static bool Test()
        {
            return RunTheMethod(Method2);
        }
    }
}

