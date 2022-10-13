namespace HIT_C2_SPF
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
            if (disposing&&(components!=null))
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
            this.MapDisplay = new System.Windows.Forms.PictureBox();
            this.SelectOrigDest = new System.Windows.Forms.Button();
            this.BeginSPF = new System.Windows.Forms.Button();
            this.ClearScreen = new System.Windows.Forms.Button();
            this.DistanceDisplay = new System.Windows.Forms.TextBox();
            this.StatusDisplay = new System.Windows.Forms.Label();
            this.DistLabel = new System.Windows.Forms.Label();
            this.TimeConsumptionDisplay = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.AboutMe = new System.Windows.Forms.Button();
            this.WalkingTimeEst = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // MapDisplay
            // 
            this.MapDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MapDisplay.Location = new System.Drawing.Point(182, 3);
            this.MapDisplay.Name = "MapDisplay";
            this.MapDisplay.Size = new System.Drawing.Size(683, 950);
            this.MapDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MapDisplay.TabIndex = 0;
            this.MapDisplay.TabStop = false;
            this.MapDisplay.Tag = "";
            this.MapDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MapDisplay_MouseClick);
            // 
            // SelectOrigDest
            // 
            this.SelectOrigDest.Location = new System.Drawing.Point(12, 15);
            this.SelectOrigDest.Name = "SelectOrigDest";
            this.SelectOrigDest.Size = new System.Drawing.Size(156, 41);
            this.SelectOrigDest.TabIndex = 1;
            this.SelectOrigDest.Text = "选择起点-终点";
            this.SelectOrigDest.UseVisualStyleBackColor = true;
            this.SelectOrigDest.Click += new System.EventHandler(this.SelectOrigDest_Click);
            // 
            // BeginSPF
            // 
            this.BeginSPF.Enabled = false;
            this.BeginSPF.Location = new System.Drawing.Point(12, 73);
            this.BeginSPF.Name = "BeginSPF";
            this.BeginSPF.Size = new System.Drawing.Size(156, 72);
            this.BeginSPF.TabIndex = 3;
            this.BeginSPF.Text = "开始寻路";
            this.BeginSPF.UseVisualStyleBackColor = true;
            this.BeginSPF.Click += new System.EventHandler(this.BeginSPF_Click);
            // 
            // ClearScreen
            // 
            this.ClearScreen.Location = new System.Drawing.Point(12, 162);
            this.ClearScreen.Name = "ClearScreen";
            this.ClearScreen.Size = new System.Drawing.Size(156, 38);
            this.ClearScreen.TabIndex = 4;
            this.ClearScreen.Text = "清除";
            this.ClearScreen.UseVisualStyleBackColor = true;
            this.ClearScreen.Click += new System.EventHandler(this.ClearScreen_Click);
            // 
            // DistanceDisplay
            // 
            this.DistanceDisplay.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceDisplay.Location = new System.Drawing.Point(12, 390);
            this.DistanceDisplay.Name = "DistanceDisplay";
            this.DistanceDisplay.ReadOnly = true;
            this.DistanceDisplay.Size = new System.Drawing.Size(156, 42);
            this.DistanceDisplay.TabIndex = 5;
            this.DistanceDisplay.Text = "0.00m";
            this.DistanceDisplay.Visible = false;
            // 
            // StatusDisplay
            // 
            this.StatusDisplay.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusDisplay.Location = new System.Drawing.Point(12, 278);
            this.StatusDisplay.Name = "StatusDisplay";
            this.StatusDisplay.Size = new System.Drawing.Size(156, 56);
            this.StatusDisplay.TabIndex = 6;
            this.StatusDisplay.Text = "aloha~";
            this.StatusDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DistLabel
            // 
            this.DistLabel.AutoSize = true;
            this.DistLabel.Location = new System.Drawing.Point(16, 363);
            this.DistLabel.Name = "DistLabel";
            this.DistLabel.Size = new System.Drawing.Size(112, 15);
            this.DistLabel.TabIndex = 7;
            this.DistLabel.Text = "最短路径长度：";
            this.DistLabel.Visible = false;
            // 
            // TimeConsumptionDisplay
            // 
            this.TimeConsumptionDisplay.AutoSize = true;
            this.TimeConsumptionDisplay.Location = new System.Drawing.Point(16, 504);
            this.TimeConsumptionDisplay.Name = "TimeConsumptionDisplay";
            this.TimeConsumptionDisplay.Size = new System.Drawing.Size(106, 15);
            this.TimeConsumptionDisplay.TabIndex = 8;
            this.TimeConsumptionDisplay.Text = "算法耗时：0us";
            this.TimeConsumptionDisplay.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 237);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(134, 19);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "显示路点与路径";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // AboutMe
            // 
            this.AboutMe.Location = new System.Drawing.Point(12, 895);
            this.AboutMe.Name = "AboutMe";
            this.AboutMe.Size = new System.Drawing.Size(156, 49);
            this.AboutMe.TabIndex = 10;
            this.AboutMe.Text = "About";
            this.AboutMe.UseVisualStyleBackColor = true;
            this.AboutMe.Click += new System.EventHandler(this.AboutMe_Click);
            // 
            // WalkingTimeEst
            // 
            this.WalkingTimeEst.AutoSize = true;
            this.WalkingTimeEst.Location = new System.Drawing.Point(16, 445);
            this.WalkingTimeEst.Name = "WalkingTimeEst";
            this.WalkingTimeEst.Size = new System.Drawing.Size(129, 15);
            this.WalkingTimeEst.TabIndex = 11;
            this.WalkingTimeEst.Text = "走路大约需要0min";
            this.WalkingTimeEst.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 956);
            this.Controls.Add(this.WalkingTimeEst);
            this.Controls.Add(this.AboutMe);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.TimeConsumptionDisplay);
            this.Controls.Add(this.DistLabel);
            this.Controls.Add(this.StatusDisplay);
            this.Controls.Add(this.DistanceDisplay);
            this.Controls.Add(this.ClearScreen);
            this.Controls.Add(this.BeginSPF);
            this.Controls.Add(this.SelectOrigDest);
            this.Controls.Add(this.MapDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shortest Path Finder for HIT Campus 2";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MapDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MapDisplay;
        private System.Windows.Forms.Button SelectOrigDest;
        private System.Windows.Forms.Button BeginSPF;
        private System.Windows.Forms.Button ClearScreen;
        private System.Windows.Forms.TextBox DistanceDisplay;
        private System.Windows.Forms.Label StatusDisplay;
        private System.Windows.Forms.Label DistLabel;
        private System.Windows.Forms.Label TimeConsumptionDisplay;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button AboutMe;
        private System.Windows.Forms.Label WalkingTimeEst;
    }
}

