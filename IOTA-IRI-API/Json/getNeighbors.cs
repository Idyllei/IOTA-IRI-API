using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOTA_IRI_API.Json
{
    public class Neighbor
    {
        public string address { get; set; }
        public int numberOfAllTransactions { get; set; }
        public int numberOfInvalidTransactions { get; set; }
        public int numberOfNewTransactions { get; set; }
    }

    public class getNeighbors
    {
        public int duration { get; set; }
        public List<Neighbor> neighbors { get; set; }

        public static string cmd = "\"command\": \"getNeighbors\"";

        public static void Display(byte[] data)
        {
            if (data == null)
                return;

            Json.getNeighbors c = JsonConvert.DeserializeObject<Json.getNeighbors>(Encoding.Default.GetString(data));

            foreach (var ff in c.neighbors)
            {
                Console.WriteLine("Address: {0}", ff.address);
                Console.WriteLine("Transactions: {0}", ff.numberOfAllTransactions);
                Console.WriteLine("Invalid Transactions: {0}", ff.numberOfInvalidTransactions);
                Console.WriteLine("New Transactions: {0}\n", ff.numberOfNewTransactions);
            }

            Console.WriteLine("End of Neighbors.");
        }
    }
}
