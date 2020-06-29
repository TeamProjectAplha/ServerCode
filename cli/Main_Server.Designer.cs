namespace OpenSSL.CLI
{
    partial class Main_Server
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
            this.label1 = new System.Windows.Forms.Label();
            this.KMS_Start = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Forum_start = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.KMSWorker = new System.ComponentModel.BackgroundWorker();
            this.ForumWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(147, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "SilentPigeon Server";
            // 
            // KMS_Start
            // 
            this.KMS_Start.Location = new System.Drawing.Point(406, 22);
            this.KMS_Start.Name = "KMS_Start";
            this.KMS_Start.Size = new System.Drawing.Size(152, 60);
            this.KMS_Start.TabIndex = 2;
            this.KMS_Start.Text = "Start";
            this.KMS_Start.UseVisualStyleBackColor = true;
            this.KMS_Start.Click += new System.EventHandler(this.KMS_Start_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Key Distribution Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Forum Server";
            // 
            // Forum_start
            // 
            this.Forum_start.Location = new System.Drawing.Point(406, 27);
            this.Forum_start.Name = "Forum_start";
            this.Forum_start.Size = new System.Drawing.Size(152, 60);
            this.Forum_start.TabIndex = 4;
            this.Forum_start.Text = "Start";
            this.Forum_start.UseVisualStyleBackColor = true;
            this.Forum_start.Click += new System.EventHandler(this.Forum_start_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.KMS_Start);
            this.panel1.Location = new System.Drawing.Point(12, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 113);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.Forum_start);
            this.panel2.Location = new System.Drawing.Point(12, 238);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(577, 131);
            this.panel2.TabIndex = 7;
            // 
            // KMSWorker
            // 
            this.KMSWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.KMSWorker_DoWork);
            // 
            // ForumWorker
            // 
            this.ForumWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ForumWorker_DoWork);
            // 
            // Main_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 446);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "Main_Server";
            this.Text = "Key_Management_Server";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button KMS_Start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Forum_start;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker KMSWorker;
        private System.ComponentModel.BackgroundWorker ForumWorker;
    }
}