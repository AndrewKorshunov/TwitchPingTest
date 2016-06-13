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

        private async void Ping()
        {
            var pinger = new TwitchServersPinger();
            foreach (var server in servers)
            {
                serverNameToControl[server.Name].Pinging = true;
                serverNameToControl[server.Name].ServerPing = await Task.Run(() => { return pinger.Ping(server).Milliseconds; });
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
                //MessageBox.Show(System.Threading.Thread.CurrentThread.GetHashCode().ToString() + " Started"); 
                var task = new Task(() => pinger.PingAsyncVoid(server));
                task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void BuildTable()
        {
            tableLayoutPanel.SuspendLayout();

            //tableLayoutPanel.ColumnStyles[0].SizeType = SizeType.AutoSize;
            foreach (var server in servers)
            {
                var control = new TwitchServerPingControl();
                control.ServerName = server.Name;
                control.ServerPing = 9999;
                //tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, control.Height));
                this.ResizeBegin += (sender, args) => { };
                this.SizeChanged += (sender, args) => { };
                tableLayoutPanel.Controls.Add(control);
                serverNameToControl.Add(server.Name, control);

                // Trying to solve problem with strange form width behaviour
                var t1 = this.Width;
                var t2 = tableLayoutPanel.Width;
                var t3 = tableLayoutPanel.ColumnStyles[0].Width;
                var t4 = control.Width;
                var t5 = tableLayoutPanel.RowStyles[0].Height;
            }
            //tableLayoutPanel.ColumnStyles[0].SizeType = SizeType.Absolute;
            //tableLayoutPanel.ColumnStyles[0].Width = 10;
            tableLayoutPanel.ResumeLayout();
        }

        private async Task LoadServers()
        {
            var serverList = await TwitchServerParser.GetAllTwitchServers().ConfigureAwait(false);
            //servers.AddRange(serverList.Where(x => x.Name.StartsWith("EU")));
            servers.AddRange(serverList);
            servers.Sort((x, y) => string.Compare(x.Name, y.Name));            
        }
    }
}
