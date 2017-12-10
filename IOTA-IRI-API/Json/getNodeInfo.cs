using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class getNodeInfo
    {
        public string appName { get; set; }
        public string appVersion { get; set; }
        public Int64 duration { get; set; }
        public Int64 jreAvailableProcessors { get; set; }
        public Int64 jreFreeMemory { get; set; }
        public Int64 jreMaxMemory { get; set; }
        public Int64 jreTotalMemory { get; set; }
        public string latestMilestone { get; set; }
        public Int64 latestMilestoneIndex { get; set; }
        public string latestSolidSubtangleMilestone { get; set; }
        public Int64 latestSolidSubtangleMilestoneIndex { get; set; }
        public Int64 neighbors { get; set; }
        public Int64 packetsQueueSize { get; set; }
        public long time { get; set; }
        public Int64 tips { get; set; }
        public Int64 transactionsToRequest { get; set; }

        public string cryptokitties { get; set; }
        /*    ^^   MEOW    ^^    ^^      ^^    */

        public static string cmd = "\"command\": \"getNodeInfo\"";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;
            getNodeInfo Ni = JsonConvert.DeserializeObject<getNodeInfo>(Encoding.Default.GetString(data));

            //Console.WriteLine("Application: " + Ni.appName);
            Console.WriteLine("Version: " + Ni.appVersion);
            //Console.WriteLine("Duration: " + Ni.duration); dunno
            //Console.WriteLine("Available Processors: " + Ni.jreAvailableProcessors);
            Console.WriteLine("System: {0} Cores | {1} MB RAM", Ni.jreAvailableProcessors, (Ni.jreTotalMemory / 1048576)); //maximum runtime memory
            Console.WriteLine("Memory: {0}/{1} MB", (Ni.jreFreeMemory / 1048576), (Ni.jreMaxMemory / 1048576));
            //Console.WriteLine("Max Memory:" + Ni.jreMaxMemory);
            Console.WriteLine("Milestone: {0}/{1}", Ni.latestSolidSubtangleMilestoneIndex, Ni.latestMilestoneIndex);
            Console.WriteLine("Latest Milestone: " + Ni.latestMilestone);
            //Console.WriteLine("Latest Milestone Index: " + Ni.latestMilestoneIndex);
            Console.WriteLine("Latest Solid Subtangle Milestone: " + Ni.latestSolidSubtangleMilestone);
            //Console.WriteLine("Latest Solid Subtangle Milestone Index: " + Ni.latestSolidSubtangleMilestoneIndex);
            Console.WriteLine("Neighbors: " + Ni.neighbors);
            Console.WriteLine("Packets Queue Size: " + Ni.packetsQueueSize);
            //Console.WriteLine("Time: " + Ni.time);
            Console.WriteLine("Tips: " + Ni.tips);
            Console.WriteLine("Transactions To Request: " + Ni.transactionsToRequest);
        }
    }
}
