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
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Media;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //**************************************Game Quiz**********************************************//
        //global declaration for all instances and variables

        //List [ generic ]
        private List<QuizQuestion> quizData;

        //variables
        private int questionIndex = 0;
        private int currentScore = 0;

        //buttons
        private Button selectedChoice = null;
        private Button correctChoiceButton = null;
        //*************************************************************************************************//

        //**************************************NLP Machine Learning**********************************************//
        // ML.NET context
        private readonly MLContext mlContext;

        // List to store training data dynamically
        private List<SentimentData> trainingData;

        // Prediction engine
        private PredictionEngine<SentimentData, SentimentPrediction> predEngine;

        // Sentiment input data class
        private class SentimentData
        {
            public string Text { get; set; }
            public bool Label { get; set; }
        }

        // Sentiment prediction result class
        private class SentimentPrediction
        {
            [ColumnName("PredictedLabel")]
            public bool Prediction { get; set; }
            public float Probability { get; set; }
            public float Score { get; set; }
        }
        //*******************************************************************************************************//

        SoundPlayer player;
        //*********************************audio method***********************************//
        public void greet()
        {
            //creating an instance for the media class
            MediaPlayer Audio_greet = new MediaPlayer();
            

            //get the path automatical
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;

            //then replace the \\bin\\Debug\\net8.0-windows
            string replaced = fullPath.Replace("\\bin\\Debug\\net8.0-windows", "");

            //combine paths once done replacing
            string combine_path = System.IO.Path.Combine(replaced, "AI voice.wav");

            //combine the url as uri
            Audio_greet.Open(new Uri(combine_path, UriKind.Relative));
            player.SoundLocation = combine_path;
            //play sound
            player.Play();

        }
        //********************************************************************************//

        //create getReminder object
        get_reminder reminder = new get_reminder();
        //create user login object  
        UserLogin user = new UserLogin();
        //create chat bot object
        ChatBot chatBot;
        //hold the task
        public string hold_task = String.Empty;
        //hold the username
        string username = String.Empty;
        //create a memory recall object
        MemoryRecall memoryRecall;


        public MainWindow()
        {
            InitializeComponent();
            player = new SoundPlayer();
            this.WindowState = WindowState.Maximized; // Maximize window
            greet();
            //*********************************************NPL Machine Learning**********************************************//
            //implimenting the NLP Model
            // Initialize ML context
            mlContext = new MLContext();

            // Initialize with base training data
            trainingData = new List<SentimentData>
            {
                new SentimentData { Text = "I am happy", Label = true },
                new SentimentData { Text = "I hate this", Label = false },
                new SentimentData { Text = "I am sad", Label = false },
                new SentimentData { Text = "I am good", Label = true },
                new SentimentData { Text = "I am happy", Label = true },
                new SentimentData { Text = "I feel great", Label = true },
                new SentimentData { Text = "Life is good", Label = true },
                new SentimentData { Text = "Everything is fine", Label = true },
                new SentimentData { Text = "I love this", Label = true },
                new SentimentData { Text = "I am doing well", Label = true },
                new SentimentData { Text = "I am good", Label = true },
                new SentimentData { Text = "Feeling awesome", Label = true },
                new SentimentData { Text = "I hate this", Label = false },
                new SentimentData { Text = "I am sad", Label = false },
                new SentimentData { Text = "This is terrible", Label = false },
                new SentimentData { Text = "I feel awful", Label = false },
                new SentimentData { Text = "I am not okay", Label = false },
                new SentimentData { Text = "I am depressed", Label = false },
                new SentimentData { Text = "This sucks", Label = false }
            };

            TrainModel();
            //*****************************************************************************************************************//
            //*********************************************Game Quiz**********************************************//
            //call the load quiz method 
            LoadQuizData();

            showQuiz();
            //*************************************************************************************************//
        }

        

        private void set_reminder(object sender, RoutedEventArgs e)
        {


            //temporary variabl to collect users input
            //add the task to the list view but get date and time
            DateTime date = DateTime.Now.Date;
            DateTime time = DateTime.Now.ToLocalTime();
            //set the format for the date
            string format_date = date.ToString("yyyy-MM-dd");
            string temp_userTask = user_task.Text.ToString();

            //Check using the chatbot class for the user input



            //check if empty or not
            if (!reminder.validate_input(temp_userTask).Equals("Found"))
            {
                //show error message
                chat_append.Items.Add(reminder.validate_input(temp_userTask));

                return;//break out of the method
            }
            chat_append.Items.Add(time + " " + username + " : " + temp_userTask);
            if (chatBot.checkQuestion(temp_userTask) != "")
            {
                chat_append.Items.Add(time + " CoCo Bot :" + chatBot.checkQuestion(temp_userTask));
            }

            temp_userTask = temp_userTask.ToLower();


            //activity_log_list.Items.Add(time + " " + chatBot.checkQuestion(temp_userTask));

            //if all validated then check for tasks


            //checks if user input conatains add task
            if (temp_userTask.Contains("add task"))
            {
                //replace the found task on add task
                string get_description = temp_userTask.Replace("add task ", "");

                //then add to the list
                chat_append.Items.Add(time + " CoCo Bot : " + "Would you like a Reminder for " + get_description + " task?");
                //activity_log_list.Items.Add(time + " " + username + " : " + get_description + "\nWould you like a Reminder?");


                //add task message to the list view
                //chat_append.Items.Add("Task Added with the description " + "'" + get_description + "'" + " \nWould you like a Reminder?");
                user_task.Text = "";


                //then assign the task to the global variable
                hold_task = get_description;

            }
            else if (temp_userTask.Contains("show reminder"))
            {
                chat_append.Items.Add(time + " CoCo Bot :" + "Your reminders are");
                chat_append.Items.Add(time + " CoCo Bot :" + reminder.get_remind());
                // activity_log_list.Items.Add(time + " " + "Your reminders are");
                //activity_log_list.Items.Add(time + " " + reminder.get_remind());
            }

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
                            chat_append.Items.Add(time + " CoCo Bot :" + reminder.today_date(hold_task, hold_day));
                            // activity_log_list.Items.Add(time + " " + reminder.today_date(hold_task, hold_day));
                        }
                        else
                        {
                            chat_append.Items.Add(time + " CoCo Bot :" + "sorry , system chatbot failed to add task");
                            // activity_log_list.Items.Add(time + " " + "sorry , system chatbot failed to add task");
                        }
                    }
                    else
                    {


                        //get to calculate the days
                        if (reminder.get_remindDate(hold_task, hold_day) == "done")
                        {
                            chat_append.Items.Add(time + " CoCo Bot :" + "great I will remind you in " + hold_day + " days");
                            //activity_log_list.Items.Add(time + " " + "great I will remind you in " + hold_day + " days");
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
                    chat_append.Items.Add(time + " CoCo Bot :" + "No task was set to remind you");
                    //activity_log_list.Items.Add(time + " " + "No task was set to remind you");
                }

            }//end of if statement


            emotions();
            //Auto Scroll
            chat_append.ScrollIntoView(chat_append.Items[chat_append.Items.Count - 1]);

            //********************************Save chats to activity Log*********************************//
            // Save the chat to a file
            List<string> chats = chat_append.Items.Cast<string>().ToList();
            string list = ConvertListToString(chats);
            string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            string new_path = fullpath.Replace("bin\\Debug\\net8.0-windows\\", "");
            String File_name = new_path + user.getUsername() + ".txt";
            FileStream fs = new FileStream(File_name, FileMode.Append, FileAccess.Write);
            if (System.IO.File.Exists(File_name) == true)
            {

                StreamWriter objWrite = new StreamWriter(fs);
                objWrite.Write(list);
                objWrite.Close();
            }
            //***************************************************************************************//

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
                // Create a memory recall object with the username
                memoryRecall = new MemoryRecall(user.getUsername());
                // Show a welcome message
                login_grid.Visibility = Visibility.Hidden;
                application_grid.Visibility = Visibility.Visible;



                //********************Create Activity Log**************************//
                string fullpath = AppDomain.CurrentDomain.BaseDirectory;
                string new_path = fullpath.Replace("bin\\Debug\\net8.0-windows\\", "");
                String File_name = new_path + user.getUsername() + ".txt";
                if (System.IO.File.Exists(File_name) == false)
                {
                    FileStream fs = new FileStream(File_name, FileMode.Create, FileAccess.Write);
                    MessageBox.Show("File Not Exist \nFile Created for user : " + user.getUsername());

                }

                //**********************************************************************//

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

        public static string ConvertListToString(List<string> stringList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string element in stringList)
            {
                stringBuilder.Append(element).Append(", \n");
            }
            string joinedEvents = stringBuilder.ToString().TrimEnd(',', ' ');
            return joinedEvents;
        }

        private void activity_button(object sender, RoutedEventArgs e)
        {
            string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            string new_path = fullpath.Replace("bin\\Debug\\net8.0-windows\\", "");
            String File_name = new_path + user.getUsername() + ".txt";
            if (System.IO.File.Exists(File_name) == true)

            {
                System.IO.StreamReader objReader;
                objReader = new StreamReader(File_name);
                activity_log_list.Text = objReader.ReadToEnd();
                objReader.Close();

            }


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

            //activity_log_list.ScrollIntoView(activity_log_list.Items[activity_log_list.Items.Count - 1]);


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
            if (chat_append.Items.Count != 0)
            {
                List<string> chats = chat_append.Items.Cast<string>().ToList();
                memoryRecall.save_memory(chats);
            }

        }




        //**********************************Machine Learning Methods******************************************//
        // Method to train/retrain the model
        private void TrainModel()
        {
            var trainDataView = mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            var model = pipeline.Fit(trainDataView);

            predEngine = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        }

        // Event triggered on emotion check
        private void emotions()
        {
            DateTime time = DateTime.Now.ToLocalTime();
            string input = user_task.Text as String;

            if (string.IsNullOrWhiteSpace(input))
            {
                //MessageBox.Show("Please enter how you are feeling");
                return;
            }

            var prediction = predEngine.Predict(new SentimentData { Text = input });

            // Confidence percentages
            float positiveScore = prediction.Probability * 100;
            float negativeScore = 100 - positiveScore;

            string emotionType = prediction.Prediction ? "Positive" : "Negative";

            // Construct feedback message
            string feedback = $"{emotionType} Emotion\n" +
                              $"Positive: {positiveScore:F1}%\n" +
                              $"Negative: {negativeScore:F1}%\n";

            string reply;
            if (positiveScore > 75)
            {
                reply = "You seem really upbeat! Keep shining ";
            }
            else if (positiveScore > 50)
            {
                reply = "You’re doing alright — keep your chin up!";
            }
            else if (positiveScore > 30)
            {
                reply = "I sense some heaviness — it’s okay to feel down sometimes.";
            }
            else
            {
                reply = "You seem quite low. Be kind to yourself — brighter days will come.";
            }
            //chat_append.Items.Add(time + " " + feedback + reply);
            // chat_append.ScrollIntoView(activity_log_list.Items[activity_log_list.Items.Count - 1]);
            //show_emotion_detected.Text = feedback + reply;

            // Ask user to confirm if prediction was correct
            var result = MessageBox.Show(feedback + reply + "\nWas this prediction correct?", "Feedback", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                // Ask what the correct label was
                var correct = MessageBox.Show("Was it actually Positive?", "Correct Label", MessageBoxButton.YesNo);
                bool correctLabel = (correct == MessageBoxResult.Yes);

                // Add to training data and retrain model
                trainingData.Add(new SentimentData { Text = input, Label = correctLabel });
                TrainModel();

                MessageBox.Show("Thanks! I've learned from that.");
            }
        }
        //*******************************************************************************************************//

        //method to show the  quiz on the buttons
        private void showQuiz()
        {

            //check if the user is not done playing
            if (questionIndex >= quizData.Count)
            {
                //show complete message
                MessageBox.Show("You already completed the game with " + currentScore + " score");
                //then reset the game
                currentScore = 0;
                currentScore = 0;
                questionIndex = 0;

                DisplayScore.Text = "";

                showQuiz();
                //stop the execute
                return;
            }

            //get the current index quiz
            correctChoiceButton = null;
            selectedChoice = null;

            //then get all the questions values
            var currentQuiz = quizData[questionIndex];

            //displays the question to the user
            DisplayedQuestion.Text = currentQuiz.Question;



            //**********//
            var allChoices = new List<string>(currentQuiz.Choices)
            {
                 currentQuiz.CorrectChoice
            };
            //**********//
            //add the choices to the buttons
            var shuffled = allChoices.OrderBy(_ => Guid.NewGuid()).ToList();




            //then add by index
            FirstChoiceButton.Content = shuffled[0];
            SecondChoiceButton.Content = shuffled[1];
            ThirdChoiceButton.Content = shuffled[2];
            FourthChoiceButton.Content = shuffled[3];

            //******//
            // Identify which button has the correct answer
            if ((string)FirstChoiceButton.Content == currentQuiz.CorrectChoice) correctChoiceButton = FirstChoiceButton;
            else if ((string)SecondChoiceButton.Content == currentQuiz.CorrectChoice) correctChoiceButton = SecondChoiceButton;
            else if ((string)ThirdChoiceButton.Content == currentQuiz.CorrectChoice) correctChoiceButton = ThirdChoiceButton;
            else if ((string)FourthChoiceButton.Content == currentQuiz.CorrectChoice) correctChoiceButton = FourthChoiceButton;
            //******//

            //correct one
            //FourthChoiceButton.Content = currentQuiz.CorrectChoice;

            clearStyle();
        }

        //method to rest the buttons
        private void clearStyle()
        {
            //use for each to reset
            foreach (Button choice in new[] { FirstChoiceButton, SecondChoiceButton, ThirdChoiceButton, FourthChoiceButton })
            {

                choice.Background = Brushes.LightGray;


            }


        }//end of the clear style method 


        //method to load the quiz data
        private void LoadQuizData()
        {
            //store info
            quizData = new List<QuizQuestion> {

                new QuizQuestion
                {
                    Question="What is Cyber Security?" ,
                    CorrectChoice ="All of the mentioned" ,
                    Choices = new List<string>
                    {
                        "Cyber Security provides security against malware","Cyber Security provides security against cyber-terrorists","Cyber Security protects a system from cyber attacks","All of the mentioned"
                    }

                }   ,
                new QuizQuestion{

                    Question="What does cyber security protect?" ,
                    CorrectChoice ="Cyber security protects internet-connected systems" ,
                    Choices = new List<string>{
                    "Cyber security protects criminals","Cyber security protects internet-connected systems","Cyber security protects hackers","None of the mentioned"
                    }

                },
                new QuizQuestion
                {
                    Question="Who is the father of computer security?" ,
                    CorrectChoice ="August Kerckhoffs" ,
                    Choices = new List<string>
                    {
                        "August Kerckhoffs","Bob Thomas","Robert","Charles"
                    }

                }   ,
                new QuizQuestion{

                    Question="The following is defined as an attempt to steal, spy, damage or destroy computer systems, networks?" ,
                    CorrectChoice ="Cyber attack" ,
                    Choices = new List<string>{
                    "Cyber attack","Computer security","Cryptography","Digital hacking"
                    }

                },
                new QuizQuestion
                {
                    Question="Which of the following is a type of cyber security?" ,
                    CorrectChoice ="All of the above" ,
                    Choices = new List<string>
                    {
                        "Cloud Security", "Network Security", "Application Security","All of the above"
                    }

                }   ,
                new QuizQuestion{

                    Question="What are the features of cyber security?" ,
                    CorrectChoice ="All of the above" ,
                    Choices = new List<string>{
                    "Compliance", "Defense against internal threats", "Threat Prevention", "All of the above"
                    }

                },
                new QuizQuestion
                {
                    Question="Which of the following is an objective of network security?" ,
                    CorrectChoice ="All of the above" ,
                    Choices = new List<string>
                    {
                        "Confidentiality","Integrity", "Availability", "All of the above"
                    }

                }   ,
                new QuizQuestion{

                    Question="Which of the following is not a cybercrime?" ,
                    CorrectChoice ="AES" ,
                    Choices = new List<string>{
                    "Denial of Service", "Man in the Middle","Malware", "AES"
                    }

                },
                new QuizQuestion
                {
                    Question="Which of the following is a component of cyber security?" ,
                    CorrectChoice ="Internet Of Things" ,
                    Choices = new List<string>
                    {
                        "Internet Of Things","AI" , "Database","Attacks"
                    }

                }   ,
                new QuizQuestion{

                    Question="Which of the following is a type of cyber attack?" ,
                    CorrectChoice ="All of the above" ,
                    Choices = new List<string>{
                    "Phishing","SQL Injections", "Password Attack","All of the above"
                    }

                }//end of second question, put , to add another one
             

            
            };


        }//end of the method load quiz data or info






        private void HandleAnswerSelection(object sender, RoutedEventArgs e)
        {

            //use sender object name to get the selected button
            selectedChoice = sender as Button;

            string chosen = selectedChoice.Content.ToString();

            //then check with correct on the current quiz
            string correct = quizData[questionIndex].CorrectChoice;

            //then check if correct or not by if statement
            if (chosen == correct)
            {
                //then set the button background color
                selectedChoice.Background = Brushes.Green;
                //assing to hold
                correctChoiceButton = selectedChoice;
            }
            else
            {
                //if incorrect
                selectedChoice.Background = Brushes.DarkRed;
                correctChoiceButton = selectedChoice;
            }



        }//end of handle answer selection event handler

        //event handler for the next button
        private void HandleNextQuestion(object sender, RoutedEventArgs e)
        {
            //check if the user selected one of the choices
            if (selectedChoice == null)
            {
                //then show error message
                MessageBox.Show("Choose one of the 4 choices");
            }
            else
            {
                //then add points , and only if correct
                string chosen = selectedChoice.Content.ToString();
                string correct = quizData[questionIndex].CorrectChoice;

                //check if correct 
                if (chosen == correct)
                {
                    //then add point
                    currentScore++;
                    //then show the score
                    DisplayScore.Text = "Score : " + currentScore;

                    //then move to the next index question
                    questionIndex++;
                    //show the question again for the next one
                    showQuiz();
                }
                else
                {
                    //move to the next question 
                    questionIndex++;
                    showQuiz();
                }

            }


        }//end of the handle next question event handler

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // Clean up resources or perform any necessary actions before exiting

        }


    }
}