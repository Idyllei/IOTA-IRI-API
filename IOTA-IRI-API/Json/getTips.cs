using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class getTips
    {
        public List<string> hashes { get; set; }
        public int duration { get; set; }

        public static string cmd = "\"command\": \"getTips\"";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;


        }
    }
}
