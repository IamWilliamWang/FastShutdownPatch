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
using 关机助手.Util;

namespace 关机助手补丁
{
    public partial class PatchForm : Form
    {
        private String Cache { get { return CacheUtil.CacheFilename; } }
        public PatchForm()
        {
            InitializeComponent();
        }

        private void PatchForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Cache))
            {
                this.Text += " (无缓存)";
                return;
            }
            string[] lines = CacheUtil.GetAllLines(Cache);
            if (lines.Length == 0)
            {
                this.Text += " (空缓存)";
                return;
            }
            string lastLine = lines[lines.Length - 1];
            if (lastLine.StartsWith("INSERT", true, System.Globalization.CultureInfo.CurrentCulture))
                this.Text += " (需关机)";
            else if (lastLine.StartsWith("UPDATE", true, System.Globalization.CultureInfo.CurrentCulture))
                this.Text += " (需开机)";
            else
                MessageBox.Show("请手动检查缓存文件格式", "读取格式异常！", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button开机_Click(object sender, EventArgs e)
        {
            CacheUtil.AppendCache("INSERT INTO [Table](开机时间) VALUES (GETDATE())"
                .Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"), false);
            Application.Exit();
        }

        private void button关机_Click(object sender, EventArgs e)
        {
            CacheUtil.AppendCache("UPDATE [Table] SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间 WHERE 序号 in (SELECT MAX(序号) FROM[Table]) "
                .Replace("GETDATE()", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"), false);
            Application.Exit();
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            if (!File.Exists(Cache))
            {
                MessageBox.Show("缓存文件不存在！", "无法打开缓存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Process process = new Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = Cache;
            process.Start();
            process.WaitForExit();
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
            string dragFilename = files[0];
            dragFilename = dragFilename.Substring(0, dragFilename.LastIndexOf('\\') + 1) + Cache;
            this.textBox源.Text = dragFilename;
        }

        private void textBox目标_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return;
            string dragFilename = files[0];
            dragFilename = dragFilename.Substring(0, dragFilename.LastIndexOf('\\') + 1) + Cache;
            this.textBox目标.Text = dragFilename;
        }

        private void button合并_Click(object sender, EventArgs e)
        {
            String 源内容 = null;
            String 目标内容 = null;
            try
            {
                源内容 = File.ReadAllText(this.textBox源.Text);
                目标内容 = File.ReadAllText(this.textBox目标.Text);
            }
            catch
            {
                if (File.Exists(this.textBox目标.Text) == false
                    && File.Exists(this.textBox源.Text))
                    目标内容 = "";
                else
                {
                    MessageBox.Show("源文件名有误，无法进行合并");
                    return;
                }
            }
            int 插入index = 目标内容.LastIndexOf(CacheUtil.CacheSpliter, 目标内容.Length - 2) + 1;
            StringBuilder stringBuilder = new StringBuilder(目标内容);
            stringBuilder.Insert(插入index, 源内容);
            using (FileStream file = new FileStream(this.textBox目标.Text, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(file))
                    writer.Write(stringBuilder);
            File.SetAttributes(this.textBox目标.Text, FileAttributes.Hidden);
            File.Delete(this.textBox源.Text);
            MessageBox.Show("成功！");
        }

        private void 删除文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除文件操作不可恢复，是否继续？", "删除警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            File.Delete(Cache);
            MessageBox.Show("已删除缓存文件！");
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Cache) == false)
            {
                MessageBox.Show("不存在缓存文件，另存为失败");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                DefaultExt = ".cache",
                FileName = Cache,
                Filter = "缓存文件|" + Cache,
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "另存为",
                CheckFileExists = false
            };
            fileDialog.ShowDialog();
            if (fileDialog.FileName == Cache)
                return;
            using (StreamWriter writer = new StreamWriter(fileDialog.FileName, false))
                using (StreamReader reader = new StreamReader(Cache))
                    writer.Write(reader.ReadToEnd());
        }

        private void 移动文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Cache) == false)
            {
                MessageBox.Show("不存在缓存文件，移动文件失败");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                DefaultExt = ".cache",
                FileName = Cache,
                Filter = "缓存文件|TimeDatabase.cache",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "移动文件",
                CheckFileExists = false
            };
            fileDialog.ShowDialog();
            if (fileDialog.FileName == Cache)
                return;
            File.Delete(fileDialog.FileName);
            File.Move(Cache, fileDialog.FileName);
        }

        private void FormMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                this.Size = new Size(436, 201);
            else if (e.Delta > 0)
                this.Size = new Size(436, 142);
        }
    }
}
