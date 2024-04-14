using System;
using System.Collections.Generic;


namespace csharpquiz
{
    public class AnotherQuiz
    {      public static string response = "";
           public static bool isValid = true;
        public static string QuizAgainQuestion()
        {

            do
            {
                Console.WriteLine("Would you like to take another quiz? (Y/N)");
                response = Console.ReadLine().ToLower();
                if(response != "y" && response != "n")
                {
                    isValid = false;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid Entry. ");
                    Console.ResetColor();
                }
                else
                {
                    isValid = true;
                }
            }
            while(!isValid);

            return response;

        }

    
        public static bool QuizAgain()
        {
           string response = QuizAgainQuestion();

            if (response == "y")
            {
                return true;
            }
            else
            {
                Console.WriteLine("OK, Goodbye!");
                return false;
            }
        }    
    
    
    }

 }
    
