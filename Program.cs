using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Text;


namespace WindowsFormsApplication3
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    class Data
    {
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        public pwd GetpwdList()
        {
            //读取json数据
            string jsonStr = File.ReadAllText(@"..\..\pwd.json");
            //反序列化
            pwd listHost = js.Deserialize<pwd>(jsonStr);

            return listHost;
        }

        public void Addpwd(pwd list)
        {
            //向数据中覆盖追加
            string strJson = js.Serialize(list);
            try
            {
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                File.WriteAllText(@"..\..\pwd.json", strJson, encode);
                return;
            }
            catch
            {

                return;
            }
        }
        public List<pwd> zGetpwdList()
        {
            string path = @"..\..\pwd.json";
            List<pwd> list = new List<pwd>();
            if (!File.Exists(path))
            {
                
                //string newpath = @"..\..\";
                //string fileNameOne = "pwd.json";
                //string filePathOne = System.IO.Path.Combine(newpath, fileNameOne);
                //File.Create(filePathOne);
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Close();
                
                
            }
            else
            {//读取json数据
                string jsonStr = File.ReadAllText(@"..\..\pwd.json");
                list = js.Deserialize<List<pwd>>(jsonStr);
            }
            

            
            return list;
        }

        public bool zAddpwd(List<pwd> list)
        {
            //向数据中覆盖追加
            string strJson = js.Serialize(list);
            try
            {
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                File.WriteAllText(@"..\..\pwd.json", strJson, encode);
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
