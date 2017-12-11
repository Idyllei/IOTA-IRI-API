using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class getInclusionStates
    {
        public List<bool> states { get; set; }
        public int duration { get; set; }

        public static string cmd = "\"command\": \"getInclusionStates\", \"transactions\": [{0}], \"tips\": [{1}]";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;


        }
    }
}
