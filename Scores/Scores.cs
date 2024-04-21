// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System;
using csharpquiz;

public class Score
{
    public int numberQuestions { get; set; }
    public int numberQuestionsCorrect { get; set; }
}

public static class ScoreHistory
{
    public static void SaveScore(int correctCount, int questionCount)
    {
        Score score = new Score();
        JsonHandler jsonHandler = new JsonHandler();
        Score jsonScore = jsonHandler.ReadScoreFromJson();

        score.numberQuestions = (questionCount + jsonScore.numberQuestions);
        score.numberQuestionsCorrect = (correctCount + jsonScore.numberQuestionsCorrect);
        jsonHandler.WriteScoreToJson(score);
    }

    public static void PrintRollingScore()
    {
        JsonHandler jsonHandler = new JsonHandler();
        Score jsonScore = jsonHandler.ReadScoreFromJson();
        if (jsonScore.numberQuestions > 0)
        {
            decimal score = ((decimal)(jsonScore.numberQuestionsCorrect / (decimal)jsonScore.numberQuestions)) * 100;
            Console.WriteLine($"Your lifetime score is: {score:F2}%");
        }
        else
        {
            Console.WriteLine("You have no score data.");
        }

    }

    public static void ClearScore()
    {
        JsonHandler jsonHandler = new JsonHandler();
        Score jsonScore = jsonHandler.ReadScoreFromJson();
        jsonScore.numberQuestions = 0;
        jsonScore.numberQuestionsCorrect = 0;
        jsonHandler.WriteScoreToJson(jsonScore);
    }

    public static void ScoreMenu()
    {
        bool isValid = false;
        Menu menu = new Menu();
        do
        {
            
            JsonHandler jsonHandler = new JsonHandler();
            Score jsonScore = jsonHandler.ReadScoreFromJson();
            Console.Clear();
            PrintRollingScore();
            Console.WriteLine("\nPlease choose an option.");
            if (jsonScore.numberQuestions > 0)
            {
                Console.WriteLine("1. Clear Scores");
            }
            Console.WriteLine("0. Return to main menu.");
            string userInput = Console.ReadLine();
            isValid = int.TryParse(userInput, out int selection);
            if (selection == 0)
            {
                menu.MenuOptions();

            }
            else if(selection == 1 && jsonScore.numberQuestions > 0)
            {
                ClearScore();
                Console.WriteLine("\nScores have been cleared.\nPress any key to return to the main menu.");
                Console.ReadKey();
                menu.MenuOptions();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                isValid = false;
            }


        }
        while (!isValid);




    }

}