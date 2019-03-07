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

        private void textBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBox源_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string filename = files[0];
            filename = filename.Substring(0, filename.LastIndexOf('\\')+1) + "TimeDatabase.cache";
            this.textBox源.Text = filename;
        }

        private void textBox目标_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string filename = files[0];
            filename = filename.Substring(0, filename.LastIndexOf('\\') + 1) + "TimeDatabase.cache";
            this.textBox目标.Text = filename;
        }

        private void button合并_Click(object sender, EventArgs e)
        {
            String 源内容 = File.ReadAllText(this.textBox源.Text);
            String 目标内容 = File.ReadAllText(this.textBox目标.Text);
            int 插入index = 目标内容.LastIndexOf('鋝', 目标内容.Length - 2) + 1;
            StringBuilder stringBuilder = new StringBuilder(目标内容);
            stringBuilder.Insert(插入index, 源内容);
            File.Delete(this.textBox目标.Text);
            File.WriteAllText(this.textBox目标.Text, stringBuilder.ToString());
            File.SetAttributes(this.textBox目标.Text, FileAttributes.Hidden);
            File.Delete(this.textBox源.Text);
            MessageBox.Show("成功！");
        }
    }
}
