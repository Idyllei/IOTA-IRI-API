using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class getTransactionsToApprove
    {
        public string trunkTransaction { get; set; }
        public string branchTransaction { get; set; }
        public int duration { get; set; }

        public static string cmd = "\"command\": \"getTransactionsToApprove\", \"depth\": {0}";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;
            getTransactionsToApprove Ni = JsonConvert.DeserializeObject<getTransactionsToApprove>(Encoding.Default.GetString(data));

            Console.WriteLine("Trunk: ", Ni.trunkTransaction);
            Console.WriteLine("Brnach: ", Ni.branchTransaction);
        }
    }
}
