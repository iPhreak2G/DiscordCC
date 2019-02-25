using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DiscordCacheCleaner
{
    public partial class Form1 : Form
    {
        public string ver = "discord";
        public Form1(){
            InitializeComponent();
        }

        void Copy(string sourceDir, string targetDir){
            Directory.CreateDirectory(targetDir);

            foreach(var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));

            foreach(var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

        void Delete(string targetDir){
            System.IO.DirectoryInfo di = new DirectoryInfo(targetDir);

            foreach (FileInfo file in di.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }

        private void button1_Click(object sender, EventArgs e){
            try{
                Delete(@"C:\DiscordCC");
                string discord = @"\discord";
                string userName = Environment.UserName;
                string newdir;
                string olddir;
                if (userName.Contains("-PC")) userName.Replace("-PC", "");
                if (Directory.Exists(@"C:\Users\" + userName + @"\AppData\Roaming\discord") && Directory.Exists(@"C:\Users\" + userName + @"\AppData\Roaming\discordptb"))
                {
                    DialogResult res = MessageBox.Show("You Have Both DiscordPTB & Discord Installed Please Select Yes for Discord or No for DiscordPTB", "Multiple Discord Clients Found", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        label1.Text = "Version: Discord";
                        ver = "discord";
                        discord = @"\discord";
                        newdir = @"C:\DiscordCC";
                        olddir = @"C:\Users\" + userName + @"\AppData\Roaming" + discord + @"\Cache";
                        Directory.CreateDirectory(newdir);
                        Copy(olddir, newdir);
                        DirectoryInfo d = new DirectoryInfo(newdir);
                        FileInfo[] Files = d.GetFiles("*");
                        foreach (FileInfo file in Files)
                        {
                            string f = file.Name + ".png";
                            File.Move(d + @"\" + file.Name, d + @"\" + f);
                            listBox1.Items.Add(d + @"\" + f);
                        }
                    }
                    else if (res == DialogResult.No)
                    {
                        label1.Text = "Version: DiscordPTB";
                        ver = "discordptb";
                        discord = @"\discordptb";
                        newdir = @"C:\DiscordCC";
                        olddir = @"C:\Users\" + userName + @"\AppData\Roaming" + discord + @"\Cache";
                        Directory.CreateDirectory(newdir);
                        Copy(olddir, newdir);
                        DirectoryInfo d3 = new DirectoryInfo(newdir);
                        FileInfo[] Files3 = d3.GetFiles("*");
                        foreach (FileInfo file in Files3)
                        {
                            string f = file.Name + ".png";
                            File.Move(d3 + @"\" + file.Name, d3 + @"\" + f);
                            listBox1.Items.Add(newdir + @"\" + f);
                        }
                    }
                }
                else
                {
                    if (Directory.Exists(@"C:\Users\" + userName + @"\AppData\Roaming\discord"))
                    {
                        label1.Text = "Version: Discord";
                        ver = "discord";
                        discord = @"\discord";
                    }
                    if (Directory.Exists(@"C:\Users\" + userName + @"\AppData\Roaming\discord"))
                    {
                        label1.Text = "Version: DiscordPTB";
                        ver = "discordptb";
                        discord = @"\discordptb";
                    }
                    newdir = @"C:\DiscordCC";
                    olddir = @"C:\Users\" + userName + @"\AppData\Roaming" + discord + @"\Cache";
                    Directory.CreateDirectory(newdir);
                    Copy(olddir, newdir);
                    DirectoryInfo d2 = new DirectoryInfo(newdir);
                    FileInfo[] Files2 = d2.GetFiles("*");
                    foreach (FileInfo file in Files2)
                    {
                        string f = file.Name + ".png";
                        File.Move(d2 + @"\" + file.Name, d2 + @"\" + f);
                        listBox1.Items.Add(d2 + @"\" + f);
                    }
                }
            }catch(Exception ex){
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show("Error: "+ex.Message, "An Error Occurred: "+line+"!");
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = Environment.UserName;
                Delete(@"C:\DiscordCC");
                Delete(@"C:\Users\" + userName + @"\AppData\Roaming\" + ver + @"\Cache");
                System.Diagnostics.Process.Start("cmd.exe", "/c taskkill /IM "+ver+".exe /T /F");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "An Error Has Occurred!");
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try{
                //MessageBox.Show(listBox1.SelectedItem.ToString());
                File.Delete(listBox1.SelectedItem.ToString());
                listBox1.Items.Remove(listBox1.SelectedItem);
            }catch (Exception ex){
                MessageBox.Show("Error: " + ex.Message, "An Error Has Occurred!");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.ImageLocation = listBox1.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "An Error Has Occurred!");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created By: Justin Stolle (Cyanokit)\nSpecial Thanks to Tex for the idea!\n\nBuild 1.0.0.0\nDate: 2/24/2019");
        }

        private void checkVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userName = Environment.UserName;
            if (Directory.Exists(@"C:\Users\" + userName + @"\AppData\Roaming\discord") && Directory.Exists(@"C:\Users\" + userName + @"\AppData\Roaming\discord"))
            {
                MessageBox.Show("You have both version of discord!");
                ver = "Version: Both";
            }
            else if (Directory.Exists(@"C:\Users\" + userName + @"\AppData\Roaming\discord"))
            {
                MessageBox.Show("You have the regular versions of discord installed!");
                ver = "Version: Discord";
            }
            else if (Directory.Exists(@"C:\Users\" + userName + @"\AppData\Roaming\discordptb"))
            {
                MessageBox.Show("You have DiscordPTB installed!");
                ver = "Version: DiscordPTB";
            }
            else
            {
                MessageBox.Show("You do not have discord installed!");
                ver = "Version: N/A";
            }
        }
    }
}
