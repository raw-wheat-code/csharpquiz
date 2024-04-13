using System.Collections.Generic;

using System.Formats.Asn1;

namespace csharpquiz
{
    public class ValidateAnswer
    {
        public static bool AnswerValidator(Question question, string input)
        {
            foreach (string answer in question.answers)
            {
                char answerLetter = (char)('A' + question.answers.IndexOf(question.correct));
                // Convert user input to lowercase for case-insensitive comparison
                string answerLetterLower = answerLetter.ToString().ToLower();

                // Check if the user input matches any of the expected formats
                if (input.ToLower() == answer.ToLower() ||
                    input.ToLower() == answerLetterLower)
                    {
                        return true;
                    }
                
            }
            return false;

        }

    }






















}