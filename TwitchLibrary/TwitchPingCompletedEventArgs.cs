using System;

namespace TwitchLibrary
{
    public class TwitchPingCompletedEventArgs : EventArgs
    {
        public TwitchPingCompletedEventArgs(TwitchServer server, TimeSpan ping)
        {
            this.Server = server;
            this.Ping = ping;
        }

        public TwitchServer Server { get; private set; }
        public TimeSpan Ping { get; private set; }
    }
}
