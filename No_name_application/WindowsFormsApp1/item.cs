using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApp1
{
    public partial class item : Form
    {
        public item()
        {
            InitializeComponent();
        }


        private void them_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\LENOVO\OneDrive\Máy tính\No_name_application\WindowsFormsApp1\setting_time.txt";
            //FileStream F = new FileStream(@"C:\Users\LENOVO\OneDrive\Máy tính\No_name_application\WindowsFormsApp1\setting_time.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //StreamWriter sr = new StreamWriter(F);
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(timeEdit1.Text);
            sw.Close();
           // sr.WriteLine(timeEdit1.Text);
            //sr.Write(timeEdit1.Text + "\n");
            //sr.Close();
            //F.Close();
            this.Close();
        }
    }
}
