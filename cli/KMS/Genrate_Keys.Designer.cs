namespace OpenSSL.CLI
{
  partial class Genrate_Keys
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
            this.label2 = new System.Windows.Forms.Label();
            this.key_txt = new System.Windows.Forms.TextBox();
            this.Genrate_Btn = new System.Windows.Forms.Button();
            this.Out_box = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(155, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "PKI Key Generator Module";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select No of keys";
            // 
            // key_txt
            // 
            this.key_txt.Location = new System.Drawing.Point(212, 107);
            this.key_txt.Name = "key_txt";
            this.key_txt.Size = new System.Drawing.Size(100, 22);
            this.key_txt.TabIndex = 2;
            // 
            // Genrate_Btn
            // 
            this.Genrate_Btn.Location = new System.Drawing.Point(437, 90);
            this.Genrate_Btn.Name = "Genrate_Btn";
            this.Genrate_Btn.Size = new System.Drawing.Size(139, 57);
            this.Genrate_Btn.TabIndex = 3;
            this.Genrate_Btn.Text = "Generate";
            this.Genrate_Btn.UseVisualStyleBackColor = true;
            this.Genrate_Btn.Click += new System.EventHandler(this.Genrate_Btn_Click);
            // 
            // Out_box
            // 
            this.Out_box.Location = new System.Drawing.Point(31, 163);
            this.Out_box.Name = "Out_box";
            this.Out_box.Size = new System.Drawing.Size(545, 183);
            this.Out_box.TabIndex = 4;
            this.Out_box.Text = "";
            // 
            // Genrate_Keys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 358);
            this.Controls.Add(this.Out_box);
            this.Controls.Add(this.Genrate_Btn);
            this.Controls.Add(this.key_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Genrate_Keys";
            this.Text = "Genrate_Keys";
            this.ResumeLayout(false);
            this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox key_txt;
	private System.Windows.Forms.Button Genrate_Btn;
	private System.Windows.Forms.RichTextBox Out_box;
  }
}