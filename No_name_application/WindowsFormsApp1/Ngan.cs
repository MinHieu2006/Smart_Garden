using Bunifu.Json.Linq;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Threading;
using System.Drawing.Imaging;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace WindowsFormsApp1
{
    public partial class Ngan : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        long time = 0;
        int vtanh = 0;
        string dc;
        bool tran = false;
        bool ch1ll_bro = true;
        List<double> nhiet_do = new List<double>();
        List<double> do_am = new List<double>();
        List<double> do_dam_dat = new List<double>();
        int gio = 0;
        int nguyen = 0;
        SeriesCollection series = new SeriesCollection();
        private void Ngan_Load(object sender, EventArgs e)
        {
            trangchu();
        }
        string dia_chi, temp2;
        public Ngan()
        {
            InitializeComponent();

        }
        #region chọn tab
        private void tabcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabcontrol.SelectedIndex == 0)
            {
                timer1.Stop();
                trangchu();
            } else if (tabcontrol.SelectedIndex == 1)
            {
                if (tran == false)
                {
                    doc_data("https://api.airtable.com/v0/app2zhSLcaJB3pgcq/caycanh?maxRecords=20&view=Grid%20view", 1);
                    doc_data("https://api.airtable.com/v0/app2zhSLcaJB3pgcq/hoa?maxRecords=20&view=Grid%20view", 2);
                    doc_data("https://api.airtable.com/v0/app2zhSLcaJB3pgcq/rau?maxRecords=20&view=Grid%20view", 3);
                    tran = true;
                }
                timer1.Stop();
                richTextBox1.Text = "";
                richTextBox2.Text = "";
                pictureBox3.Image = imageList1.Images[3];
                label3.Text = "";
            } else if (tabcontrol.SelectedIndex == 2)
            {
                timer1.Stop();
                thoitiet();
                // nhiệt độ
                FileStream F = new FileStream(Application.StartupPath + @"\temp.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter sr = new StreamWriter(F);
                sr.WriteLine(temp);
                sr.Close();
                F.Close();
                //thời tiết
                FileStream A = new FileStream(Application.StartupPath + @"thoitiet.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter ab = new StreamWriter(A);
                ab.WriteLine(temp2);
                ab.Close();
                A.Close();
            } else if (tabcontrol.SelectedIndex == 3)
            {
                timer1.Start();
                timer2.Start();
                ketnoi();
                //xuli_bieu_do();
            } else if (tabcontrol.SelectedIndex == 4)
            {
                timer1.Stop();
            } else if (tabcontrol.SelectedIndex == 5)
            {
                timer1.Stop();
                setting();
            } else if (tabcontrol.SelectedIndex == 6)
            {
               // xuli_bieu_do();
            }
        }
        #endregion
        #region di chuyển đến tab
        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

            TabPage t = tabcontrol.TabPages[0];
            tabcontrol.SelectedTab = t; //go to tab
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {

            TabPage t = tabcontrol.TabPages[1];
            tabcontrol.SelectedTab = t; //go to tab
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {

            TabPage t = tabcontrol.TabPages[2];
            tabcontrol.SelectedTab = t; //go to tab
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {

            TabPage t = tabcontrol.TabPages[3];
            tabcontrol.SelectedTab = t; //go to tab
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {

            //TabPage t = tabcontrol.TabPages[4];
            //tabcontrol.SelectedTab = t; //go to tab

            System.Diagnostics.Process.Start("https://smartgardenvn.blogspot.com/");
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {

            TabPage t = tabcontrol.TabPages[5];
            tabcontrol.SelectedTab = t; //go to tab
        }

        private void accordionControlElement6_Click_1(object sender, EventArgs e)
        {
            TabPage t = tabcontrol.TabPages[6];
            tabcontrol.SelectedTab = t; //go to tab
                                        // bieuto_tab.Show();
        }
        #endregion
        #region xử lí thời tiết
        #region lấy ip => vị trí người dùng
        //Khai báo biến
        string vitri, city, country, doam, tocdogio, temp;
        // http://home.openweathermap.org/users/sign_in -- link đăng ký lấy API
        private const string API_KEY = "c7055ce01673bc05bf8af1cb09e60bd2";
        private const string CurrentUrl = "http://api.openweathermap.org/data/2.5/weather?" + "q@=@LOC@&mode=xml&units=metric&APPID=" + API_KEY;
        private const string ForecastUrl = "http://api.openweathermap.org/data/2.5/forecast?" + "q=@LOC@&mode=xml&units=metric&APPID=" + API_KEY;
        public static string getIpInternet()
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string ip = client.DownloadString("http://ipinfo.io/ip");
                    ip = ip.Replace("\r", "").Replace("\n", "");
                    return ip;
                }
            }
            catch
            {
                return "127.0.0.1";
            }
        }
        #region set địa chỉ
        #endregion
        public static string CityStateCountByIp(string IP)
        {
            string url = "http://api.ipstack.com/" + IP + "?access_key=0779c5957f09d1ee10296a7d5820090e";
            var request = System.Net.WebRequest.Create(url);

            using (WebResponse wrs = request.GetResponse())
            using (Stream stream = wrs.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var obj = JObject.Parse(json);
                string City = (string)obj["city"];
                string Country = (string)obj["region_name"];
                string CountryCode = (string)obj["country_code"];
                return (City);
            }

            return "";

        }
        #endregion
        void thoitiet()
        {
            vitri = CityStateCountByIp(getIpInternet());
            string url = ForecastUrl.Replace("@LOC@", "Danang");
            richTextBox3.Text = "";
            using (WebClient client = new WebClient())
            {

                try
                {
                    DisplayForecast(client.DownloadString(url));
                    if (temp2.Contains("sun"))
                    {
                        thoitiet_img.Image = imageList1.Images[0];
                        richTextBox3.Text += "Dự báo thời tiết sẽ có nắng. \n";
                    }
                    if (temp2.Contains("rain"))
                    {
                        richTextBox3.Text += "Dự báo thời tiết sẽ có mưa. \n";
                        thoitiet_img.Image = imageList1.Images[1];
                    }
                    if (temp2.Contains("clouds"))
                    {
                        thoitiet_img.Image = imageList1.Images[2];
                        richTextBox3.Text += "Dự báo thời tiết sẽ có mây mờ. \n";
                    }
                    if (Convert.ToDouble(temp) <= 30 && Convert.ToDouble(temp) >= 20)
                    {
                        richTextBox3.Text += "Thời tiết thích hợp cho cây trồng";
                    } else richTextBox3.Text += "Thời tiết không thích hợp cho cây trồng";
                    label3.Text = temp + "°C";
                    label4.Text = "Độ ẩm: " + doam + "%";
                    label5.Text = "Đà Nẵng";
                }
                catch (WebException ex)
                {
                    url = ForecastUrl.Replace("@LOC@", "Danang");
                    //DisplayError(ex);
                    DisplayForecast(client.DownloadString(url));
                    if (temp2.Contains("sun")) thoitiet_img.Image = imageList1.Images[0];
                    if (temp2.Contains("rain")) thoitiet_img.Image = imageList1.Images[1];
                    if (temp2.Contains("clouds")) thoitiet_img.Image = imageList1.Images[2];

                    label3.Text = temp + "°C";
                    label4.Text = "Độ ẩm: " + doam + "%";
                    label5.Text = "Đà Nẵng";
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Unknown esadasdrror\n" + ex.Message);

                    url = ForecastUrl.Replace("@LOC@", "Danang");
                    //DisplayError(ex);
                    DisplayForecast(client.DownloadString(url));
                    if (temp2.Contains("sun")) thoitiet_img.Image = imageList1.Images[0];
                    if (temp2.Contains("rain")) thoitiet_img.Image = imageList1.Images[1];
                    if (temp2.Contains("clouds")) thoitiet_img.Image = imageList1.Images[2];

                    label3.Text = temp + "°C";
                    label4.Text = "Độ ẩm: " + doam + "%";
                    label5.Text = "Đà Nẵng";
                }
            }
        }
        void setting()
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            nguyen++;
            if (time == 1)
            {
                ketnoi();
                nguyen++;
                time = 0;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SetResponse set = client.Set(@"test/congtac", "ON");
            label11.Text = "Máy bơm đang bật";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            SetResponse set = client.Set(@"test/congtac", "OFF");
            label11.Text = "Máy bơm đang tắt";
        }

        private void DisplayForecast(string xml)
        {

            XmlDocument xml_doc = new XmlDocument();
            xml_doc.LoadXml(xml);
            XmlNode loc_node = xml_doc.SelectSingleNode("weatherdata/location");
            city = loc_node.SelectSingleNode("name").InnerText;
            country = loc_node.SelectSingleNode("country").InnerText;
            XmlNode geo_node = loc_node.SelectSingleNode("location");
            //txtLat.Text = geo_node.Attributes["latitude"].Value;
            char degrees = (char)176;
            DateTime d1 = DateTime.Now;

            foreach (XmlNode time_node in xml_doc.SelectNodes("//time"))
            {

                DateTime time =
                    DateTime.Parse(time_node.Attributes["from"].Value,
                        null, DateTimeStyles.AssumeUniversal);

                XmlNode temp_node = time_node.SelectSingleNode("temperature");
                temp = temp_node.Attributes["value"].Value;
                // lấy dữ liệu thời tiết vd: nắng mưa , .....
                XmlNode temp_node2 = time_node.SelectSingleNode("clouds");
                temp2 = temp_node2.Attributes["value"].Value;
                // lấy độ ẩm
                XmlNode temp_node3 = time_node.SelectSingleNode("humidity");
                doam = temp_node3.Attributes["value"].Value;
                // lấy tốc độ gió
                XmlNode temp_node4 = time_node.SelectSingleNode("windSpeed");
                tocdogio = temp_node4.Attributes["mps"].Value;
                break;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (vtanh - 1 >= 0)
            {

                Image img = Image.FromFile(Application.StartupPath + @"\hd\hd" + Convert.ToString(vtanh - 1) + ".png");
                //panel1.BackgroundImage = img;

                //pictureBox1.Image = img;
                //trangchu_tab.BackgroundImage = img;
                //              pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                //                panel1.Bac
                //pictureBox1.Image = imageList1.Images[vtanh ];
                vtanh -= 1;
            }
            if (vtanh == 2)
            {
                //teacher1.Visible = true;
                //teacher2.Visible = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            if (vtanh + 1 <= 5)
            {
                Image img = Image.FromFile(Application.StartupPath + @"\hd\hd" + Convert.ToString(vtanh + 1) + ".png");
                //panel1.BackgroundImage = img;
                //pictureBox1.Image = img;
                // trangchu_tab.BackgroundImage = img;

                //pictureBox1.Image = imageList1.Images[vtanh];
                vtanh += 1;
            }
            if (vtanh > 2)
            {
                //teacher1.Visible = false;
                //teacher2.Visible = true;
            }
        }

        private void DisplayError(WebException exception)
        {
            try
            {
                StreamReader reader = new StreamReader(exception.Response.GetResponseStream());
                XmlDocument response_doc = new XmlDocument();
                response_doc.LoadXml(reader.ReadToEnd());
                XmlNode message_node = response_doc.SelectSingleNode("//message");
                MessageBox.Show(message_node.InnerText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error\n" + ex.Message);
            }
        }


        #endregion


        #region kết nối
        IFirebaseConfig config = new FirebaseConfig
        {

            AuthSecret = "KXE4qIZrs9RzlFaOgSpOKHKiZwcKrUi7f6c3JT6Y",
            BasePath = "https://he-thong-quan-li-smat-garden-default-rtdb.firebaseio.com/"
        };



        IFirebaseClient client;
        void ketnoi()
        {

            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                //MessageBox.Show("Kết nối thành công");
            }

            var result = client.Get("test/doam");
            var result1 = client.Get("test/doamdat");
            var result2 = client.Get("test/temperate");
            if (ch1ll_bro)
            {
                var A7 = client.Get("test/bongden");
                var A5 = client.Get("test/congtac");
                
                if (A7.Body == "ON") label12.Text = "Bóng đèn đang bật"; else label12.Text = "Bóng đèn đang tắt";
                if (A5.Body == "ON") label11.Text = "Máy bơm đang bật"; else label11.Text = "Máy bơm đang tắt";
                ch1ll_bro = false;
            }

            doam_lbl.Text = result.Body;
            nhietdo_lbl.Text = result2.Body;
            doamdat_lbl.Text = result1.Body;
            if (nguyen == 0)
            {
                series.Add(new LineSeries { Values = new ChartValues<ObservablePoint> { new ObservablePoint(Convert.ToDouble(nguyen), Convert.ToDouble(result2.Body) )} });
            }
            else if(nguyen%10==0)
            {
                series[series.Count - 1].Values.Add(new ObservablePoint(Convert.ToDouble(nguyen), Convert.ToDouble(result2.Body)));
            }
            if (nguyen > 100) series.Clear();
            if (Convert.ToInt32(result2.Body) <= 85)
            {
                SetResponse set = client.Set(@"test/maybom", "ON");
            }
            else {
                SetResponse set = client.Set(@"test/maybom", "OFF");
            } 
            MyChart.Series = series;
            //dulieu std = result.ResultAs<dulieu>();
            // MessageBox.Show(std.doam);
        }
        #endregion
        #region trang chủ
        void trangchu()
        {
            //            teacher1.Visible = true;
            //            teacher2.Visible = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SetResponse set = client.Set(@"test/bongden", "ON");
            label12.Text = "Bóng đèn đang bật";
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            SetResponse set = client.Set(@"test/bongden", "OFF");
            label12.Text = "Bóng đèn đang tắt";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            nguyen++;
            //if(gio)7
        }

        private void thêmCâyToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // chon hi = new chon();
          //  hi.ShowDialog();
        }

        #endregion

        #region đọc cây trồng
        void doc_data(string hieu, int ngan)
        {
            var url = hieu;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Headers["Authorization"] = "Bearer keySblEbpUkAz7h9u";


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(httpResponse.StatusCode);
                if (ngan == 1) richtemp.Text = result.ToString(); else if (ngan == 2) richtemp2.Text = result.ToString(); else richtemp3.Text = result.ToString();
            }
            int temp;
            if (ngan == 1) temp = richtemp.Find("Artist"); else if (ngan == 2) temp = richtemp2.Find("Artist"); else temp = richtemp3.Find("Artist");
            while (temp != -1)
            {
                int i = temp + 9;
                string t = "";
                if (ngan == 1)
                {
                    while (richtemp.Text[i] != '"' && richtemp.Text[i] != ',')
                    {
                        t = t + richtemp.Text[i];
                        i++;
                    }
                    if (t != "") caycanh.DropDownItems.Add(t);
                    temp = richtemp.Find("Artist", temp + 1, richtemp.Text.Length, RichTextBoxFinds.None);
                } else if (ngan == 2)
                {
                    while (richtemp2.Text[i] != '"' && richtemp2.Text[i] != ',')
                    {
                        t = t + richtemp2.Text[i];
                        i++;
                    }
                    if (t != "") cayhoa.DropDownItems.Add(t);
                    temp = richtemp2.Find("Artist", temp + 1, richtemp2.Text.Length, RichTextBoxFinds.None);
                } else
                {
                    while (richtemp3.Text[i] != '"' && richtemp3.Text[i] != ',')
                    {
                        t = t + richtemp3.Text[i];
                        i++;
                    }
                    if (t != "") rau.DropDownItems.Add(t);
                    temp = richtemp3.Find("Artist", temp + 1, richtemp3.Text.Length, RichTextBoxFinds.None);
                }
            }
        }

        private void caycanh_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < caycanh.DropDownItems.Count; i++)
            {
                if (caycanh.DropDownItems[i].Text == e.ClickedItem.Text)
                {
                    label1.Text = e.ClickedItem.Text;
                    read("N là cute nhất :>>", 1, i + 1);
                }
            }
        }
        private void cayhoa_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < cayhoa.DropDownItems.Count; i++)
            {
                if (cayhoa.DropDownItems[i].Text == e.ClickedItem.Text)
                {
                    label1.Text = e.ClickedItem.Text;
                    read("N là cute nhất :>>", 2, i + 1);
                }
            }
        }

        private void rau_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < rau.DropDownItems.Count; i++)
            {
                if (rau.DropDownItems[i].Text == e.ClickedItem.Text)
                {
                    label1.Text = e.ClickedItem.Text;
                    read("N là cute nhất :>>", 3, i + 1);
                }
            }
        }
        #region đọc thông tin kiến thức
        void read(string hieu, int ngan_cute, int u)
        {
            int sl = 0;
            int temp;
            if (ngan_cute == 1) temp = richtemp.Find("Data"); else if (ngan_cute == 2) temp = richtemp2.Find("Data"); else temp = richtemp3.Find("Data");
            // đọc data 
            while (temp != -1)
            {
                //int i = temp + 9;
                sl++;
                string t = "";
                int ngan;
                if (ngan_cute == 1) ngan = richtemp.Find("Artist", temp + 1, richtemp.Text.Length, RichTextBoxFinds.None); else if (ngan_cute == 2) ngan = richtemp2.Find("Artist", temp + 1, richtemp2.Text.Length, RichTextBoxFinds.None); else ngan = richtemp3.Find("Date", temp + 1, richtemp3.Text.Length, RichTextBoxFinds.None);
                for (int i = temp + 7; i <= ngan - 4; i++)
                {
                    if (ngan_cute == 1)
                    {
                        if ((int)(richtemp.Text[i]) == 92)
                        {
                            t = t + "\n";
                            i++;
                        }
                        else t = t + richtemp.Text[i];
                    } else if (ngan_cute == 2)
                    {
                        if ((int)(richtemp2.Text[i]) == 92)
                        {
                            t = t + "\n";
                            i++;
                        }
                        else t = t + richtemp2.Text[i];
                    }
                    else
                    {
                        if ((int)(richtemp3.Text[i]) == 92)
                        {
                            t = t + "\n";
                            i++;
                        }
                        else t = t + richtemp3.Text[i];
                    }
                }
                if (sl == u)
                {
                    richTextBox1.Text = t;
                    break;
                }
                if (ngan_cute == 1) temp = richtemp.Find("Data", temp + 1, richtemp.Text.Length, RichTextBoxFinds.None); else if (ngan_cute == 2) temp = richtemp2.Find("Data", temp + 1, richtemp2.Text.Length, RichTextBoxFinds.None); else temp = richtemp3.Find("Data", temp + 1, richtemp3.Text.Length, RichTextBoxFinds.None);
            }
            // đọc thongtin
            if (ngan_cute == 1) temp = richtemp.Find("Thongtin"); else if (ngan_cute == 2) temp = richtemp2.Find("Thongtin"); else temp = richtemp3.Find("Thongtin");
            sl = 0;
            while (temp != -1)
            {
                //int i = temp + 9;
                sl++;
                string t = "";
                int ngan;
                if (ngan_cute == 1) ngan = richtemp.Find("},", temp + 1, richtemp.Text.Length, RichTextBoxFinds.None); else if (ngan_cute == 2) ngan = richtemp2.Find("video", temp + 1, richtemp2.Text.Length, RichTextBoxFinds.None); else ngan = richtemp3.Find("Data", temp + 1, richtemp3.Text.Length, RichTextBoxFinds.None);
                for (int i = temp + 11; i <= ngan - 4; i++)
                {
                    if (ngan_cute == 1)
                    {
                        if ((int)(richtemp.Text[i]) == 92)
                        {
                            t = t + "\n";
                            i++;
                        }
                        else t = t + richtemp.Text[i];
                    } else if (ngan_cute == 2)
                    {
                        if ((int)(richtemp2.Text[i]) == 92)
                        {
                            t = t + "\n";
                            i++;
                        }
                        else t = t + richtemp2.Text[i];
                    } else
                    {
                        if ((int)(richtemp3.Text[i]) == 92)
                        {
                            t = t + "\n";
                            i++;
                        }
                        else t = t + richtemp3.Text[i];
                    }
                }
                if (sl == u)
                {
                    richTextBox2.Text = t;
                    break;
                }
                if (ngan_cute == 1) temp = richtemp.Find("Thongtin", temp + 1, richtemp.Text.Length, RichTextBoxFinds.None); else if (ngan_cute == 2) temp = richtemp2.Find("Thongtin", temp + 1, richtemp2.Text.Length, RichTextBoxFinds.None); else temp = richtemp3.Find("Thongtin", temp + 1, richtemp3.Text.Length, RichTextBoxFinds.None);
            }
            // đọc url ảnh
            if (ngan_cute == 1) temp = richtemp.Find("Image"); else if (ngan_cute == 2) temp = richtemp2.Find("Image"); else temp = richtemp3.Find("Image");
            sl = 0;
            while (temp != -1)
            {
                //int i = temp + 9;
                sl++;
                string t = "";
                int ngan;
                if (ngan_cute == 1) ngan = richtemp.Find("Thongtin", temp + 1, richtemp.Text.Length, RichTextBoxFinds.None); else if (ngan_cute == 2) ngan = richtemp2.Find("},", temp + 1, richtemp2.Text.Length, RichTextBoxFinds.None); else ngan = richtemp3.Find("createdTime", temp + 1, richtemp3.Text.Length, RichTextBoxFinds.None);
                for (int i = temp + 8; i <= ngan - 4; i++)
                {
                    if (ngan_cute == 1)
                    {
                        t = t + richtemp.Text[i];
                    } else if (ngan_cute == 2)
                    {
                        t = t + richtemp2.Text[i];
                    }
                    else
                    {
                        t = t + richtemp3.Text[i];
                    }
                }
                if (ngan_cute == 2) {
                    t = t + "pg";
                }
                if (ngan_cute == 3)
                {
                    t = t.Remove(t.Length - 1, 1);
                }
                if (sl == u)
                {
                    dc = t;
                    break;
                }
                if (ngan_cute == 1) temp = richtemp.Find("Image", temp + 1, richtemp.Text.Length, RichTextBoxFinds.None); else if (ngan_cute == 2) temp = richtemp2.Find("Image", temp + 1, richtemp2.Text.Length, RichTextBoxFinds.None); else temp = richtemp3.Find("Image", temp + 1, richtemp3.Text.Length, RichTextBoxFinds.None);
            }
            string crush;
            if (ngan_cute == 1) crush = "caycanh"; else if (ngan_cute == 2) crush = "cayhoa"; else crush = "hoa";
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(dc);
                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        // If you want it as Jpeg
                        if (!Directory.Exists(Application.StartupPath + crush + u.ToString() + " .jpg"))
                        {
                            yourImage.Save(Application.StartupPath + crush + u.ToString() + " .jpg", ImageFormat.Jpeg);
                        }

                    }
                }
            }
            Image img = Image.FromFile(Application.StartupPath + crush + u.ToString() + " .jpg");
            pictureBox3.Image = img;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        #endregion
        #endregion
        #region Xử lí biểu đồ

    void xuli_bieu_do()
        {
            double[] k = { 0, 1, 2, 3, 4 };
            //vector
            //for (int i = 0; i < k.Length; i++) series.Add(new LineSeries() { Title = (i+10).ToString(), Values = new ChartValues<int>(k) });
            //foreach(var i in k)
            //{
            //    List<int> hieu = new List<int>();
            //    hieu.Add(i + 10);
            //    series.Add(new LineSeries() { Title = i.ToString(), Values = new ChartValues<int>(hieu) , ChartPoints = 1 });
            //    series.Add(new ChartPoint() { ChartView = ""});
            //}

            
            

            //series.Add(new LineSeries { Values = new ChartValues<ObservablePoint> { new ObservablePoint(3, 30) } , PointGeometrySize = 15 , DataContext = this});
            //series.Name

        }
    }
    //public SeriesCollection SeriesCollection { get; set; }
}
    
        #endregion



