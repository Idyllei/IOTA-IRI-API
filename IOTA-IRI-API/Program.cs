using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace IOTA_IRI_API
{
    class Program
    {
        public class RequestsClass
        {
            //this will all be removed soon
            public string getNodeInfo { get; set; }
            public string getNeighbors { get; set; }
            public string addNeighbors { get; set; }
            public string removeNeighbors { get; set; }
            public string getTips { get; set; }
            public string findTransactions { get; set; }
            public string getTrytes { get; set; }
            public string getInclusionStates { get; set; }
            public string getBalances { get; set; }
            public string getTransactionsToApprove { get; set; }
            public string attachToTangle { get; set; }
            public string interruptAttachingToTangle { get; set; }
            public string broadcastTransactions { get; set; }
            public string storeTransactions { get; set; }
            //I'm covering it in gasoline.
            //public Dictionary<int, string> Requests { get; set; }

            /*
             * remainder for tomrow collapse json files into signle fiel and nest classes or list or somethin 
             * 
             */

            public RequestsClass()
            {
                getNodeInfo = "\"command\": \"getNodeInfo\"";
                getNeighbors = "\"command\": \"getNeighbors\"";
                addNeighbors = "\"command\": \"addNeighbors\", \"uris\": [{0}]";
                removeNeighbors = "\"command\": \"removeNeighbors\", \"uris\": [{0}]";
                getTips = "\"command\": \"getTips\"";
                findTransactions = "\"command\": \"findTransactions\", \"addresses\": [{0}]";
                getTrytes = "\"command\": \"getTrytes\", \"hashes\": [{0}]";
                getInclusionStates = "\"command\": \"getInclusionStates\", \"transactions\": [{0}], \"tips\": [{1}]";
                getBalances = "\"command\": \"getBalances\", \"addresses\": [{0}], \"threshold\": 100";
                getTransactionsToApprove = "\"command\": \"getTransactionsToApprove\", \"depth\": {0}";
                attachToTangle = "\"command\": \"attachToTangle\", \"trunkTransaction\": {0}, \"branchTransaction\": {1}, \"minWeightMagnitude\": {2}, \"trytes\": [{3}]";
                interruptAttachingToTangle = ""; //empty POST will do the trick.
                broadcastTransactions = "\"command\": \"broadcastTransactions\", \"trytes\": [{0}]";
                storeTransactions = "\"command\": \"storeTransactions\", \"trytes\": [{0}]";
            }
        }
        //public static RequestsClass Requests = new RequestsClass();
        private static string NodeIP;
        private static Int16 NodePort;

        static void Main(string[] args)
        {
            string[] _cmd_args;
            //string cmd;

            ClearConsolas(); //This will print out the stupid welcome message at the top so I don't have to keep pasting the crap everywhere.

            while (true)
            {

                Console.WriteLine("Node Address usage(3 methods): 92.92.92.92, !resolv domain.com, !local");
                while (true)
                {
                    Console.Write("Please enter your Node IP>");
                    _cmd_args = Console.ReadLine().ToLower().Split(' ');

                    if (_cmd_args[0] == "!resolv" || _cmd_args[0] == "!resolve")
                    {
                        if (string.IsNullOrEmpty(_cmd_args[1]) || !_cmd_args[1].Contains("."))
                            Console.WriteLine("Invalid input.");
                        else
                        {
                            NodeIP = _cmd_args[1];
                            break;
                        }
                    }
                    else if (_cmd_args[0] == "!local" || _cmd_args[0] == "!localhost")
                    {
                        NodeIP = "127.0.0.1";
                        break;
                    }
                    else
                    {
                        if (Regex.IsMatch(_cmd_args[0], @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$"))
                        {
                            NodeIP = _cmd_args[0];
                            break;
                        }
                        else
                            Console.WriteLine("Invalid IPv4 address.");
                    }
                }

                while (true)
                {
                    Console.WriteLine("!def for default port of 14265");
                    Console.Write("Please enter your Nodes port>");
                    _cmd_args = Console.ReadLine().ToLower().Split(' ');
                    if (_cmd_args[0] == "!def" || _cmd_args[0] == "!default")
                    {
                        NodePort = 14265;
                        break;
                    }
                    else if (_cmd_args[0] == "!l")
                    {
                        NodePort = 14421;
                        break;
                    }
                    else
                    {
                        try { NodePort = Convert.ToInt16(_cmd_args[0]); break; }
                        catch { Console.WriteLine("Invalid port."); continue; }
                    }
                }


               if (Validate_Node())
                   break;
               else
                   ClearConsolas("Error connecting to server! Please make sure you have the information correct.", ConsoleColor.Yellow);
            }

            ClearConsolas(string.Format("Connected to {0}:{1} sucessful.", NodeIP, NodePort), ConsoleColor.Green);

            while (true)
            {
                Console.Write(">");

                _cmd_args = Console.ReadLine().ToLower().Split(new char[] { ' '}, 2);

                switch (_cmd_args[0])
                {
                    case "!clear":
                        ClearConsolas();
                        break;
                    case "!help":
                        Console.WriteLine("!help - menu\n!clear - clears console\nquit - exits program (alias: exit, stop)\n!api - list api commands.");
                        break;
                    case "!api": //I haven't collapsed this into a non-literal yet, obviously.
                        Console.WriteLine(@"
getnodeinfo - Returns information about your node. No args.\n
\tArgs: None\n
getneighbors - Returns the set of neighbors you are connected with, as well as their activity count. The activity counter is reset after restarting IRI.\n
\tArgs: None\n
addneighbors - Add a list of neighbors to your node. It should be noted that this is only temporary, and the added neighbors will be removed from your set of neighbors after you relaunch IRI.\n
\tArgs: udp:\\neighbor:port seperated by a comma. Example: 'udp:\\cooldomain.com:14600', 'tcp:\\cooldomain.com:15600'\n
removeneighbors - Removes a list of neighbors to your node. This is only temporary, and if you have your neighbors added via the command line, they will be retained after you restart your node. \n
\tArgs: udp:\\neighbor:port seperated by a comma. Example: 'udp:\\cooldomain.com:14600', 'tcp:\\cooldomain.com:15600'\n
gettips - Returns the list of tips.\n
\tArgs: None.\n
findtransactions - Find the transactions which match the specified input and return.\n
\tArgs: Lots\n
gettrytes - Returns the raw transaction data (trytes) of a specific transaction. These trytes can then be easily converted into the actual transaction object.\n
\tArgs: Hash.\n
getinclusionstates - Get the inclusion states of a set of transactions. This is for determining if a transaction was accepted and confirmed by the network or not. You can search for multiple tips (and thus, milestones) to get past inclusion states of transactions. \n
\tArgs: transactions, tips\n
getbalances - Similar to getInclusionStates. It returns the confirmed balance which a list of addresses have at the latest confirmed milestone. In addition to the balances, it also returns the milestone as well as the index with which the confirmed balance was determined.\n
\tArgs: addresses\n
gettransactionstoapprove - Tip selection which returns trunkTransaction and branchTransaction. The input value is depth, which basically determines how many bundles to go back to for finding the transactions to approve. The higher your depth value, the more 'babysitting' you do for the network (as you have to confirm more transactions).\n
\tArgs: depth.\n
attachtotangle - Attaches the specified transactions (trytes) to the Tangle by doing Proof of Work. You need to supply branchTransaction as well as trunkTransaction (basically the tips which you're going to validate and reference with this transaction) - both of which you'll get through the getTransactionsToApprove API call.\n
\tArgs: trunkTransaction, branchTransaction, Intensity (Minimum 18), trytes.\n
interruptattachingtotangle - Self Explainatory\n
\tArgs: None.\n
broadcasttraansactions - Broadcast a list of transactions to all neighbors. The input trytes for this call are provided by attachToTangle.\n
\tArgs: Trytes.\n
storetransactions - Store transactions into the local storage. The trytes to be used for this call are returned by attachToTangle.\n
\tArgs: Trytes.\n\nEND OF API COMMANDS\n");
                        break;
                    case "quit":
                        Environment.Exit(0);
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    case "stop":
                        Environment.Exit(0);
                        break;
                        //api commands
                    case "getnodeinfo":
                        Json.getNodeInfo.Display(SendCmd(Json.getNodeInfo.cmd));
                        break;
                    case "getneighbors":
                        Json.getNeighbors.Display(SendCmd(Json.getNeighbors.cmd));
                        break;
                    case "addneighbors":
                        //SendCmd(, 1, _cmd_args[1]);
                        break;
                    case "removeneighbors":
                        //SendCmd(Requests.removeNeighbors);
                        break;
                    case "gettips":
                        //SendCmd(Requests.getTips);
                        break;
                    case "findtransactions":
                        //SendCmd(Requests.findTransactions);
                        break;
                    case "gettrytes":
                        //SendCmd(Requests.getTrytes);
                        break;
                    case "getinclusionstates":
                        //SendCmd(Requests.getInclusionStates, 2, _cmd_args[1]);
                        break;
                    case "getbalances":
                        //SendCmd(Requests.getBalances);
                        break;
                    case "gettransactionstoapprove":
                        Json.getTransactionsToApprove.Display(SendCmd(string.Format("\"command\": \"getTransactionsToApprove\", \"depth\": {0}", _cmd_args[1])));
                        break;
                    case "attachtotangle":
                        Json.attachToTangle.Display(SendCmd(Json.attachToTangle.cmd, 4, _cmd_args[1]));
                        break;
                    case "interruptattachingtotangle":
                        //SendCmd(Requests.interruptAttachingToTangle);
                        break;
                    case "broadcasttransactions":
                        //SendCmd(Requests.broadcastTransactions);
                        break;
                    case "storetransactions":
                        //SendCmd(Requests.storeTransactions);
                        break;
                    default:
                        Console.WriteLine("Invalid. Try !help for list of commands, or !API for list of api calls.");
                        break;
                }
            }
        }
        
        /* * Normal removal of nbags are integres removen 1 2 3 4 and it will first pull getneighbours() and assign them integers while listing the integeres to the user.
         * I was obviously really fucked when I wrote this I don't know what the hell it means. leaving it to try and figure out later.
         * I don't even want to think about how many times I've refactored or completely rewritten this trying to get it to work. Me no smart.
         */

        private static bool Validate_Node()
        {
            bool Accessible = false;

            using(WebClient wc = new WebClient())
            {
                try {
                    wc.Headers["Content-Type"] = "application/json"; wc.Headers["X-IOTA-API-Version"] = "1";
                    byte[] Returned = wc.UploadData(string.Format("http://{0}:{1}", NodeIP, NodePort), "POST", Encoding.Default.GetBytes("{" + Json.getNeighbors.cmd + "}"));
                    Accessible = true;
                }
                catch { Accessible = false; }
            }

            return Accessible;
        }

        private static void ClearConsolas(string msg = "", ConsoleColor cc = ConsoleColor.White)
        {
            Console.Clear();

            Console.WriteLine("***********************************************************************\n*       IOTA Node API console v0.4 by Glimmstangel aka Snowlove       *\n***********************************************************************");
            Console.WriteLine("");

            if (!string.IsNullOrEmpty(msg))
            {
                Console.ForegroundColor = cc;
                    Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static byte[] SendCmd(string cmd, int required=0, string args = null)
        {
            if(required > 0 && args == null)
            {
                Console.WriteLine("Required parameters not met! type !API for list of API commands with their parameters.");
                return null;
            }

            if(args != null)
            {
                /*
                 * I really hate my mouse. double clicking piece of junk
                 * 
                 * This is very shaky and brutal to look at trying to handle user input to single, multiple args for multiple parameters
                 * Currently need to make it so it doesn't wrap integres in qoutations like "18" otherwise the API says no
                 */
                List<string> Params = new List<string>(args.Split(' '));
                if(Params.Count < required)
                {
                    Console.WriteLine("Required parameters not met! type !API for list of API commands with their parameters.");
                    return null;
                }

                int wtf = args.Split(',').Length;

                List<string> Stuffed = new List<string>();
                int internalc = 0;

                for (int j = 0; j < Params.Count; j++)
                {

                    int c = Params[j].Split(',').Length;
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < c; i++)
                    {//stuffing args from params
                        if (i == c-1)
                            sb.Append("\"{" + internalc + "}\"");
                        else
                            sb.Append("\"{" + internalc + "}\", ");
                        internalc++;
                    }

                    Stuffed.Add(sb.ToString());
                }


                string[] _f = new string[Stuffed.Count]; //3

                for (int i = 0; i < Stuffed.Count; i++)
                    _f[i] = Stuffed[i];
                    /*for (int i = 0; i < args.Length; i++)
                    {
                        _f = _f + "{" + i + "}";
                    }*/

                    //cmd = string.Format(cmd, _f);       //WHAT THE FUCK AM i DOING
                    //cmd = string.Format(cmd, args);

                //for (int i = 0; i < required; i++)
                //{
                    //cmd = string.Format(cmd, _f);
                    //cmd = string.Format(string.Format(cmd, _f), Params[i].Split(',')); //1, 2 WHERE THE FUCK IS 3 4?! is what the program wants to know.
                cmd = string.Format(cmd, _f);
                //Console.WriteLine(cmd);
                //Console.WriteLine(args.Replace(" ", "").Split(',').Length);
                //foreach (string s in args.Split(','))
                    //Console.WriteLine(">>" + s);
                //Console.ReadLine();
                    cmd = string.Format(cmd, args.Replace(" ", ",").Split(','));
                //}
            }

            using(WebClient wc = new WebClient())
            {
                wc.Headers["Content-Type"] = "application/json";
                wc.Headers["X-IOTA-API-Version"] = "1";

                try
                {
                    byte[] Returned = wc.UploadData(string.Format("http://{0}:{1}", NodeIP, NodePort), "POST", Encoding.Default.GetBytes("{" + cmd + "}"));

                    return Returned;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Error running the command please try again.");
                    Console.WriteLine(ex.ToString());
                    Console.ForegroundColor = ConsoleColor.White;

                    return null;
                }
            }
        }
    }
}