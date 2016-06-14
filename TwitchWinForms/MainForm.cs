using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private readonly Dictionary<string, TwitchServerPingControl> serverNameToControl;
        private readonly List<TwitchServer> servers;

        public MainForm()
        {
            InitializeComponent();
            serverNameToControl = new Dictionary<string, TwitchServerPingControl>();
            servers = new List<TwitchServer>();

            this.Shown += async (sender, args) =>
            {
                await LoadServers();
                BuildTable();
            };
            buttonStart.Click += (sender, args) => Ping();
            buttonStartParallelEvents.Click += (sender, args) => PingParallelEvents();
            buttonStartParallelAwait.Click += (sender, args) => PingParallelAwait();
        }

        private void BuildTable()
        {
            tableLayoutPanel.SuspendLayout();
            foreach (var server in servers)
            {
                var pingControl = new TwitchServerPingControl();
                pingControl.ServerName = server.Name;

                serverNameToControl.Add(server.Name, pingControl);
                tableLayoutPanel.Controls.Add(pingControl);
            }
            tableLayoutPanel.ResumeLayout();
        }

        private async Task LoadServers()
        {
            var serverList = await TwitchServerParser.GetAllTwitchServers().ConfigureAwait(false);
            //servers.AddRange(serverList.Where(x => x.Name.StartsWith("EU")));
            servers.AddRange(serverList);
            servers.Sort((x, y) => string.Compare(x.Name, y.Name));
        }

        private async void Ping()
        {
            var pinger = new TwitchServersPinger();
            foreach (var server in servers)
            {
                serverNameToControl[server.Name].Pinging = true;
                serverNameToControl[server.Name].ServerPing = await Task.Run(() => { return pinger.Ping(server).Milliseconds; });
                //serverNameToControl[server.Name].ServerPing = (await pinger.PingAsyncTask(server)).Milliseconds; // same?
                serverNameToControl[server.Name].Pinging = false;
            }
        }

        private async void PingParallelAwait()
        {
            var pinger = new TwitchServersPinger();
            var pingTasks = new List<Task<TwitchPingResult>>();
            foreach (var server in servers)
            {
                serverNameToControl[server.Name].Pinging = true;
                var pingTask = Task.Run<TwitchPingResult>(() => pinger.PingAsyncTaskArgs(server));
                pingTasks.Add(pingTask);
            }
            while (pingTasks.Count > 0)
            {
                var firstFinishedTask = await Task.WhenAny(pingTasks);
                pingTasks.Remove(firstFinishedTask);

                var pingResult = await firstFinishedTask;
                serverNameToControl[pingResult.Server.Name].ServerPing = pingResult.Ping.Milliseconds;
                serverNameToControl[pingResult.Server.Name].Pinging = false;
            }
        }

        private void PingParallelEvents()
        {
            var pinger = new TwitchServersPinger();
            pinger.PingCompleted += (sender, args) =>
            {
                var control = serverNameToControl[args.Server.Name];
                control.ServerPing = args.Ping.Milliseconds;
                control.Pinging = false;
            };
            foreach (var server in servers)
            {
                serverNameToControl[server.Name].Pinging = true;
                var task = new Task(() => pinger.PingAsyncVoid(server));
                task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
