using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTA_IRI_API
{
    public class CommandCenter
    {
        public static string getNeighbors(string[] addresses)
        {
            StringBuilder sb = new StringBuilder();
            bool e = false;

            for (int i = 1; i < addresses.Length; i++)
            {
                if (!addresses[i].Contains("udp://") || !addresses[i].Contains(":"))
                    e = true;
                else
                    if (i == addresses.Length - 1)
                        sb.Append("\"" + addresses[i] + "\"");
                    else
                        sb.Append("\"" + addresses[i] + "\",");
            }

            if (!e)
                return sb.ToString();
            else
                return "you done fucked up fam";
        }
    }
}
