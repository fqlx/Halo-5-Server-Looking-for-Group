using ScpDriverInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Halo_5_Server_Looking_for_Group
{

    class Program
    {
        const int CONTROLLER_NUMBER = 1;

        static string prevGamertag = "baby jesus";
        static string gamertag = null;

        public static void Scrapthread(ScrapMessages sm)
        {
            gamertag = sm.GetNextGamertagRequestedToJoin();
            Thread.Sleep(60000);
        }

        public static void dc()
        {
            XboxCommands xc = new XboxCommands();

            while (true)
            {
                X360Controller controller = new X360Controller();
                xc.SendtoController(controller);
                controller.RightTrigger ^= 1;
                Thread.Sleep(20);
            }
        }

        static void Main(string[] args)
        {
            ScrapMessages sm = new ScrapMessages();
            XboxCommands xc = new XboxCommands();

            sm.LoginWithRawCookie();

            Console.WriteLine("10 seconds to focus Xbox Streaming App");
            Thread.Sleep(5000);

            Thread thread = new Thread(() => Scrapthread(sm));
            thread.Start();

            Thread t2 = new Thread(() => dc());
            t2.Start();

            while (true)
            {
                if (gamertag == null)
                    continue;
                if (!gamertag.Equals(prevGamertag))
                {
                    prevGamertag = gamertag;
                    Console.WriteLine("Sending invite to " + gamertag);
                    xc.SendInvite(gamertag);
                }
                Thread.Sleep(500);
            }
        }
    }
}