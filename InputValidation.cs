using System.Collections.Generic;
using System.Linq;

namespace csharpquiz
{
    public class InputValidation
    {
        public static bool AnswerValidator(Question question, string input)
        {
            char letter = 'A';
            int i = 0;
            foreach (string answer in question.answers)
            {
                var answerLetter = ((char)(letter + i)).ToString();

                // Check if the user input matches any of the expected formats
                if (input.ToLower() == answer.ToLower() ||
                    input.ToUpper() == answerLetter)
                {
                    return true;
                }
                i++;
            }
            return false;

        }
        public static bool TopicValidator(int input, TopicList topics)
        {

            if (input <= topics.Topics.Count())
            {
                return true;
            }

            return false;
        }

        public static bool QuestionCountInputIsInvalid(List<string> questionCount, string userInput)
        {
            int i = 0;

            // 5, 10, 25, 50, 100

            foreach (string count in questionCount)
            {
                char letter = (char)('A' + i);

                if ((userInput == count) || userInput.ToLower() == letter.ToString().ToLower())
                {
                    return false;
                }
                i++;
            }
            return true;
        }
    }

    // public static bool MenuOptionValidator(string userInput)
    // {
    //     int selection = 0;
    //     return int.TryParse(userInput, out selection);

    // }
}