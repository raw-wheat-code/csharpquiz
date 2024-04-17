using System.Collections.Generic;

using System.Formats.Asn1;
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

        public static bool QuestionCountValidator(List<string> questionCount, string userInput)
        {
            char letter = 'A';

            foreach (string count in questionCount)
            {
                for (int i = 0; i < questionCount.Count; i++)
                {
                    letter = (char)(letter + i);
                    if (userInput == count)
                    {
                        return false;
                    }
                    else if (userInput.ToLower() == letter.ToString().ToLower())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }






















}