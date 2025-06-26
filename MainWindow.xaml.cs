using System.Text;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //creating an instance of a class get reminder [Global]
        get_reminder reminder = new get_reminder();
        UserLogin user = new UserLogin();
        ChatBot chatBot;
        public string hold_task = String.Empty;
        string username = String.Empty;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized; // Maximize window

            //implimenting the NLP Model


        }



        private void set_reminder(object sender, RoutedEventArgs e)
        {//temporary variabl to collect users input

            string temp_userTask = user_task.Text.ToString();

            //check if empty or not
            if (!reminder.validate_input(temp_userTask).Equals("Found"))
            {
                //show error message
                MessageBox.Show(reminder.validate_input(temp_userTask));
                return;//break out of the method
            }
            temp_userTask = temp_userTask.ToLower();

            //Check using the chatbot class for the user input
            if (!temp_userTask.Contains("add task"))
            {
                chat_append.Items.Add(chatBot.checkQuestion(temp_userTask));
            }
            if (!temp_userTask.Contains("remind"))
            {
                chat_append.Items.Add(chatBot.checkQuestion(temp_userTask));
            }
            if (!temp_userTask.Contains("show reminder"))
            {
                chat_append.Items.Add(chatBot.checkQuestion(temp_userTask));
            }




            //if all validated then check for tasks


            //checks if user input conatains add task
            if (temp_userTask.Contains("add task"))
            {
                //replace the found task on add task
                string get_description = temp_userTask.Replace("add task ", "");


                //add the task to the list view but get date and time
                DateTime date = DateTime.Now.Date;
                DateTime time = DateTime.Now.ToLocalTime();


                //set the format for the date
                string format_date = date.ToString("yyyy-MM-dd");

                //then add to the list
                chat_append.Items.Add(username + " : " + get_description + "\n" + format_date + " Time " + time +"\nWould you like a Reminder?");

                //set list view to auto scroll
                chat_append.ScrollIntoView(chat_append.Items[chat_append.Items.Count - 1]);

                //add task message to the list view
                //chat_append.Items.Add("Task Added with the description " + "'" + get_description + "'" + " \nWould you like a Reminder?");
                user_task.Text = "";


                //then assign the task to the global variable
                hold_task = get_description;

            }
            else if (temp_userTask.Contains("show reminder"))
            {
                chat_append.Items.Add("Your reminders are");
                chat_append.Items.Add(reminder.get_remind());
            }

            //view task

            else if (temp_userTask.Contains("remind"))
            {//check if conatains remind
                //check task holder
                if (hold_task != "")
                {
                    //get the day in users input
                    string hold_day = reminder.get_days(temp_userTask);

                    if (hold_day == "today")
                    {
                        //get the messafe
                        if (reminder.today_date(hold_task, hold_day) != "error")
                        {
                            //add to list
                            chat_append.Items.Add(reminder.today_date(hold_task, hold_day));
                        }
                        else
                        {
                            chat_append.Items.Add("sorry , system chatbot failed to add task");
                        }
                    }
                    else
                    {

                        
                        //get to calculate the days
                        if (reminder.get_remindDate(hold_task, hold_day) == "done")
                        {
                            chat_append.Items.Add("great I will remind you in " + hold_day + " days");

                        }


                    }

                    //MessageBox.Show("Remaing days to reminder are " + hold_day);
                    //chat_append.Items.Add("Days to reminder " + hold_day);
                    //user_task.Text = "";
                }
                else
                {
                    //show error message
                    System.Console.Beep();
                    chat_append.Items.Add("No task was set to remind you");
                }
            }//end of if statement

            
            

        }

        private void mcq_button(object sender, RoutedEventArgs e)
        {
            chat_grid.Visibility = Visibility.Hidden;
            game_grid.Visibility = Visibility.Visible;
        }

        private void task_button(object sender, RoutedEventArgs e)
        {
            game_grid.Visibility = Visibility.Hidden;
            chat_grid.Visibility = Visibility.Visible;
        }

        private void submit_username(object sender, RoutedEventArgs e)
        {
            username = txt_username.Text as string;
            if (username != "")
            {
                // Set the username in the UserLogin class
                user.setUsername(username);
                // Set the username in the chat bot
                chatBot = new ChatBot(user.getUsername());
                // Show a welcome message
                login_grid.Visibility = Visibility.Hidden;
                application_grid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Error please enter a username");
            }
        }

        private void refresh_button(object sender, RoutedEventArgs e)
        {

        }

        private void chat_button(object sender, RoutedEventArgs e)
        {
            if (game_grid.Visibility == Visibility.Visible)
            {
                game_grid.Visibility = Visibility.Hidden;
                chat_grid.Visibility = Visibility.Visible;
            }
            else if (activity_log_grid.Visibility == Visibility.Visible)
            {
                activity_log_grid.Visibility = Visibility.Hidden;
                chat_grid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No chat to show");
            }
        }

        private void game_button(object sender, RoutedEventArgs e)
        {
            if (activity_log_grid.Visibility == Visibility.Visible)
            {
                activity_log_grid.Visibility = Visibility.Hidden;
                game_grid.Visibility = Visibility.Visible;
            }
            else if (chat_grid.Visibility == Visibility.Visible)
            {
                chat_grid.Visibility = Visibility.Hidden;
                game_grid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No game to show");
            }
        }

        private void activity_button(object sender, RoutedEventArgs e)
        {
            if (game_grid.Visibility == Visibility.Visible)
            {
                game_grid.Visibility = Visibility.Hidden;
                activity_log_grid.Visibility = Visibility.Visible;
            }
            else if (chat_grid.Visibility == Visibility.Visible)
            {
                chat_grid.Visibility = Visibility.Hidden;
                activity_log_grid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No activity to show");

            }
        }

        private void show_chats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //get the selected item from the list view
            string selected_task = chat_append.SelectedItem.ToString();


            //check if the task is not marked done
            if (!selected_task.Contains("status done"))
            {

                //get the index of the selected item
                int getIndex = chat_append.Items.IndexOf(selected_task);
                if (selected_task.Contains("Description"))
                {
                    //edit the selected item to be marked done
                    chat_append.Items[getIndex] = selected_task + " status done";
                }
            }
            else
            {
                //then remove if marked done

                //get the index

                chat_append.Items.Remove(selected_task);

            }
        }
    }
}