using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class findTransactions
    {
        public List<string> hashes { get; set; }

        public static string cmd = "\"command\": \"findTransactions\", \"addresses\": [{0}]";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;


        }
    }
}
