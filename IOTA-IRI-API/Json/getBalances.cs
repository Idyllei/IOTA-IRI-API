using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class getBalances
    {
        public List<string> balances { get; set; }
        public int duration { get; set; }
        public string milestone { get; set; }
        public int milestoneIndex { get; set; }

        public static void Display(byte[] data)
        {
            if (data == null)
                return;


        }
    }
}
