using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSSL.CLI
{
    class Forum_msgs
    {
        private const string Msgsfile = "Msgs.csv";

        public string GetMessages(string msgs)
        {
            if (msgs.Equals("GetAllMessages"))
            {
               return GetAllMessages();
            }
            return "";
        }
        private string GetAllMessages()
        {
            var data = File.ReadAllLines(Msgsfile);
            StringBuilder msg = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                string temp = data[i] + " & ";
                msg.Append(temp);
            }
            return msg.ToString();
        }

        public void send_Msgs(string user,string data)
        {
            saveMsgs(user, data);
        }
        private void saveMsgs(string user, string data)
        {
            try
            {
                File.AppendAllText(Msgsfile, user + "," + data + Environment.NewLine);
            }
            catch (Exception ex) { }
        }

    }
}
