using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenSSL.CLI
{
    class Check_User
    {
        private string user, password;
        private const string loginfile = "Login_credentials.csv";
        private const string Private_Keys = "Private_Keys.csv";
        private const string Public_Keys = "Public_Keys.csv";

        public Check_User(string user, string password)
        {
            this.user = user;
            this.password = password; 
        }
        public bool check_user()
        { return checkuser(); }
        private bool checkuser()
        {
            foreach (var credentials in File.ReadAllLines(loginfile))
            {
                if (credentials.Contains(user))
                {
                    var userpass = credentials.Split(',');
                    if (user.Equals(userpass[0]) && password.Equals(userpass[1]))
                        return true;
                }
            }
            return false;
        }

        public string Get_User_PKI()
        { return Get_PKI(); }
        private string Get_PKI()
        {
            var keys = File.ReadAllLines(Private_Keys);
            StringBuilder msg = new StringBuilder();
            for (int i = 0; i < keys.Length; i+=28)
            {
                string temp = keys[i];

                if (temp.Equals(user))
                {
                    msg.Append(temp + "\n");
                    for (int j = i+1; j < i + 28; j++)
                    { 
                        temp = keys[j] + "\n";
                        msg.Append(temp);
                    }
                    break;
                }
            }
            return msg.ToString();
        }

        public string Get_Users_PKI()
        { return Get_PKIs(); }
        private string Get_PKIs()
        {
            var keys = File.ReadAllLines(Public_Keys);
            StringBuilder msg = new StringBuilder();
            for (int i = 0; i < keys.Length; i ++)
            {
                string temp = keys[i] + "\n";
                msg.Append(temp);
            }
            return msg.ToString();
        }



    }
}
