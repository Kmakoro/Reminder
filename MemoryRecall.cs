using System.Collections.Generic;
using System.IO;
using System;

namespace Reminder
{
    public class MemoryRecall
    {

        string username = string.Empty; //create an empty string variable
        public bool fileExists = false; //create a boolean variable to check if the file exists 
        public MemoryRecall(string username)
        { //beginng of contructor
            this.username = username;
            check_file();

        }//end of constructor

        //Since AppDomain as global gives an error of static
        //Lets do a return method for it to return the path
        private string path_return()
        {
            //App Domain get full path
            string fullpath = AppDomain.CurrentDomain.BaseDirectory;
            //then replace the bin/Debug/
            //so its bin\\Debug\\ inside the "" no space
            string new_path = fullpath.Replace("bin\\Debug\\", "");
            //now combine the path of new_path and the txt file
            string path = Path.Combine(new_path, username + ".txt");

            return path;
        }//end of return path method

        //Now lets do the three methods

        //Method to check the txt file and create if not found
        public void check_file()
        {
            try
            {
                //get the path
                string path = path_return();
                //Now check if the path exists or not
                //then if not found then create the txt file
                if (!File.Exists(path))
                {
                    //this by !
                    //it means if not, the path of the file is
                    //not found the create or do something
                    File.CreateText(path);
                }
                else
                {
                    //then if the file is found 
                    Console.WriteLine("File is found...");
                    fileExists = true;
                }
            }
            catch (Exception ex)
            {
                //display appropriate error message
                Console.WriteLine(ex.Message);
            }


        }//end of method check_file

        //Now for the get what is in the file method
        public List<string> return_memory()
        {
            try
            {
                //Now get the path of the file
                string path = path_return();

                return new List<string>(File.ReadAllLines(path));
                //by this you return all in the txt file
            }
            catch (Exception ex)
            {
                //display appropriate error message
                Console.WriteLine(ex.Message);
            }
            return null;

        }//end of return memory method

        //method to write to the file
        public void save_memory(List<string> save_new)
        {
            try
            {
                //get the path
                string path = path_return();

                //then for the parameter pass a List
                //then lets write into the txt file
                File.WriteAllLines(path, save_new);
                //File.AppendAllLines(path, save_new);
                //if you pass a variable it give you an error
                //you can test using variable

            }
            catch (Exception ex)
            {
                //display appropriate error message
                Console.WriteLine(ex.Message);
            }

        }//end of save memory method

        //Now you  are done with the File Memory class
        //Remember you can still use Arrays or Generics 
        //For memory recall

        //Go back to the program class in the main method


    }
}