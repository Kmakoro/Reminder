using System;

namespace Reminder
{
    public class UserLogin
    {
        //create an empty string variable
        private string username = string.Empty;

        //setter
        public void setUsername(string username)
        {

            this.username = username;

        }
        //getter
        public string getUsername()
        {
            return this.username;
        }
    }
}