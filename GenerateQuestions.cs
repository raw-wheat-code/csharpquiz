using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;


namespace csharpquiz
{
    // Class to load questions from json into a Quiz list
    public class GenerateQuestions
    {
        // Declares file path and sets it equal to root directory of project
        private string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        public QuestionList LoadQuizFromJson()
        {
            // Navigates backwards to the correct directory in the project
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += @"\questions.json"; // Then adds the actual file name
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                QuestionList questionList = JsonConvert.DeserializeObject<QuestionList>(json);
                return questionList;
            }
            return new QuestionList();
        }
    }
}