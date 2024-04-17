using System;
using System.Collections.Generic;

namespace csharpquiz
{
    // Object that holds all the attributes of a question
    public class QuizHandler
    {
        private readonly Menu menu;
        GradeQuiz gradeQuiz = new GradeQuiz();
        string userInput;

        public QuizHandler(Menu menu)
        {
            this.menu = menu;
        }

        public void BeginQuiz()
        {
            // Get question set
            GenerateQuestions generateQuestions = new GenerateQuestions();
            QuestionList questionList = generateQuestions.LoadQuizFromJson(menu.TopicId);

            int id = 1; // index to iterate and print to console.

            foreach (Question question in questionList.Questions)
            {
                WriteQuestionToConsole(question, id);
                do
                {
                    userInput = Console.ReadLine();
                    menu.IsValid = InputValidation.AnswerValidator(question, userInput);
                    if (!menu.IsValid)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid Entry. ");
                        Console.ResetColor();
                    }
                }
                while (!menu.IsValid);

                EvaluateAnswer(question, userInput);

                id++;
            }
            gradeQuiz.GetScore();
        }

        private void EvaluateAnswer(Question question, string input)
        {
            char correctAnswerLetter = (char)('A' + question.answers.IndexOf(question.correct));
            string fullAnswer = correctAnswerLetter + ". " + question.correct;

            // Convert user input to lowercase for case-insensitive comparison
            string userInputLower = input.ToLower();
            string correctAnswerLower = question.correct.ToLower();
            string correctAnswerLetterLower = correctAnswerLetter.ToString().ToLower();
            string fullAnswerLower = fullAnswer.ToLower();

            // Check if the user input matches any of the expected formats
            if (userInputLower == correctAnswerLower ||
                userInputLower == correctAnswerLetterLower ||
                userInputLower == fullAnswerLower)
            {
                gradeQuiz.IncrementCorrectCount();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct!");
                Console.ResetColor();
            }
            else
            {
                gradeQuiz.IncrementQuestionCount();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect. The correct answer is " + fullAnswer);
                Console.ResetColor();
            }
        }

        // Reusable method to write question text and answers to the console
        private void WriteQuestionToConsole(Question question, int id)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Question #" + id + ": " + question.question);
            Console.ResetColor();

            // Shuffle the order of the list of possible answers
            Shuffle(question.answers);

            char letter = 'A';

            for (int i = 0; i < question.answers.Count; i++)
            {
                Console.WriteLine($"{(char)(letter + i)}. {question.answers[i]}");
            }
        }

        // Fisher-Yates shuffle algorithm
        public static void Shuffle<T>(List<T> list)
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
