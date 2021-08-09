using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public const string LOCK = ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}";
        public bool w = false;
        public Form1()
        {
            InitializeComponent();
            this.button1.Text = "点击选取文件夹\n（或将文件夹拖拽至此）";
        }
        //事件处理方法
        void frm_TransfEvent1(Boolean value)
        {
            w = value;

        }
        //事件处理方法
        void frm_TransfEvent(Boolean value)
        {
            w = value;
        }
        private void SetPwd(string fname)
        {
            Form2 fm2 = new Form2(fname);
            //弹出的窗口居中
            fm2.Top = this.Top + (this.Height - fm2.Height) / 2;
            fm2.Left = this.Left + (this.Width - fm2.Width) / 2;
            
            fm2.TransfEvent1 += frm_TransfEvent1;
            fm2.ShowDialog();
        
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //status = lockType;//

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo d = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                string selectedpath = d.Parent.FullName + d.Name;
                if (folderBrowserDialog1.SelectedPath.LastIndexOf(".{") == -1)//通过文件夹名称，判断加密
                {
                    SetPwd(d.Name);
                    if (w == true)
                    {
                        try
                        {
                            if (!d.Root.Equals(d.Parent.FullName))
                            {
                                d.MoveTo(d.Parent.FullName + "\\" + d.Name + LOCK);//文件夹重命名
                            }
                            else d.MoveTo(d.Parent.FullName + d.Name + LOCK);
                            MessageBox.Show("成功上锁");
                        }
                        catch (IOException ee)
                        {
                            MessageBox.Show("文件夹被占用，请先关闭该文件夹才能上锁");
                        }
                    }
                    
                    //txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
                    
                }
                else//解密文件夹
                {
                    string fname=d.Name;
                    Form3 frm = new Form3(fname);
                    //弹出的窗口居中
                    frm.Top = this.Top + (this.Height - frm.Height) / 2;
                    frm.Left = this.Left + (this.Width - frm.Width) / 2;
                    //注册事件
                    frm.TransfEvent += frm_TransfEvent;
                    frm.ShowDialog();
                    if (w == true)
                    {
                        File.Delete(folderBrowserDialog1.SelectedPath + "\\key.xml");
                        string path = folderBrowserDialog1.SelectedPath.Substring(0, folderBrowserDialog1.SelectedPath.LastIndexOf("."));
                        d.MoveTo(path);
                        MessageBox.Show("解锁成功！");
                        w = false;
                    }
                    else { MessageBox.Show("解锁失败"); }
                    
                    //status = GetStatus(status);
                    //bool s = CheckPwd();
                    //if (s)
                    //{
                    //  File.Delete(folderBrowserDialog1.SelectedPath + "\\key.xml");
                    //  string path = folderBrowserDialog1.SelectedPath.Substring(0, folderBrowserDialog1.SelectedPath.LastIndexOf("."));
                    //  d.MoveTo(path);
                    //  txtFolderPath.Text = path;

                }
            }
        }



        private void button1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (path.EndsWith(LOCK))//解密文件夹
            {
                DirectoryInfo d = new DirectoryInfo(path);
                string selectedpath = d.Parent.FullName + d.Name;
                string fname = d.Name;
                Form3 frm = new Form3(fname);
                //弹出的窗口居中
                frm.Top = this.Top + (this.Height - frm.Height) / 2;
                frm.Left = this.Left + (this.Width - frm.Width) / 2;
                //注册事件
                frm.TransfEvent += frm_TransfEvent;
                frm.ShowDialog();
                if (w == true)
                {
                    File.Delete(path + "\\key.xml");
                    string path1 = path.Substring(0, path.LastIndexOf("."));
                    d.MoveTo(path1);
                    MessageBox.Show("解锁成功！");
                    w = false;
                }
                else { MessageBox.Show("解锁失败"); }
            }
            if (System.IO.Directory.Exists(path))//加密文件夹
            {
                DirectoryInfo d = new DirectoryInfo(path);
                string selectedpath = d.Parent.FullName + d.Name;
                SetPwd(d.Name);
                if (w == true)
                {                    
                    try
                    {
                        if (!d.Root.Equals(d.Parent.FullName))
                        {
                            d.MoveTo(d.Parent.FullName + "\\" + d.Name + LOCK);//文件夹重命名
                        }
                        else d.MoveTo(d.Parent.FullName + d.Name + LOCK);
                        MessageBox.Show("成功上锁");
                    }
                    catch (IOException ee)
                    {
                        MessageBox.Show("文件夹被占用，请先关闭该文件夹才能上锁");
                    }
                    w = false;
                }
            }

        }

        private void button1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))      //判断该文件是否可以转换到文件放置格式
            {
                e.Effect = DragDropEffects.Link;       //放置效果为链接放置
            }
            else
            {
                e.Effect = DragDropEffects.None;      //不接受该数据,无法放置，后续事件也无法触发
            }

        }
    }
}

