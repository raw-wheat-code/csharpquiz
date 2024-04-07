using System;

namespace csharpquiz
{
    class Program
    {
        public GenerateQuiz generate;
        static void Main(string[] args)
        {
            GenerateQuiz generateQuiz = new GenerateQuiz();
            generateQuiz.LoadQuiz();
        }
    }
}
