using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;



namespace csharpquiz
{
    public class JsonHandler
    {
        // Declares file path and sets it equal to root directory of project
        private string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        private string GetFilePath(string path)
        {
            // Navigates backwards to the correct directory in the project
            string newFilePath = Directory.GetParent(filePath).FullName;
            newFilePath = Directory.GetParent(Directory.GetParent(newFilePath).FullName).FullName;
            newFilePath += path; // Then adds the actual file name
            return newFilePath;
        }

        public QuestionList LoadQuizFromJson(int topicId, int count)
        {
            string filePath = GetFilePath(@"\questions.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                QuestionList questionList = JsonConvert.DeserializeObject<QuestionList>(json);

                if (topicId != 1)
                {
                    List<Question> filteredQuestions = questionList.Questions.FindAll(q => q.group == topicId);
                    questionList = new QuestionList { Questions = filteredQuestions };
                }
                FluffAndGroomTheQuestionList(questionList, count);
                return questionList;
            }
            return new QuestionList();
        }

        public TopicList LoadTopicsFromJson()
        {
            // Navigates backwards to the correct directory in the project
            string filePath = GetFilePath(@"\topics.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                TopicList topicList = JsonConvert.DeserializeObject<TopicList>(json);
                return topicList;
            }
            return new TopicList();
        }

        private void FluffAndGroomTheQuestionList(QuestionList questionList, int count)
        {
            // shuffle the order of the list
            Shuffle(questionList.Questions);

            // trim the list to the specified size
            while (questionList.Questions.Count > count)
            {
                questionList.Questions.RemoveAt(questionList.Questions.Count - 1);
            }

            // shuffle the possible answers for each question
            foreach (Question question in questionList.Questions)
            {
                Shuffle(question.answers);
            }
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

        public int QuestionCountByTopicList(int topicId)
        {
            int i = 0;
            QuestionList questionList = LoadQuizFromJson(topicId, 10000);

            foreach (Question question in questionList.Questions)
            {
                i++;
            }

            return i;
        }
    }
}