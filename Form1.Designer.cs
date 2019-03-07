namespace 关机助手补丁
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button开机 = new System.Windows.Forms.Button();
            this.button关机 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button开机
            // 
            this.button开机.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button开机.Location = new System.Drawing.Point(39, 21);
            this.button开机.Name = "button开机";
            this.button开机.Size = new System.Drawing.Size(148, 63);
            this.button开机.TabIndex = 0;
            this.button开机.Text = "开机";
            this.button开机.UseVisualStyleBackColor = true;
            this.button开机.Click += new System.EventHandler(this.button开机_Click);
            // 
            // button关机
            // 
            this.button关机.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button关机.Location = new System.Drawing.Point(235, 21);
            this.button关机.Name = "button关机";
            this.button关机.Size = new System.Drawing.Size(140, 63);
            this.button关机.TabIndex = 1;
            this.button关机.Text = "关机";
            this.button关机.UseVisualStyleBackColor = true;
            this.button关机.Click += new System.EventHandler(this.button关机_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 113);
            this.Controls.Add(this.button关机);
            this.Controls.Add(this.button开机);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "关机助手补丁程序";
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button开机;
        private System.Windows.Forms.Button button关机;
    }
}

