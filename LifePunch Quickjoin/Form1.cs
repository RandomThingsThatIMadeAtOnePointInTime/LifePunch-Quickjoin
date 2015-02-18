using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QueryMaster;

namespace LifePunch_Quickjoin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IPAddress resolveIP(string hostname)
        {
            try
            {
                IPAddress[] address = Dns.GetHostAddresses(hostname);
                return address[0];
            }
            catch
            {
                return null;
            }
        }
        void gotoLink(string hostname)
        {
            Process process = new Process();
            try
            {
                // true is the default, but it is important not to set it to false
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "steam://connect/" + hostname;
                process.Start();
                Thread.Sleep(1000);
                process.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error launching: " + hostname + "\nInner: " + e.InnerException + "\nSource: " + e.Source + "\nString" + e.ToString());
            }
        }
        void updateServer(string id, string hostname, TextBox map, TextBox players, TextBox ping)
        {
            logBox.Items.Add("Attempting to look up " + hostname);
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, resolveIP(hostname).ToString(), 27015);
                try
                {
                    ServerInfo serverInfo = server.GetInfo();
                    logBox.Items.Add("Result for " + hostname + ":");
                    logBox.Items.Add(" -Map: " + serverInfo.Map);
                    logBox.Items.Add(" -Players/Maxplayers: " + serverInfo.Players + "/" + serverInfo.MaxPlayers);
                    logBox.Items.Add(" -Ping: " + serverInfo.Ping);
                    map.Text = serverInfo.Map;
                    players.Text = serverInfo.Players + "/" + serverInfo.MaxPlayers;
                    ping.Text = serverInfo.Ping.ToString();
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    logBox.Items.Add("ERROR: Failed to get information for \"" + hostname + "\"! (" + e.ToString() + ")");
                    map.Text = "Server offline";
                }
            }
            catch (System.NullReferenceException e)
            {
                logBox.Items.Add("ERROR: Unknown host \"" + hostname + "\"! (" + e.ToString() + ")");
                map.Text = "Server offline";
            }
        }
        void refreshServers()
        {
            logBox.Items.Clear();
            // Jailbreak
            updateServer("Jailbreak 1", Settings1.Default.Jailbreak1Host, jbMap1, jbPlayers1, jbPing1);
            updateServer("Jailbreak 2", Settings1.Default.Jailbreak2Host, jbMap2, jbPlayers2, jbPing2);
            updateServer("Jailbreak 3", Settings1.Default.Jailbreak3Host, jbMap3, jbPlayers3, jbPing3);
            updateServer("Jailbreak 4", Settings1.Default.Jailbreak4Host, jbMap4, jbPlayers4, jbPing4);
            updateServer("Jailbreak 5", Settings1.Default.Jailbreak5Host, jbMap5, jbPlayers5, jbPing5);
            updateServer("Jailbreak 6", Settings1.Default.Jailbreak6Host, jbMap6, jbPlayers6, jbPing6);
            updateServer("Jailbreak 7", Settings1.Default.Jailbreak7Host, jbMap7, jbPlayers7, jbPing7);
            updateServer("Jailbreak 8", Settings1.Default.Jailbreak8Host, jbMap8, jbPlayers8, jbPing8);
            updateServer("Jailbreak 9", Settings1.Default.Jailbreak9Host, jbMap9, jbPlayers9, jbPing9);
            // Deathrun
            updateServer("Deathrun 1", Settings1.Default.Deathrun1Host, drMap1, drPlayers1, drPing1);
            updateServer("Deathrun 2", Settings1.Default.Deathrun2Host, drMap2, drPlayers2, drPing2);
            updateServer("Deathrun 3", Settings1.Default.Deathrun3Host, drMap3, drPlayers3, drPing3);
            updateServer("Deathrun 4", Settings1.Default.Deathrun4Host, drMap4, drPlayers4, drPing4);
            updateServer("Deathrun 5", Settings1.Default.Deathrun5Host, drMap5, drPlayers5, drPing5);
            // Bunnyhop
            updateServer("Bunnyhop 1", Settings1.Default.Bunnyhop1Host, bhMap1, bhPlayers1, bhPing1);
            updateServer("Bunnyhop 2", Settings1.Default.Bunnyhop2Host, bhMap2, bhPlayers2, bhPing2);
            updateServer("Bunnyhop 3", Settings1.Default.Bunnyhop3Host, bhMap3, bhPlayers3, bhPing3);
            // Gungame
            updateServer("Gungame 1", Settings1.Default.Gungame1Host, ggMap1, ggPlayers1, ggPing1);
            updateServer("Gungame 2", Settings1.Default.Gungame2Host, ggMap2, ggPlayers2, ggPing2);
            // Cinema
            updateServer("Cinema", Settings1.Default.CinemaHost, cnMap1, cnPlayers1, cnPing1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshServers();

            // Version check
            int version = 1;
            using (WebClient client = new WebClient())
            {
                int latestVersion = Convert.ToInt32(client.DownloadString("https://raw.githubusercontent.com/Scarsz/LifePunch-Quickjoin/master/version"));
                if (version < latestVersion)
                {
                    MessageBox.Show("This version of Quickjoin is outdated, please download the newest version.");
                    System.Diagnostics.Process.Start("https://github.com/Scarsz/LifePunch-Quickjoin/releases/latest");
                }
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            refreshServers();
        }

        // Join jailbreak
        private void jbJoin1_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak1Host);
        }
        private void jbJoin2_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak2Host);
        }
        private void jbJoin3_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak3Host);
        }
        private void jbJoin4_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak4Host);
        }
        private void jbJoin5_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak5Host);
        }
        private void jbJoin6_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak6Host);
        }
        private void jbJoin7_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak7Host);
        }
        private void jbJoin8_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak8Host);
        }
        private void jbJoin9_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Jailbreak9Host);
        }
        // Join deathrun
        private void drJoin1_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Deathrun1Host);
        }
        private void drJoin2_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Deathrun2Host);
        }
        private void drJoin3_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Deathrun3Host);
        }
        private void drJoin4_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Deathrun4Host);
        }
        private void drJoin5_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Deathrun5Host);
        }
        // Join bunnyhop
        private void bhJoin1_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Bunnyhop1Host);
        }
        private void bhJoin2_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Bunnyhop2Host);
        }
        private void bhJoin3_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Bunnyhop3Host);
        }
        // Join gungame
        private void ggJoin1_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Gungame1Host);
        }
        private void ggJoin2_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.Gungame2Host);
        }
        // Join cinema
        private void cnJoin1_Click(object sender, EventArgs e)
        {
            gotoLink(Settings1.Default.CinemaHost);
        }
    }
}
