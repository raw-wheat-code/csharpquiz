using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;


namespace csharpquiz
{
    // Class to load questions from json into a Quiz list
    public class GenerateQuestions
    {
        // Declares file path and sets it equal to root directory of project
        private string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        public QuestionList LoadQuizFromJson(int topicId)
        {
            // Navigates backwards to the correct directory in the project
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += @"\questions.json"; // Then adds the actual file name
            if (File.Exists(filePath))
            {
                Console.WriteLine("Fail 1");
                string json = File.ReadAllText(filePath);
                Console.WriteLine("Fail 2");
                QuestionList questionList = JsonConvert.DeserializeObject<QuestionList>(json);
                Console.WriteLine("Fail 3");
                if(topicId == 1)
                {
                    Console.WriteLine("Fail 4");
                    return questionList;
                }

                else
                {
                    Console.WriteLine("Fail 5");
                List<Question> filteredQuestions = questionList.Questions.FindAll(q => q.group == topicId);
                Console.WriteLine("Fail 6");
                return new QuestionList { Questions = filteredQuestions };
                }
            }
            return new QuestionList();
        }
    }
}