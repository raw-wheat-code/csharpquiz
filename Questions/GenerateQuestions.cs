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

        public QuestionList LoadQuizFromJson(int topicId, int count = 10)
        {
            // Navigates backwards to the correct directory in the project
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += @"\questions.json"; // Then adds the actual file name
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                QuestionList questionList = JsonConvert.DeserializeObject<QuestionList>(json);
                if (topicId == 1)
                {
                    return questionList;
                }

                else
                {
                    List<Question> filteredQuestions = questionList.Questions.FindAll(q => q.group == topicId);
                    return new QuestionList { Questions = filteredQuestions };
                }
            }
            return new QuestionList();
        }

        // Fisher-Yates shuffle algorithm
        public void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}