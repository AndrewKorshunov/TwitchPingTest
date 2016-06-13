using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TwitchLibrary
{
    public class TwitchPingResult
    {
        public TwitchPingResult(TwitchServer server, TimeSpan ping)
        {
            this.Ping = ping;
            this.Server=server;
        }

        public TwitchServer Server { get; private set; }
        public TimeSpan Ping { get; private set; }
    }

    public class TwitchServersPinger
    {
        // Twitch server ignores ICMP packets of regular ping
        private const int rtmpPort = 1935;

        public event EventHandler<TwitchPingCompletedEventArgs> PingCompleted;

        public TimeSpan Ping(TwitchServer server)
        {
            var tcpClient = new TcpClient();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            tcpClient.Connect(server.Url, rtmpPort);
            stopWatch.Stop();
            tcpClient.GetStream().Close();
            tcpClient.Close();
            return stopWatch.Elapsed;
        }

        public async Task<TimeSpan> PingAsyncTask(TwitchServer server)
        {
            var tcpClient = new TcpClient();
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            await tcpClient.ConnectAsync(server.Url, rtmpPort).ConfigureAwait(false);
            stopWatch.Stop();

            tcpClient.GetStream().Close();
            tcpClient.Close();

            return stopWatch.Elapsed;
        }

        public async Task<TwitchPingResult> PingAsyncTaskArgs(TwitchServer server)
        {
            var tcpClient = new TcpClient();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            await tcpClient.ConnectAsync(server.Url, rtmpPort).ConfigureAwait(false);
            stopWatch.Stop();
            tcpClient.GetStream().Close();
            tcpClient.Close();

            return new TwitchPingResult(server, stopWatch.Elapsed);
        }

        public async void PingAsyncVoid(TwitchServer server)
        {
            var tcpClient = new TcpClient();
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            await tcpClient.ConnectAsync(server.Url, rtmpPort);
            stopWatch.Stop();

            OnPingCompleted(new TwitchPingCompletedEventArgs(server, stopWatch.Elapsed));
            tcpClient.GetStream().Close();
            tcpClient.Close();
        }
        
        private void OnPingCompleted(TwitchPingCompletedEventArgs e)
        {
            var tempHandler = PingCompleted;
            if (tempHandler != null)
                tempHandler(this, e);
        }
    }
}