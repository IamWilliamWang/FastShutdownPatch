using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 关机助手补丁
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button开机_Click(object sender, EventArgs e)
        {
            File.AppendAllText("TimeDatabase.cache",
                "INSERT INTO [Table](开机时间) VALUES (" + GETDATE() + ")鋝");
            Application.Exit();
        }

        private void button关机_Click(object sender, EventArgs e)
        {
            String nowTime = GETDATE();
            File.AppendAllText("TimeDatabase.cache",
                "UPDATE [Table] SET 关机时间 = " + nowTime + ", 时长 = "+ nowTime + " - 开机时间 WHERE 序号 in (SELECT MAX(序号) FROM[Table]) 鋝");
            Application.Exit();
        }

        private String GETDATE()
        {
            return "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = "TimeDatabase.cache";
            process.Start();
        }
    }
}
