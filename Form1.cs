using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PlaylistMaker
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            ReadSettings();
        }

        private void ReadSettings()
        {
            txtDirectory.Text = Properties.Settings.Default.MediaDirectory;
            txtFilename.Text = Properties.Settings.Default.PlaylistFileName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            txtPlaylist.Text = "";

            try
            {
                MakePlaylist();
                SaveSettings();
                lblResult.Text = "Playlist created";
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }

        }

        private void SaveSettings()
        {
            Properties.Settings.Default.MediaDirectory = txtDirectory.Text;
            Properties.Settings.Default.PlaylistFileName = txtFilename.Text;
            Properties.Settings.Default.Save();
        }

        private void MakePlaylist()
        {
            Playlist playlist = new Playlist(txtDirectory.Text, txtFilename.Text);
            playlist.Make();
            txtPlaylist.Lines = playlist.GetFilenames();
        }
        
    }
}