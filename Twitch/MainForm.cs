using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLibrary;

namespace Twitch
{
    public partial class MainForm : Form
    {
        Dictionary<string, TwitchServerPingControl> serversDict;
        List<TwitchServer> servers;

        public MainForm()
        {
            InitializeComponent();
            serversDict = new Dictionary<string, TwitchServerPingControl>();

            this.Shown += (sender, args) => LoadServers();
            buttonStart.Click += (sender, args) => Ping();
            buttonStartParallelEvents.Click += (sender, args) => PingParallelEvents();
            buttonStartParallelAwait.Click += (sender, args) => PingParallelAwait();
        }

        private void PingParallelEvents() //Working
        {
            var tasks = new List<Task>();
            foreach (var server in servers)
            {
                var pinger = new TwitchServersPinger();
                serversDict[server.Name].Pinging = true;
                pinger.PingCompleted += (sender, args) =>
                    {
                        var ct = serversDict[args.Server.Name];
                        ct.ServerPing = args.Ping.Milliseconds;
                        ct.Pinging = false;
                    };
                //var task = new Task(async () => await pinger.PingAsync(server));
                var task = new Task(() => pinger.PingAsyncVoid(server));
                tasks.Add(task);
                task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void PingParallelAwait() //Working 
        {
            var tasks = new List<Task>();
            foreach (var server in servers)
            {
                var pinger = new TwitchServersPinger();
                serversDict[server.Name].Pinging = true;
                pinger.PingCompleted += (sender, args) =>
                {
                    var ct = serversDict[args.Server.Name];
                    ct.ServerPing = args.Ping.Milliseconds;
                    ct.Pinging = false;
                };
                var task = new Task(async () => await pinger.PingAsyncTask(server));
                //var task = new Task(() => pinger.PingAsyncTask(server));
                tasks.Add(task);
                task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private async void Ping()
        {
            var pinger = new TwitchServersPinger();
            foreach (var server in servers)
            {
                serversDict[server.Name].Pinging = true;
                var task = new Task<int>(() => { return pinger.Ping(server).Milliseconds; });
                task.Start();
                serversDict[server.Name].ServerPing = await task;
                serversDict[server.Name].Pinging = false;
            }
        }

        private void LoadServers()
        {
            servers = new List<TwitchServer>(TwitchServerParser.GetAllTwitchServers().Where(x => x.Name.StartsWith("US")));
            servers.Sort((x, y) => string.Compare(x.Name, y.Name));
            tableLayoutPanel.RowCount = servers.Count;
            foreach (var server in servers)
            {
                var control = new TwitchServerPingControl();
                control.ServerName = server.Name;
                control.ServerPing = 9999;
                tableLayoutPanel.Controls.Add(control);
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));

                serversDict.Add(server.Name, control);
            }
        }
    }
}
