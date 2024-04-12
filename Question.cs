using System.Collections.Generic; 

namespace csharpquiz
{
    // Object that holds all the attributes of a question
    public class Question
    {
        public string question; // Full text of question to display to the console
        public string correct; // Single letter corresponding to the correct answer
        public List<string> answers = new List<string>(); // Possible answers to display to the console
        public int group; // identifier assigned to groups of questions that are entered at the same time
    }
}