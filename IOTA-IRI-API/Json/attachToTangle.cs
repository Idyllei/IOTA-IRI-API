using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class attachToTangle
    {
        public List<string> trytes { get; set; }

        public static string cmd = "\"command\": \"attachToTangle\", \"trunkTransaction\": {0}, \"branchTransaction\": {1}, \"minWeightMagnitude\": {2}, \"trytes\": [{3}]";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;

            attachToTangle Ni = JsonConvert.DeserializeObject<attachToTangle>(Encoding.Default.GetString(data));

            foreach(string s in Ni.trytes)
                Console.WriteLine("Trytes: ", s);

            Console.WriteLine("End trytes.");
        }
    }
}
