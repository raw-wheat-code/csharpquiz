using System;
using System.Collections.Generic;

namespace csharpquiz
{
    public class Menu
    {
        private readonly QuizHandler quizHandler;

        private string userInput;
        private int topicId;
        private bool isValid = false;
        private bool keepAlive = false;
        private List<string> countAnswers = ["5", "10", "20", "50"];
        private int count;

        public Menu()
        {
            quizHandler = new QuizHandler(this);
        }

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        public int TopicId
        {
            get { return topicId; }
            private set { topicId = value; }
        }

        public void Start()
        {
            Console.WriteLine("Would you like to take a quiz? (Y/N)");
            userInput = Console.ReadLine();

            if (HandleGeneralInput(userInput, "Y"))
            {
                do
                {
                    PrintTopics();
                    PrintQuestionCount();
                    quizHandler.BeginQuiz(count);
                    keepAlive = AnotherQuiz.QuizAgain();
                }
                while (keepAlive);
            }
            else
            {
                Console.WriteLine("Quiz terminated.");
            }
        }

        private bool HandleGeneralInput(string input, string expected)
        {
            if (input != null && input.ToLower() == expected.ToLower())
            {
                return true;
            }
            return false;
        }

        private void PrintTopics()
        {
            GenerateTopics generateTopics = new GenerateTopics();
            TopicList topics = generateTopics.LoadTopicsFromJson();

            do
            {
                Console.WriteLine("\nPlease select a question set:\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                foreach (Topic topic in topics.Topics)
                {
                    Console.WriteLine($"{topic.topicId}: {topic.topicName}");
                }
                var input = Console.ReadLine();
                Console.ResetColor();
                if (int.TryParse(input, out topicId))
                {
                    isValid = InputValidation.TopicValidator(topicId, topics);
                }
                else isValid = false;

                if (!isValid)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid Entry.");
                    Console.ResetColor();
                }
            }
            while (!isValid);
        }

        private void PrintQuestionCount()
        {
            char letter = 'A';
            do
            {
                Console.WriteLine("\nPlease choose the number of questions you'd like to answer:\n");

                for (int i = 0; i < countAnswers.Count; i++)
                {
                    Console.WriteLine($"{(char)(letter + i)}: {countAnswers[i]}");
                }
                userInput = Console.ReadLine();
            }
            while (InputValidation.QuestionCountValidator(countAnswers, userInput));

            if (char.IsLetter(userInput, 0))
            {
                // If the user answered with a letter
                int index = userInput[0] - 'A';
                int.TryParse(countAnswers[index], out count);
            }
            else
            {
                // If the user answered with the numeric value
                int.TryParse(userInput, out count);
            }
        }
    }
}
