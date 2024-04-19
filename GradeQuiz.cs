using System;
using System.Collections.Generic;

namespace csharpquiz
{

    public class GradeQuiz
    {
        private int questionCount = 0;
        private int correctCount = 0;

        // Call when a question is answered incorrectly
        // Do not call when answer is correct
        public void IncrementQuestionCount()
        {
            questionCount++;
        }

        public void ResetQuizScore()
        {
            questionCount = 0;
            correctCount = 0;
        }

        // Call when question is answered correctly
        public void IncrementCorrectCount()
        {
            IncrementQuestionCount();
            correctCount++;
        }

        public void GetScore()
        {
            double percentage = (double)correctCount / questionCount;
            Console.WriteLine("Your Score: " + correctCount + "/" + questionCount + " (" + percentage.ToString("P") + ")");
        }
    }
}