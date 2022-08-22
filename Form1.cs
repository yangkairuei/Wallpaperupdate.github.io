using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace _勒索程序_
{
    public partial class Form1 : Form
    {

        //[DllImport("user32.dll", EntryPoint = "SetParent")]
        //private static extern int SetParent(int hWndChild, int hWndNewParent);
        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        private static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindowExA")]
        private static extern IntPtr FindWindowExA(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", EntryPoint = "GetClassNameA")]
        private static extern IntPtr GetClassNameA(IntPtr hWnd, IntPtr lpClassName, int nMaxCount);
        [DllImport("user32.dll", EntryPoint = "GetParent")]
        private static extern IntPtr GetParent(IntPtr hWnd);

        public static void SetFather(int form)
        {
            SetParent((int)form, GetBackground());
        }

        private static int GetBackground()
        {
            
            {
                IntPtr background = IntPtr.Zero;
                IntPtr father = FindWindowA("progman", "Program Manager");
                IntPtr workerW = IntPtr.Zero;
                do
                {
                    workerW = (IntPtr)FindWindowExA((int)IntPtr.Zero, (int)workerW, "workerW", 0);
                    if (workerW != IntPtr.Zero)
                    {
                        char[] buff = new char[200];
                        IntPtr b = Marshal.UnsafeAddrOfPinnedArrayElement(buff, 0);
                        int ret = (int)GetClassNameA(workerW, b, 400);
                        if (ret == 0) throw new Exception("出错");
                    }
                    if (GetParent(workerW) == father)
                    {
                        background = workerW;
                    }
                } while (workerW != IntPtr.Zero);
                return (int)background;
            }
        }

        //换背景dll
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        //桌面句柄dll
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern int GetDesktopWindow();
        //隐藏窗口dll
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(int hwnd, int nCmdShow);
        //寻找窗口dll(根据类名)
        [DllImport("user32.dll", EntryPoint = "FindWindowExA")]
        public static extern int FindWindowExA(int hWndParent, int hWndChildAfter, string lpszClass, int lpszWindow);
        //寻找窗口dll(根据标题)
        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        public static extern int FindWindowAb(string lpszClass, string Windowtitle);
        //发送信息dll(普通)
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessageA(int hWnd, int Msg, int wParam, int lParam);
        //发送信息dll(等待)
        [DllImport("user32.dll", EntryPoint = "SendMessageTimeoutA")]
        public static extern int SendMessageTimeoutA(int hWnd, int Msg, int wParam, int lParam, int fuFlags, int uTimeout, int lpdwsult);
        //直父窗口
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(int zihWnd, int fuhwnd);
        [DllImport("bizhi.dll", EntryPoint = "dongtaizhuomian")]
        public static extern int dongtaizhuomian(string 路径);
        [DllImport("bizhi.dll", EntryPoint = "bofangshipin")]
        public static extern int bofangshipin(string 路径);
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern int SetForegroundWindow(int hWnd);
        //XXXX
        //[DllImport("user32.dll", EntryPoint = "EnumWindows")]
        //public static extern int EnumWindows(Func zhizhen, int canshu);

        public bool StartProcess(string runFilePath, params string[] args)
        {
            string s = "";
            foreach (string arg in args)
            {
                s = s + arg + " ";
            }
            s = s.Trim();
            Process process = new Process();//创建进程对象    
            ProcessStartInfo startInfo = new ProcessStartInfo(runFilePath, s); // 括号里是(程序名,参数)
            process.StartInfo = startInfo;
            process.Start();
            return true;
        }
        public Form1()
        {
            InitializeComponent();
        }
        void StartNewProcess(string processName)
        {
            Process p = new Process();//创建Process 类的对象
            p.StartInfo.FileName = processName;
            p.Start();//启动进程
        }
        public bool EnumWindowsProc( int hwnd)
        {

            int hDefView = FindWindowExA(hwnd, 0, "SHELLDLL_DefView", 0);
            if (hDefView != 0)
            {
                // 找它的下一个窗口，类名为WorkerW，隐藏它
                int hWorkerw = FindWindowExA(0, hwnd, "WorkerW", 0);
                ShowWindow(hWorkerw, 0);
                return true;
            }
            return false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //string shipin = "C:\\Users\\lsr22\\source\\repos\\《勒索程序》\\bin\\Release\\ffplay.exe D:\\360MoveData\\Users\\lsr22\\Desktop\\1.mp4  -noborder -x 1920 -y 1080  -loop 0";
            

            
            
            //试试
            //ShowWindow(FindWindowA("CabinetWClass", "此电脑"), 0);
        }
        
        //按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //换背景
            SystemParametersInfo(20, 1, textBox1.Text, 1);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //any code
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //创建图片选择框
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "选择背景图片";
            openFileDialog1.InitialDirectory = @"/Pictures";
            openFileDialog1.Filter = "All files（*.*）|*.*|所有可支持的文件|*.jpg;*.png;*.bmp;*.mp4;*.mpeg;*.wma;*.wmv;*.wav;*.avi;*.mp3;*.m4a";
            
            openFileDialog1.FilterIndex = 2;
            
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获取路径
                textBox1.Text = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                //加载图片
                if (textBox1.Text != ".mp4" || textBox1.Text != ".mpeg" || textBox1.Text != ".wma" || textBox1.Text != ".wav" || textBox1.Text != ".avi")
                {
                    try
                    {
                        this.pictureBox2.Load(textBox1.Text);
                    }
                    catch
                    {
                        try
                        {
                            bofangshipin(textBox1.Text);
                        }
                        catch
                        {
                            MessageBox.Show("e,格式似乎不支持。。。", "作者阳开蕊の遗憾");
                        }
                        

                    }
                    



                }
                else {
                    //string exe_path = "ffplay.exe";  // 被调exe
                    //string[] the_args = { textBox1.Text, "-noborder", " -x 192", "-y 108", " -loop 0" };
                    //StartProcess(exe_path, the_args);
                    bofangshipin(textBox1.Text);
                    
                    

                }
                
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
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
                        MessageBox.Show("成功关闭动态壁纸！","成功关闭");
                    }
                    catch (Win32Exception e)
                    {
                        MessageBox.Show("错误："+e.Message.ToString(),"出错");
                    }
                    
                }

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dongtaizhuomian(textBox1.Text);
            //System.Threading.Thread.Sleep(200);
            int hFfplay = FindWindowAb("SDL_app", textBox1.Text);
            //MessageBox.Show(hFfplay.ToString("G"));
            SetForegroundWindow(hFfplay);
            //string exe_path = "ffplay.exe";  // 被调exe
            //string[] the_args = { "1.mp4", "-noborder", " -x 192", "-y 108", " -loop 0" };   // 被调exe接受的参数
            //
            //if (StartProcess(exe_path, the_args))                                                          //{
            //{
            //System.Threading.Thread.Sleep(2000);

            //桌面句柄

            // int hFfplay = FindWindowAb("SDL_app", "1.mp4");

            //SetFather(hFfplay);
            //MessageBox.Show(hWorkerw.ToString("G"));
            //ShowWindow(hWorkerw, 0);
            //SetParent(hFfplay, hProgman);

            //MessageBox.Show(hFfplay.ToString("G"));
            //};

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            this.Hide();
        }

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); 
            form2.ShowDialog();  
            
        }
    }
    
        
        
    
    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    internal class ShellLink
    {
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    internal interface IShellLink
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
        void GetIDList(out IntPtr ppidl);
        void SetIDList(IntPtr pidl);
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        void GetHotkey(out short pwHotkey);
        void SetHotkey(short wHotkey);
        void GetShowCmd(out int piShowCmd);
        void SetShowCmd(int iShowCmd);
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
        void Resolve(IntPtr hwnd, int fFlags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }
}
