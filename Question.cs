using System.Collections.Generic; 

namespace csharpquiz
{
    // Object that holds all the attributes of a question
    public class Question
    {
        public string text; // Full text of question to display to the console
        public string correct; // Single letter corresponding to the correct answer
        public string[] answers = new string[4]; // Possible answers to display to the console
    }
}