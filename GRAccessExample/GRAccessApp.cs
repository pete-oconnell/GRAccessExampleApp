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
using ArchestrA.GRAccess;

namespace GRAccessExample
{
    public partial class GRAccessExampleApp : Form
    {
        GRAccessApp grAccess = new GRAccessAppClass();
        string galaxyName = "";
        string grUser = "";
        string grPass = "";
        IGalaxies galaxies;
        ICommandResult cmdResult;
        bool loggedIn = false;

        public GRAccessExampleApp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Fetching galaxies...";
            galaxies = grAccess.QueryGalaxies("localhost");
            if (!grAccess.CommandResult.Successful)
            {
                toolStripStatusLabel1.Text = "Failed to fetch galaxies, is this a GR node?";
            }
            else
            {
                foreach (IGalaxy galaxy in galaxies)
                {
                    comboGalaxy.Items.Add(galaxy.Name);
                }
                toolStripStatusLabel1.Text = "Completed.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboGalaxy.Text != "")
            {
                toolStripStatusLabel1.Text = "Attempting to login...";
                IGalaxy galaxy = galaxies[comboGalaxy.Text];
                galaxy.Login(grUser, grPass);
                if (!galaxy.CommandResult.Successful)
                {
                    loggedIn = false;
                    toolStripStatusLabel1.Text = "Failed to log into galaxy - " + galaxy.CommandResult.Text;
                }
                else
                {
                    loggedIn = true;
                    toolStripStatusLabel1.Text = "Logged in successfully";
                }

            }
            else
            {
                loggedIn = false;
                toolStripStatusLabel1.Text = "Please select a galaxy.";
            }
        }
    }
}
