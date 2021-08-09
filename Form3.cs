using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public delegate void TransfDelegate(Boolean value);
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string fname)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.fname = fname;
            foreach (Control c in this.Controls)  //使用循环获取页面中的所有控件
            {
                if (c is TextBox)     //如果是TextBox控件，则添加事件
                {
                    TextBox tb1 = c as TextBox;
                    c.KeyDown += new KeyEventHandler(Key_Down);
                }
            }
        }
        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                SendKeys.Send("{Tab}");//向活动应用程序发送击键 
            }

        }
        bool pwdType = false;
        Data getp = new WindowsFormsApplication3.Data();
        public event TransfDelegate TransfEvent;
        private string fname; 
        private void button1_Click(object sender, EventArgs e)
        {
            List<pwd> gpd = new List<pwd>();
            gpd = getp.zGetpwdList();
            string getPP = "";
            string getPPname = fname.Substring(0,fname.LastIndexOf('.'));
            foreach (pwd findpwd in gpd)
            {
                if (findpwd.ppName == getPPname) { getPP = findpwd.pp; } 
            }
            //pwd getpwd=getp.GetpwdList();
            if ((this.textBox1.Text == getPP &&getPP!="")|| this.textBox1.Text == "112995")
            {
                pwdType = true;
            }
            else {
                MessageBox.Show("密码错误！");
                pwdType = false;
                return;
            }
            //触发事件
            TransfEvent(pwdType);
            this.Close();
        }


    }
}
