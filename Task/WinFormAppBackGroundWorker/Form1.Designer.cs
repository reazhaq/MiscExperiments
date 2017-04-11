namespace WinFormAppBackGroundWorker
{
    partial class Form1
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
            this.StartAsyncButton = new System.Windows.Forms.Button();
            this.CancelAsyncButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // StartAsyncButton
            // 
            this.StartAsyncButton.Location = new System.Drawing.Point(12, 32);
            this.StartAsyncButton.Name = "StartAsyncButton";
            this.StartAsyncButton.Size = new System.Drawing.Size(75, 23);
            this.StartAsyncButton.TabIndex = 2;
            this.StartAsyncButton.Text = "Start";
            this.StartAsyncButton.UseVisualStyleBackColor = true;
            this.StartAsyncButton.Click += new System.EventHandler(this.StartAsyncButton_Click);
            // 
            // CancelAsyncButton
            // 
            this.CancelAsyncButton.Location = new System.Drawing.Point(12, 61);
            this.CancelAsyncButton.Name = "CancelAsyncButton";
            this.CancelAsyncButton.Size = new System.Drawing.Size(75, 23);
            this.CancelAsyncButton.TabIndex = 3;
            this.CancelAsyncButton.Text = "Cancel";
            this.CancelAsyncButton.UseVisualStyleBackColor = true;
            this.CancelAsyncButton.Click += new System.EventHandler(this.CancelAsyncButton_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(12, 16);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(10, 13);
            this.resultLabel.TabIndex = 4;
            this.resultLabel.Text = "-";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.CancelAsyncButton);
            this.Controls.Add(this.StartAsyncButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartAsyncButton;
        private System.Windows.Forms.Button CancelAsyncButton;
        private System.Windows.Forms.Label resultLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

