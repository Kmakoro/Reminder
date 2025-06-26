using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder
{
    class QuizQuestion
    {
        //getters and setters
        public string Question { get; set; }
        public string CorrectChoice { get; set; }

        //get and set on the List 
        public List<string> Choices { get; set; }
    }
}
