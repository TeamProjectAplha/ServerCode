using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenSSL.Crypto;
using System.IO;
using OpenSSL.Core;

namespace OpenSSL.CLI
{
    public partial class Genrate_Keys : Form
    {
        private const string Private_Keys = "Private_Keys.csv";
        private const string Public_Keys = "Public_Keys.csv";
        public Genrate_Keys()
        {
            InitializeComponent();
        }

        private void Genrate_Btn_Click(object sender, EventArgs e)
        {

        }
        private void Check_PKI()
        {
            BIO bio = BIO.MemoryBuffer();
            var keys = File.ReadAllLines(Private_Keys);
            byte[] bytes;
            for (int i = 1; i < 28; i++)
            {
                string temp = keys[i] + "\n";

                bytes = Encoding.ASCII.GetBytes(temp);

                bio.Write(bytes, bytes.Length);
            }

            var rsa = RSA.FromPrivateKey(bio);

            bytes = Encoding.ASCII.GetBytes("Hello");

            byte[] ctext = rsa.PublicEncrypt(bytes, RSA.Padding.PKCS1);

            byte[] ptext = rsa.PrivateDecrypt(ctext, RSA.Padding.PKCS1);
        }
        private void Generate_Private_PKI()
        {
            Out_box.Clear();
            int no_of_keys = 0;
            try
            {
                if (Convert.ToInt32(key_txt.Text) > 0)
                    no_of_keys = Convert.ToInt32(key_txt.Text);
                else
                    throw new Exception() { };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter correct value!!");
            }

            for (int i = 0; i < no_of_keys; i++)
            {
                CmdGenRSA Priv_rsa = new CmdGenRSA();
                var rsa_priv_key = Priv_rsa.Generate_Private_Key(new string[] { "genrsa", "2048" });
                File.AppendAllText(Private_Keys, "User" + (i + 1) + "\n" + rsa_priv_key.PrivateKeyAsPEM);
                File.AppendAllText(Public_Keys, "User" + (i + 1) + "\n" + rsa_priv_key.PublicKeyAsPEM);
                Out_box.AppendText("User" + (i + 1) + " Public Key Generated..\n");
            }
            MessageBox.Show(this, "All keys Generated successfully.", "Keys Generation", MessageBoxButtons.OK);
        }
    }
}
