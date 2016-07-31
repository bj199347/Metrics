using System;
using Metrics;
using System.Threading;

namespace MetricsPOC
{
    class Program
    {
        private static Counter ctr = Metric.Counter("MyNewCounter1", Unit.Calls, "Group1");
        private static Counter ctr2 = Metric.Counter("MyNewCounter2", Unit.Calls, "Group2");
        
        public static void Stop()
        {
            Console.WriteLine("Stop service");
        }

        public static void Start()
        {
            Console.WriteLine("Start service");
        }

        static void Main(string[] args)
        {
            Metric.Config.WithHttpEndpoint("http://localhost:1234/", config => config
            .WithBasicCounters()
            );

            RegisterHealthChecks();

            for (int i = 0; i < 3433; i++)
            {
                ctr.Increment();
                ctr2.Decrement();
                Thread.Sleep(50);
            }           

            Console.ReadLine();
        }

        public static void RegisterHealthChecks()
        {
            HealthChecks.RegisterHealthCheck("DatabaseConnected", () =>
            {
                CheckDbIsConnected();
                return "Database Connection OK";
            });

            HealthChecks.RegisterHealthCheck("DiskSpace", () =>
            {
                int freeDiskSpace = GetFreeDiskSpace();

                if (freeDiskSpace <= 512)
                {
                    return HealthCheckResult.Unhealthy("Not enough disk space: {0}", freeDiskSpace);
                }
                else
                {
                    return HealthCheckResult.Unhealthy("Disk space ok: {0}", freeDiskSpace);
                }
            });

            HealthChecks.RegisterHealthCheck("SampleOperation", () => ConnectToBetfair());
        }

        private static bool ConnectToBetfair()
        {
            return true;
        }

        private static int GetFreeDiskSpace()
        {
            return 1024;
        }

        private static bool CheckDbIsConnected()
        {
            return true;
        }
    }

    
}
