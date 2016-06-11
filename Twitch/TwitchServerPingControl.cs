using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Twitch
{
    public partial class TwitchServerPingControl : UserControl
    {
        public TwitchServerPingControl()
        {
            InitializeComponent();
        }

        public string ServerName
        {
            get { return ServerNameLabel.Text; }
            set { ServerNameLabel.Text = value; }
        }
        public int ServerPing
        {
            get { return int.Parse(ServerPingLabel.Text); }
            set { ServerPingLabel.Text = value.ToString(); }
        }
        public bool Pinging
        {
            set { checkBox1.Checked = value; }
        }
    }
}
