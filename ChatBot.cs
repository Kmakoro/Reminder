using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace Reminder
{
    public class ChatBot
    {
        bool password = false;
        bool phishing = false;
        bool safebrowsing = false;
        bool virus = false;
        bool malware = false;
        bool ransomware = false;

        bool passwordMemory = false;
        bool phishingMemory = false;
        bool safebrowsingMemory = false;
        bool virusMemory = false;
        bool malwareMemory = false;
        bool ransomwareMemory = false;


        int programLoopCounter = 0;

        static List<string> save_Data = new List<string>();

        //username 
        private string user = string.Empty;
        //creaye cyber dictionary object to give responses
        private CyberDictionary responseDictionary;
        //string to hold the question
        private string question = string.Empty;
        private MemoryRecall memoryRecall;
        //constructor to initialize the chatbot
        public ChatBot(string user)
        {
            this.user = user;
            responseDictionary = new CyberDictionary();


        }

       
        

        public string checkQuestion(string question)
        {
            string final_response = string.Empty;

            Boolean passworddetected = false;
            Boolean phishingDetected = false;
            Boolean safebrowsingDetected = false;
            Boolean virusdeteDetected = false;
            Boolean malwareDetected = false;
            Boolean ransomwareDetected = false;


            //create a local string variable to hold the words
            string[] words;//array for the split function to split words

            //begin working if the string is not empty
            if (question != string.Empty)
            {

                words = question.Split(' ');
                int correctwords = 0;
                int incorrectwords = 0;
                //loop to check if the question contains correct words in the dictionary if not then the user will have to be prompted again
                for (int counter = 0; counter < words.Length; counter++)
                {
                    //check if the word is in the dictionary
                    if (responseDictionary.getKeywords().Contains(words[counter]))
                    {
                        correctwords++;
                    }
                    //check if the word is not in the dictionary
                    else
                    {
                        incorrectwords++;
                    }
                }//end of loop

                //check if no incorrect keywords were entered
                if (incorrectwords == 0)
                {
                    //loop through the words or the sentence
                    for (int index = 0; index < words.Length; index++)
                    {
                        //check if the user is asking about passwords
                        if (words[index].Contains("password"))
                        {

                            passworddetected = true;
                            password = true;

                        }
                        //check if the user is asking about phishing
                        if (words[index].Contains("phishing"))
                        {

                            phishingDetected = true;
                            phishing = true;
                        }
                        //check if the user is asking about safe browsing
                        if (question.Contains("safe browsing"))
                        {

                            safebrowsingDetected = true;
                            safebrowsing = true;
                        }
                        //check if the user is asking about virus
                        if (words[index].Contains("virus"))
                        {
                            virusdeteDetected = true;
                            virus = true;
                        }
                        //check if the user is asking about malware
                        if (words[index].Contains("malware"))
                        {
                            malwareDetected = true;
                            malware = true;
                        }
                        //check if the user is asking about ransomware
                        if (words[index].Contains("ransomware"))
                        {
                            ransomwareDetected = true;
                            ransomware = true;
                        }
                        //provide a response to the user to greet the user
                        if (question.Contains("hello"))
                        {
                            final_response +=(string.Concat("Hello ", user, " I trust you are well, " +
                                "how would you like me to assist you today? " +
                                "please note this chatbot can only respond to cybersecurity related questions."));
                            
                        }
                        //provide a response to the user to know how the chatbot is doing
                        if (question.Contains("how are you"))
                        {
                            final_response += ("I'm doing great, thank you for asking. How can I help you today?");
                            
                            
                        }
                        //provide a repsonse to the user to know the purpose of the chatbot
                        if (question.Contains("what's your purpose"))
                        {
                            final_response += ("I'm here to help you with any questions you have about cybersecurity. Feel free to ask me anything!");
                            
                        }
                        //provaide a response for the user to know what they can ask the chatbot
                        if (question.Contains("what can i ask you about"))
                        {
                            final_response += ("You can ask me about passwords, phishing, safe browsing, and more. Just type your question, and I'll do my best to help!");
                            
                        }
                        //provide a response to a user should there be a question without direct meaning
                        /* if(!phishingDetected && !passworddetected && !safebrowsingDetected)
                         {
                             botResponse("Remember you can ask me about anything related to cyber security examples include things like password, phishing and safe browsing!", true);
                             break;
                         }*/
                    }

                }
                //else give error should there be incorrect or non-words entered
                else
                {
                    //give error message for not understanding the question of the user or words used not in the dictionary
                    final_response = ("I didn't quite understand that. Could you rephrase? Remember to as me about topics related to cyber security");

                }
                //if password has been detected then proceed
                if (passworddetected)
                {
                    
                    
                        final_response += checkSentiment(question) + "\n" + Response(responseDictionary.getPasswordDictionary());
                    
                    
                    
                }
                //if phishing has been detected then proceed
                if (phishingDetected)
                {

                    final_response += checkSentiment(question) + "\n" + Response(responseDictionary.getPhishingDictionary());
                    
                   

                }
                //if safe browsing has been detected then proceed
                if (safebrowsingDetected)
                {
                    final_response += checkSentiment(question) + "\n" + Response(responseDictionary.getSafeBrosingDictionary());
                    
                  

                }
                if (virusdeteDetected)
                {
                    final_response += checkSentiment(question) + "\n" + Response(responseDictionary.getVirusDictionary());
                    
                }
                if (malwareDetected)
                {
                    final_response += checkSentiment(question) + "\n" + Response(responseDictionary.getMalwareDictionary());
                   
                }
                if (ransomwareDetected)
                {
                    final_response += checkSentiment(question) + "\n" + Response(responseDictionary.getRansomwareDictionary());
                   
                }

            }
            //else if the string is empty display appropriate message
            else
            {
                //give error message for not understanding the question of the user or words used not in the dictionary
                final_response = ("Remember to please ask a question related to cyber security to enjoy better experince from the program");

            }
            return final_response;
        }

        //method to check sentiment of the user
        private string checkSentiment(string question)
        {
            //create a local string variable to hold the words
            string[] words;//array for the split function to split words
            //split the question into words
            words = question.Split(' ');
            //loop through the words or the sentence
            for (int index = 0; index < words.Length; index++)
            {
                if (words[index].Contains("happy"))
                {//using lambda expression
                    return responseDictionary.sentimentKeyword.FirstOrDefault(x => x.Key == "happy").Value;
                }
                if (words[index].Contains("sad"))
                {
                    //using lambda expression
                    return responseDictionary.sentimentKeyword.FirstOrDefault(x => x.Key == "sad").Value;
                }
                if (words[index].Contains("excited"))
                {
                    //using lambda expression
                    return responseDictionary.sentimentKeyword.FirstOrDefault(x => x.Key == "excited").Value;
                }
                if (words[index].Contains("worried"))
                {
                    //using lambda expression
                    return responseDictionary.sentimentKeyword.FirstOrDefault(x => x.Key == "worried").Value;
                }
                if (words[index].Contains("frustrated"))
                {
                    //using lambda expression
                    return responseDictionary.sentimentKeyword.FirstOrDefault(x => x.Key == "frustrated").Value;
                }
                if (words[index].Contains("angry"))
                {
                    //using lambda expression
                    return responseDictionary.sentimentKeyword.FirstOrDefault(x => x.Key == "angry").Value;
                }
                if (words[index].Contains("scared"))
                {
                    //using lambda expression
                    return responseDictionary.sentimentKeyword.FirstOrDefault(x => x.Key == "scared").Value;
                }
                if (words[index].Contains("curious"))
                {
                    //using lambda expression
                    return responseDictionary.sentimentKeyword.FirstOrDefault(x => x.Key == "curious").Value;
                }
            }
            return "";
        }



       

        

        private void displayMemory(List<string> memory)
        {

            foreach (string line in memory)
            {
                Console.WriteLine(line);
                if (line.Contains("password"))
                {
                    passwordMemory = true;
                }
                if (line.Contains("phishing"))
                {
                    phishingMemory = true;
                }
                if (line.Contains("safe browsing"))
                {
                    safebrowsingMemory = true;
                }
                if (line.Contains("virus"))
                {
                    virusMemory = true;
                }
                if (line.Contains("malware"))
                {
                    malwareMemory = true;
                }
                if (line.Contains("ransomware"))
                {
                    ransomwareMemory = true;
                }
            }

        }

        //function to retrieve 3 random reponses based on Topic
        private string Response(List<string> Topic, string optional = null)
        {
            //local string variable to hold the response
            string response = string.Empty;
            //create a local random object
            Random random = new Random();
            //convert dictionary keys to a list for easy random access
            //List<string> randomkeys = Topic.Keys.ToList();

            string[] arrayTopic = (string[])Topic.ToArray();
            //randomly display 3 values from the dictionary
            for (int counter = 0; counter < 3; counter++)
            {
                //generate a random index or number
                int index = random.Next(arrayTopic.Length);

                //get the corresponding value pair at index
                string message = arrayTopic[index];
                //concatinate or join the responses on a new line
                response = response + '\n' + String.Concat((counter + 1), ". ", message);
            }
            //return the response should there be an optional value
            if (optional != null)
            {//text formatting and underling the optional value
                return string.Concat(optional, "\n", "\t\t\t", new string('-', optional.Length), "\n", response);
            }
            //return the response
            return response;
        }



    }
}