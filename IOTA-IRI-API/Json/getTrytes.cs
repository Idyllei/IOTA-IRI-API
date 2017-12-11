using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class getTrytes
    {
        public List<string> trytes { get; set; }

        public static string cmd = "\"command\": \"getTrytes\", \"hashes\": [{0}]";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;


        }
    }
}
