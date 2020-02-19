namespace 五子棋
{
    partial class EvolvForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.GenerationText = new System.Windows.Forms.TextBox();
            this.EvolvFactorText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.StartText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AmountText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "迭代次数";
            // 
            // GenerationText
            // 
            this.GenerationText.Location = new System.Drawing.Point(139, 96);
            this.GenerationText.Name = "GenerationText";
            this.GenerationText.Size = new System.Drawing.Size(100, 21);
            this.GenerationText.TabIndex = 1;
            // 
            // EvolvFactorText
            // 
            this.EvolvFactorText.Location = new System.Drawing.Point(139, 142);
            this.EvolvFactorText.Name = "EvolvFactorText";
            this.EvolvFactorText.Size = new System.Drawing.Size(100, 21);
            this.EvolvFactorText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "变异系数";
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Stencil", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(334, 50);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 67);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.AutoSize = true;
            this.DisplayLabel.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.Red;
            this.DisplayLabel.Location = new System.Drawing.Point(60, 254);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(259, 20);
            this.DisplayLabel.TabIndex = 5;
            this.DisplayLabel.Text = "1111111111111111111111111";
            // 
            // StartText
            // 
            this.StartText.Location = new System.Drawing.Point(139, 50);
            this.StartText.Name = "StartText";
            this.StartText.Size = new System.Drawing.Size(100, 21);
            this.StartText.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "迭代起始";
            // 
            // AmountText
            // 
            this.AmountText.Location = new System.Drawing.Point(139, 186);
            this.AmountText.Name = "AmountText";
            this.AmountText.Size = new System.Drawing.Size(100, 21);
            this.AmountText.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "种群规模";
            // 
            // EvolvForm
            // 
            this.ClientSize = new System.Drawing.Size(880, 461);
            this.Controls.Add(this.AmountText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.StartText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.EvolvFactorText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GenerationText);
            this.Controls.Add(this.label1);
            this.Name = "EvolvForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox GenerationText;
        private System.Windows.Forms.TextBox EvolvFactorText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.TextBox StartText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AmountText;
        private System.Windows.Forms.Label label4;
    }
}
