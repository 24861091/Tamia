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
            this.ToText = new System.Windows.Forms.TextBox();
            this.TopNumText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.FromText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ChildrenNumText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MutationMinText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MutationMaxText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MutationRateText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.GenerationFactorText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "To";
            // 
            // ToText
            // 
            this.ToText.Location = new System.Drawing.Point(135, 96);
            this.ToText.Name = "ToText";
            this.ToText.Size = new System.Drawing.Size(100, 21);
            this.ToText.TabIndex = 1;
            // 
            // TopNumText
            // 
            this.TopNumText.Location = new System.Drawing.Point(135, 142);
            this.TopNumText.Name = "TopNumText";
            this.TopNumText.Size = new System.Drawing.Size(100, 21);
            this.TopNumText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "TopNum";
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
            this.DisplayLabel.Size = new System.Drawing.Size(0, 20);
            this.DisplayLabel.TabIndex = 5;
            // 
            // FromText
            // 
            this.FromText.Location = new System.Drawing.Point(135, 50);
            this.FromText.Name = "FromText";
            this.FromText.Size = new System.Drawing.Size(100, 21);
            this.FromText.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "From";
            // 
            // ChildrenNumText
            // 
            this.ChildrenNumText.Location = new System.Drawing.Point(135, 186);
            this.ChildrenNumText.Name = "ChildrenNumText";
            this.ChildrenNumText.Size = new System.Drawing.Size(100, 21);
            this.ChildrenNumText.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "ChildrenNum";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 288);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "MutationMin";
            // 
            // MutationMinText
            // 
            this.MutationMinText.Location = new System.Drawing.Point(135, 285);
            this.MutationMinText.Name = "MutationMinText";
            this.MutationMinText.Size = new System.Drawing.Size(100, 21);
            this.MutationMinText.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 334);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "MutationMax";
            // 
            // MutationMaxText
            // 
            this.MutationMaxText.Location = new System.Drawing.Point(135, 331);
            this.MutationMaxText.Name = "MutationMaxText";
            this.MutationMaxText.Size = new System.Drawing.Size(100, 21);
            this.MutationMaxText.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "MutationRate";
            // 
            // MutationRateText
            // 
            this.MutationRateText.Location = new System.Drawing.Point(135, 239);
            this.MutationRateText.Name = "MutationRateText";
            this.MutationRateText.Size = new System.Drawing.Size(100, 21);
            this.MutationRateText.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 378);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "GenerationFactor";
            // 
            // GenerationFactorText
            // 
            this.GenerationFactorText.Location = new System.Drawing.Point(135, 375);
            this.GenerationFactorText.Name = "GenerationFactorText";
            this.GenerationFactorText.Size = new System.Drawing.Size(100, 21);
            this.GenerationFactorText.TabIndex = 9;
            // 
            // EvolvForm
            // 
            this.ClientSize = new System.Drawing.Size(880, 461);
            this.Controls.Add(this.GenerationFactorText);
            this.Controls.Add(this.ChildrenNumText);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MutationRateText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FromText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.MutationMaxText);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TopNumText);
            this.Controls.Add(this.MutationMinText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ToText);
            this.Controls.Add(this.label1);
            this.Name = "EvolvForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EvolvForm_FormClosing);
            this.Load += new System.EventHandler(this.EvolvForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ToText;
        private System.Windows.Forms.TextBox TopNumText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.TextBox FromText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ChildrenNumText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MutationMinText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MutationMaxText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox MutationRateText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox GenerationFactorText;
    }
}
