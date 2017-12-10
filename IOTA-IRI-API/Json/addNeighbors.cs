using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class addNeighbors
    {
        public int addedNeighbors { get; set; }
        public int duration { get; set; }

        public static string cmd = "\"command\": \"addNeighbors\", \"uris\": [{0}]";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;

            addNeighbors Ni = JsonConvert.DeserializeObject<addNeighbors>(Encoding.Default.GetString(data));

            Console.WriteLine("addedNeighbors: {0}", Ni.addedNeighbors);
            Console.WriteLine("duration: {0}", Ni.duration);
        }
    }
}
