using System;
using System.Collections.Generic;
using System.IO;

namespace csharpquiz
{
    // Class to load questions from json into a Quiz list
    public class GenerateQuiz
    {
        private static readonly string directoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private string filePath;

        public GenerateQuiz()
        {
            // filePath = Path.Combine(directoryPath, "questions.json");
            // Console.WriteLine("Directory Path: " + directoryPath);
            // Console.WriteLine("File Path: " + filePath);
            // Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());
            filePath = @"D:\Repos\csharpquiz\bin\Debug\net5.0\questions.json";
        }

        // Will load questions for a quiz, like:
        // private Quiz quiz = GenerateQuiz.LoadQuiz();
        //public List<Question> LoadQuiz()
        public void LoadQuiz()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while checking file existence: " + ex.Message);
            }
        }
    }
}