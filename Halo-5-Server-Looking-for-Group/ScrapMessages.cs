using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Halo_5_Server_Looking_for_Group
{
    class ScrapMessages
    {
        //private CookieContainer cookiecontainer = new CookieContainer();
        private string rawcookie;
        const string INVITE = "inv";

        public string GetNextGamertagRequestedToJoin()
        {
            string messages = ReadMessages();
            Tuple<string, string> lastmessage = GetLastMessage(messages);

            string gamertag = lastmessage.Item1;
            string message = lastmessage.Item2;

            if(message.Equals(INVITE))
            {
                return gamertag;
            }

            return null;
        }

        public Tuple<string, string> GetLastMessage(string messages)
        {
            Match gamertag = Regex.Match(messages, "<div class=\"senderGamertag\">(.*)</div>",
                                         RegexOptions.RightToLeft);

            Match message = Regex.Match(messages, "<div class=\"messageBody\">(.*)</div>",
                             RegexOptions.RightToLeft);

            if (gamertag.Success && message.Success)
            {
                Console.WriteLine(gamertag.Groups[1] + ":  " + message.Groups[1]);
                return Tuple.Create(gamertag.Groups[1].ToString(),  message.Groups[1].ToString());
            }

            return null;
        }

        public string ReadMessages()
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Cookie] = rawcookie;
             byte[] messages = client.DownloadData("https://account.xbox.com/en-US/Messages/UserConversation");
            string str = Encoding.Default.GetString(messages);
            return str;
        }

        public void LoginWithRawCookie()
        {

            int bufSize = 10000;
            Stream inStream = Console.OpenStandardInput(bufSize);
            Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));

            Console.WriteLine("***DO NOT SHARE YOUR COOKIE WITH ANYONE***");
            Console.WriteLine("Enter raw cookie:");

            rawcookie = Console.ReadLine();
        }

        /* Not working
        public bool Login(string email, string password)
        {
            string URI = "https://login.live.com/ppsecure/post.srf";
            string formdata = "loginfmt=" + email
                            + "&login=" + email
                            + "&passwd=" + password
                            + extras;

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application /x-www-form-urlencoded");
                wc.Headers.Add("Origin", "https://login.live.com");
                wc.Headers.Add("Referer", "https://login.live.com/login.srf");


                byte[] messages = wc.DownloadData("https://account.xbox.com/en-US/Messages/UserConversation");
                Console.WriteLine(wc.ResponseHeaders["Set-Cookie"]);
            }
            return true;
        }
        */


    }
}
