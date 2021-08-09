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
    public delegate void TransfDelegate1(Boolean value);
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string fname)
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
        public event TransfDelegate1 TransfEvent1;
        bool pwdType = false;
        //创建数据对象
        Data getHost = new WindowsFormsApplication3.Data();
        List<pwd> dd = new List<pwd>();
        private string fname;
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == this.textBox2.Text && this.textBox1.Text != "")
            {
                
                int a = 0;
                try { dd = getHost.zGetpwdList(); foreach (pwd dpd in dd) { if (dpd.ppName == fname) { dpd.pp = this.textBox1.Text; a += 1; } } }
                catch (Exception ex) { a = 0; }
                
                //实例化一个数据对象
                if (a == 0)
                {
                    pwd p = new pwd() { ppName = fname, pp = this.textBox1.Text };
                    if (dd == null) { List<pwd> ww = new List<pwd>(); dd = ww; }
                    dd.Add(p);
                }                  
                bool zad=getHost.zAddpwd(dd);
                if (zad == true) { pwdType = true; } else { pwdType = false; }              
                TransfEvent1(pwdType);
               
                this.Close();
            }
            else {
                pwdType = false;
                TransfEvent1(pwdType);
                MessageBox.Show("前后密码不一致或为空");
            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pwdType = false;
            TransfEvent1(pwdType);
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pwdType = true;
            TransfEvent1(pwdType);
            //MessageBox.Show("快捷上锁");
            this.Close();
        }
    }
}
