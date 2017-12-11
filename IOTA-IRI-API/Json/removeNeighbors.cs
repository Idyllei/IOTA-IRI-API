using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class removeNeighbors
    {
        public int removedNeighbors { get; set; }
        public int duration { get; set; }

        public static string cmd = "\"command\": \"removeNeighbors\", \"uris\": [{0}]";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;

            removeNeighbors Ni = JsonConvert.DeserializeObject<removeNeighbors>(Encoding.Default.GetString(data));

            Console.WriteLine("addedNeighbors: {0}", Ni.removedNeighbors);
            Console.WriteLine("duration: {0}", Ni.duration);
        }
    }
}
