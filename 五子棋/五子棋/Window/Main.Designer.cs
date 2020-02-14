namespace 五子棋
{
    partial class Main
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
            this.StartButton = new System.Windows.Forms.Button();
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.BlackPlayersBox = new System.Windows.Forms.ComboBox();
            this.WhitePlayersBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(47, 68);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "开始";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.OnClickStartButton);
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.AutoSize = true;
            this.DisplayLabel.Font = new System.Drawing.Font("楷体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.Red;
            this.DisplayLabel.Location = new System.Drawing.Point(45, 104);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(163, 29);
            this.DisplayLabel.TabIndex = 1;
            this.DisplayLabel.Text = "五子棋准备";
            // 
            // BlackPlayersBox
            // 
            this.BlackPlayersBox.FormattingEnabled = true;
            this.BlackPlayersBox.Items.AddRange(new object[] {
            "one",
            "not",
            "bu"});
            this.BlackPlayersBox.Location = new System.Drawing.Point(82, 172);
            this.BlackPlayersBox.Name = "BlackPlayersBox";
            this.BlackPlayersBox.Size = new System.Drawing.Size(121, 20);
            this.BlackPlayersBox.TabIndex = 2;
            // 
            // WhitePlayersBox
            // 
            this.WhitePlayersBox.FormattingEnabled = true;
            this.WhitePlayersBox.Items.AddRange(new object[] {
            "one",
            "not",
            "bu"});
            this.WhitePlayersBox.Location = new System.Drawing.Point(82, 198);
            this.WhitePlayersBox.Name = "WhitePlayersBox";
            this.WhitePlayersBox.Size = new System.Drawing.Size(121, 20);
            this.WhitePlayersBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "黑棋";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "白棋";
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(47, 294);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 23);
            this.TestButton.TabIndex = 6;
            this.TestButton.Text = "测试";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Visible = false;
            this.TestButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 715);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WhitePlayersBox);
            this.Controls.Add(this.BlackPlayersBox);
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.StartButton);
            this.Name = "Main";
            this.Text = "Main";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.ComboBox BlackPlayersBox;
        private System.Windows.Forms.ComboBox WhitePlayersBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button TestButton;
    }
}

