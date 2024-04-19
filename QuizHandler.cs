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

        public void BeginQuiz(int count)
        {
            // Get question set
            gradeQuiz.ResetQuizScore();
            GenerateQuestions generateQuestions = new GenerateQuestions();
            QuestionList questionList = generateQuestions.LoadQuizFromJson(menu.TopicId, count);

            int id = 1; // index to iterate and print to console.

            foreach (Question question in questionList.Questions)
            {
                WriteQuestionToConsole(question, id, generateQuestions);
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
                Console.WriteLine("\nCorrect!\n");
                Console.ResetColor();
            }
            else
            {
                gradeQuiz.IncrementQuestionCount();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nIncorrect. The correct answer is " + fullAnswer + "\n");
                Console.ResetColor();
            }
        }

        // Reusable method to write question text and answers to the console
        private void WriteQuestionToConsole(Question question, int id, GenerateQuestions generateQuestions)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Question #" + id + ": " + question.question);
            Console.ResetColor();

            // Shuffle the order of the list of possible answers
            // generateQuestions.Shuffle(question.answers);

            char letter = 'A';

            for (int i = 0; i < question.answers.Count; i++)
            {
                Console.WriteLine($"{(char)(letter + i)}. {question.answers[i]}");
            }
        }
    }
}
