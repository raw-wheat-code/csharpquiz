using System;

namespace csharpquiz
{
    public class Menu
    {
        private readonly QuizHandler quizHandler;

        private string userInput;
        private int topicId;
        private bool isValid = false;
        private bool keepAlive = false;

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
                    Console.WriteLine("Let's get started!");
                    PrintTopics();
                    quizHandler.BeginQuiz();
                    keepAlive = AnotherQuiz.QuizAgain();
                }
                while (keepAlive);
            }
            else
            {
                Console.WriteLine("Ok, goodbye.");
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
    }
}
