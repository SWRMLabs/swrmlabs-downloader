namespace WizardLayout
{
    partial class LastForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LastForm));
            this.pnlContent = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.download = new System.Windows.Forms.Label();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.download);
            this.pnlContent.Controls.Add(this.button2);
            this.pnlContent.Controls.Add(this.richTextBox2);
            this.pnlContent.Controls.Add(this.progressBar1);
            this.pnlContent.Controls.Add(this.button1);
            this.pnlContent.Controls.Add(this.richTextBox1);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlContent.Size = new System.Drawing.Size(997, 591);
            this.pnlContent.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(358, 421);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(219, 61);
            this.button2.TabIndex = 9;
            this.button2.Text = "View Files";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(160, 266);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(677, 123);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(164, 189);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(545, 45);
            this.progressBar1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(737, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 51);
            this.button1.TabIndex = 6;
            this.button1.Text = "Download";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(164, 74);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(464, 51);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // download
            // 
            this.download.AutoSize = true;
            this.download.Location = new System.Drawing.Point(747, 213);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(0, 21);
            this.download.TabIndex = 10;
            // 
            // LastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 591);
            this.Controls.Add(this.pnlContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LastForm";
            this.Text = "Hive";
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label download;
    }
}