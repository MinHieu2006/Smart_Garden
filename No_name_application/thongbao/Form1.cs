using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace thongbao
{
    public partial class Form1 : Form
    {
        double temp;
        string tt;
        public Form1()
        {
            InitializeComponent();
            read();
            string s = dateTimePicker1.Value.ToString();
            MessageBox.Show(s);
        }
        void read()
        {
                string line = "";
                FileStream F = new FileStream(@"C:\Users\LENOVO\OneDrive\Máy tính\No_name_application\WindowsFormsApp1\temp.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader sr = new StreamReader(F);
                line = sr.ReadLine();
                temp = Convert.ToDouble(line);
            sr.Close();
            F.Close();

            FileStream A = new FileStream(@"C:\Users\LENOVO\OneDrive\Máy tính\No_name_application\WindowsFormsApp1\thoitiet.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader ab = new StreamReader(A);
            tt = ab.ReadLine();
            MessageBox.Show(temp.ToString() + " " + tt);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string line;
            string url = @"C:\Users\LENOVO\OneDrive\Máy tính\No_name_application\WindowsFormsApp1\bin\Debug\setting.txt";
             FileStream F = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(F);
            line = sr.ReadToEnd();
            this.Hide();
            RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\thongbao");
            //mo registry khoi dong cung win
            RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            if (line == "true")
            {
                
                string keyvalue = "1";
                //string subkey = "Software\\ManhQuyen";
                try
                {
                    //chen gia tri key
                    regkey.SetValue("Index", keyvalue);
                    //regstart.SetValue("taoregistrytronghethong", "E:\\Studing\\Bai Tap\\CSharp\\Channel 4\\bai temp\\tao registry trong he thong\\tao registry trong he thong\\bin\\Debug\\tao registry trong he thong.exe");
                    regstart.SetValue("thongbao", Application.StartupPath + "\\thongbao.exe");
                    ////dong tien trinh ghi key
                    //regkey.Close();
                }
                catch (System.Exception ex)
                {
                }
            } else if (line == "false")
            {
                regstart.DeleteValue("thongbao");
            }    
        }
    }
}
