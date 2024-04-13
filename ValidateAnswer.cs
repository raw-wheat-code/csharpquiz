using System.Collections.Generic;

using System.Formats.Asn1;

namespace csharpquiz
{
    public class ValidateAnswer
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

    }






















}