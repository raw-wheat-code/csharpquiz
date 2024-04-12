using System;

namespace csharpquiz
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateQuestions generateQuestions = new GenerateQuestions();
            QuestionList questionList = generateQuestions.LoadQuizFromJson();
        }
    }
}
