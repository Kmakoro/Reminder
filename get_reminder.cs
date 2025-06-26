using System.ComponentModel;
using System.IO.Packaging;
using System.Text.RegularExpressions;
using System.Windows.Markup;

namespace Reminder
{
    public class get_reminder
    {//global arrays or generics
        private List<string> descriptions = new List<string>();
        private List<string> dates = new List<string>();

        //validate user input
        public string validate_input(string user_input)
        {
            //check if empty
            if (!string.IsNullOrEmpty(user_input))
            {
                return "Found";
            }
            return "Please add task";
        }//end of string validation

        //method to get number
        public string get_days(string day) 
        {
            //get number from the users input
            string get_day_in = Regex.Replace(day,@"[^\d]","");

            //check if its zero 0
            if(get_day_in.Equals("0"))
            {
                return "today";
            }
            else
            {
                return get_day_in;
            }
        }//end of get days method

        //method to store todays date
        public string today_date(string description, string date) 
        {
            //validate
            if (date == "today")
            {
                //get the date
                DateTime today_date = DateTime.Now.Date;
               
                string format_date = today_date.ToString("yyyy-MM-dd");

                //get the Time 
                TimeSpan today_time = DateTime.Now.TimeOfDay;
                string format_time = today_time.ToString();

                format_time = format_time.Substring(0, 8);

                //add all
                descriptions.Add(description);
                dates.Add(format_date);

                return "reminder will be " + date + " " + format_date + " " + format_time;

            }
            else
            {
                return "error";
            }  
        }//end of today date method

        //get reminder date
        public string get_remindDate(string description, string date)
        {
            //get current date
            DateTime current_date = DateTime.Now.Date;

            //then format date
            string format_date = current_date.ToString("yyyy-MM-dd");

            //get the day in the format
            string find_date = format_date.Substring(8,2);

            //get date from 2 to 8
            string final_date = format_date.Substring(0,8);

            //cast find date and add it to current date
            int total_days = int.Parse(find_date) + int.Parse(date);

            //store final date with total days
            string store_date = final_date + total_days;

            descriptions.Add(description);
            dates.Add(date);

            return "done";

            
        }//end of get remind date

        //method to check reminders
        public string get_remind()
        {
            //then search for today
            DateTime today = DateTime.Now.Date;
            string now_date = today.ToString("yyyy-MM-dd");

            string found_remind = "";

            for (int count = 0; count < dates.Count; count++)
            {
                //check for the date
                if (dates[count].ToString() == now_date)
                {
                    //then append message
                    found_remind += "\nDue Today: " + descriptions[count].ToString() + "\n" + dates[count].ToString();
                }//end of if
                else
                {
                    found_remind +=  "Description: " + descriptions[count].ToString() + "\nPeriod: " + dates[count].ToString() +" Days.";
                }

            }
            return found_remind;
        }
    }//end of class
}//end of namespace