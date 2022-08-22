using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _勒索程序_
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                IShellLink link = (IShellLink)new ShellLink();

                // setup shortcut information
                link.SetDescription("换壁纸程序 作者：@阳开蕊");
                string path = Assembly.GetExecutingAssembly().Location;
                link.SetPath(@path);

                // save it
                IPersistFile file = (IPersistFile)link;
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                file.Save(Path.Combine(desktopPath, "换壁纸程序 作者：@阳开蕊.lnk"), false);
            }
            if (checkBox1.Checked==false)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                System.IO.File.Delete(desktopPath+ "/换壁纸程序 作者：@阳开蕊.lnk");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string startpath = "C:\\Users\\"+ System.Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup";
            if (checkBox2.Checked == true)
            {
                IShellLink link = (IShellLink)new ShellLink();

                // setup shortcut information
                link.SetDescription("换壁纸程序 作者：@阳开蕊");
                string path = Assembly.GetExecutingAssembly().Location;
                link.SetPath(@path);

                // save it
                IPersistFile file = (IPersistFile)link;
                
                file.Save(Path.Combine(startpath, "换壁纸程序.lnk"), false);
            }
            if (checkBox2.Checked == false)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                System.IO.File.Delete(startpath + "/换壁纸程序.lnk");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start( "https://space.bilibili.com/1123306452");
        }
        public static void KillProcess(string processName)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains(processName))
                {
                    try
                    {
                        p.Kill();
                        p.WaitForExit(); // possibly with a timeout
                        MessageBox.Show("成功关闭动态壁纸！", "成功关闭");
                    }
                    catch (Win32Exception e)
                    {
                        MessageBox.Show("错误：" + e.Message.ToString(), "出错");
                    }

                }

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (
                System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcesses()
                ) { 
                if (thisproc.ProcessName.Equals("ffplay")) 
                { 
                    thisproc.Kill();
                    MessageBox.Show("成功关闭！","成功关闭！");
                }
            }
            //MessageBox.Show("成功关闭！");
        }
    }
}
