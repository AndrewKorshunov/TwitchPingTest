using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TwitchLibrary
{
    /*
    public class TwitchServersPingerFake
    {
        private const int rtmpPort = 1935;

        // Twitch ignores ICMP protocol of regular ping
        public Task<TimeSpan>[] PingAllServers(IEnumerable<TwitchServer> servers)
        {
            var pingTasks = new List<Task<TimeSpan>>();
            foreach (var server in servers)
            {
                string hostname = GetHostnameFromUrl(server.UrlTemplate);
                pingTasks.Add(PingAsync(hostname));
            }
            //return Task.WhenAll(pingTasks.ToArray());
            return pingTasks.ToArray();
        }

        public async Task<TimeSpan> PingAsync(string hostToPing)
        {
            var tcpClient = new TcpClient();
            var sw = new Stopwatch();
            tcpClient = new TcpClient();
            sw.Start();

            await tcpClient.ConnectAsync(hostToPing, rtmpPort);
            sw.Stop();
            tcpClient.GetStream().Close();
            tcpClient.Close();
            return sw.Elapsed;
        }

        public event Action<TwitchServer> PingCompleted;

        static public TimeSpan Ping(TwitchServer server)
        {
            var tcpClient = new TcpClient();
            var sw = new Stopwatch();
            string hostToPing = GetHostnameFromUrl(server.UrlTemplate);
            sw.Start();
            tcpClient.Connect(hostToPing, rtmpPort);
            sw.Stop();
            tcpClient.GetStream().Close();
            tcpClient.Close();

            return sw.Elapsed;
        }

        static private string GetHostnameFromUrl(string urlTemplate)
        {
            // example "rtmp://live-mia.twitch.tv/app/{stream_key}"
            var result = urlTemplate
                .SkipWhile(x => x != '/')  // skip protocol
                .Skip(2)  // 
                .TakeWhile(x => x != '/');  // take only host address
            return new string(result.ToArray());
        }

        private void InvokeEvent(Action action)
        {
            if (action != null)
                action();
        }
    }
    */
}


/*
namespace Test
{
    class Program
    {
        static void Main()
        {
            PingReply[] replies = PingRange("192.168.1.1", "192.168.1.104");
            foreach (PingReply reply in replies)
            {
                Console.WriteLine(reply.Address + " " + reply.Status);
            }
            Console.ReadKey();
        }

        public static PingReply[] PingRange(string from, string to)
        {
            List<Task<PingReply>> tasks = new List<Task<PingReply>>();

            for (uint current = from.ToUInt(); current <= to.ToUInt(); current++)
            {
                IPAddress ip = current.ToIPAddress();
                Task<PingReply> pingTask = PingIPAsync(ip);
                tasks.Add(pingTask);
            }

            //Wait for the tasks to finish
            Task.WaitAll(tasks.ToArray());
            //Return the ping replies
            return tasks.Select(x => x.Result).ToArray();
        }
        
        public static Task<PingReply> PingIPAsync(IPAddress ip)
        {
            TaskCompletionSource<PingReply> tcs = new TaskCompletionSource<PingReply>();
            Ping ping = new Ping();

            ping.PingCompleted += (sender, pingCompletedEventArgs) =>
            {
                //The task completes when the TaskCompletionSource's result has been set!
                tcs.SetResult(pingCompletedEventArgs.Reply);
            };

            ping.SendAsync(ip, new object());
            return tcs.Task;
        }
    }
}
*/
