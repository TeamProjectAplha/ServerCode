using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenSSL.CLI
{
    public partial class Main_Server : Form
    {
        public Main_Server()
        {
            InitializeComponent();
        }

        private void KMS_Start_Click(object sender, EventArgs e)
        {
            KMSWorker.RunWorkerAsync();
        }

        private void KMSWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            KMS_Server kms = new KMS_Server(this);
            kms.StartServer();
        }
        public void Kms_server_started()
        {
            this.Invoke((MethodInvoker)delegate { KMS_Start.BackColor = Color.LightGreen; KMS_Start.Enabled = false; });
        }

        private void Forum_start_Click(object sender, EventArgs e)
        {
            ForumWorker.RunWorkerAsync();
        }
        private void ForumWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Forum_Server FS = new Forum_Server(this);
            FS.StartServer();
        }
        public void Forum_server_started()
        {
            this.Invoke((MethodInvoker)delegate { Forum_start.BackColor = Color.LightGreen; Forum_start.Enabled = false; });
        }

    }
}
