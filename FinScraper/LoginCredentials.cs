using System;
using System.Collections.Generic;
using System.Text;

namespace FinScraper
{
    public class LoginCredentials
    {
        public string Username { get; }
        public readonly string Password;

        public LoginCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
