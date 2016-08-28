using ScpDriverInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void BringToFront(string title)
        {
            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == IntPtr.Zero)
            {
                return;
            }

            // Make Calculator the foreground application
            SetForegroundWindow(handle);
        }

        static void Main(string[] args)
        {
            ScrapMessages sm = new ScrapMessages();
            XboxNavigation xn = new XboxNavigation();
            Halo5Navigation hn = new Halo5Navigation();

            sm.LoginWithRawCookie();

            Console.WriteLine("5 seconds to focus Xbox Streaming App");
            BringToFront("Xbox");
            Thread.Sleep(5000);

            Thread thread = new Thread(() => Scrapthread(sm));
            thread.Start();


            Stopwatch sw = new Stopwatch();

            xn.SelectGame();
            hn.SelectCustomGameOnLaunch();
            hn.SelectMapAlpine();
            hn.SelectModeFFARockets();
            hn.StartGame();
            sw.Restart();

            while (true)
            {
                if (gamertag != null && !gamertag.Equals(prevGamertag))
                {
                    prevGamertag = gamertag;
                    Console.WriteLine("Sending invite to " + gamertag);
                    xn.SendInvite(gamertag);
                }

                if(sw.Elapsed.Minutes >= 13)
                {
                    hn.SelectMapAlpine();
                    hn.SelectModeFFARockets();
                    hn.StartGame();
                    sw.Restart();
                }
                Thread.Sleep(500);
            }
        }
    }
}