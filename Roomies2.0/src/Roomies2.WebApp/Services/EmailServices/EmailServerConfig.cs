using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies2.WebApp.Services.EmailServices
{
    public class EmailServerConfig
    {
        string _smtpAddress;
        int _port;
        bool _ssl;
        string _email;
        string _password;

        public EmailServerConfig(string smtpAddress, int port, bool ssl, string email, string password)
        {
            _smtpAddress = smtpAddress;
            _port = port;
            _ssl = ssl;
            _email = email;
            _password = password;
        }

        public string SMTPAddress
        {
            get{return _smtpAddress;}
            set {  _smtpAddress = value;}
        }
        public int Port { get { return _port; }
            set { _port = value; }
        }
        public bool Ssl
        {
            get { return _ssl; }
            set { _ssl = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}
