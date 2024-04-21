using System;
using System.Collections.Generic;

namespace csharpquiz
{
    public class Menu
    {
        private readonly QuizHandler quizHandler;
        JsonHandler jsonHandler = new JsonHandler();

        private string userInput;
        private int topicId;
        private bool isValid = false;
        private bool keepAlive = false;
        private List<string> countAnswers = ["5", "10", "25", "50", "100"];
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

        public void MenuOptions()
        {
            bool isValid = false;
            do
            {
                Score score = jsonHandler.ReadScoreFromJson();
                int selection;
                
                Console.Clear();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Take a quiz.");
                Console.WriteLine("2. View your rolling score");
                Console.WriteLine("9. Exit");
                var userInput = Console.ReadLine();
                isValid = int.TryParse(userInput, out selection);
                switch(selection)
                    {
                        case 1: 
                        Start();
                        break;
                        case 2:
                        ScoreHistory.ScoreMenu();
                        break;
                        case 9:
                        Console.WriteLine("Ok, Goodbye.");
                        break;
                        default:
                        Console.WriteLine("Invalid entry.");
                        isValid = false;
                        break;
                    }

            }
            while(isValid == false);





        }


        public void Start()
        {


                    Console.Clear();
                    PrintTopics();
                    PrintQuestionCount();
                    quizHandler.BeginQuiz(count);
                    MenuOptions();


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
            TopicList topics = jsonHandler.LoadTopicsFromJson();

            do
            {
                Console.WriteLine("Please select a question set:\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                foreach (Topic topic in topics.Topics)
                {
                    Console.WriteLine($"{topic.topicId}: {topic.topicName}");
                }
                var input = Console.ReadLine();
                Console.Clear();
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
                    Console.Clear();
                    Console.ResetColor();
                }
            }
            while (!isValid);
        }

        private void PrintQuestionCount()
        {
            char letter = 'A';
            int max = jsonHandler.QuestionCountByTopicList(topicId);

            do
            {
                Console.WriteLine("Choose the number of questions:\n");

                for (int i = 0; i < countAnswers.Count; i++)
                {
                    if (Convert.ToInt32(countAnswers[i]) <= max)
                    {
                        Console.WriteLine($"{(char)(letter + i)}: {countAnswers[i]}");
                    }
                }
                userInput = Console.ReadLine();
                Console.Clear();
            }
            while (InputValidation.QuestionCountValidator(countAnswers, userInput));

            if (char.IsLetter(userInput, 0))
            {
                // If the user answered with a letter
                int index = (char.ToUpper(userInput[0]) - 64) - 1;
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
