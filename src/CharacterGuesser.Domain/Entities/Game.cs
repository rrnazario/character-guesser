using CharacterGuesser.Domain.Constants;
using CharacterGuesser.Domain.Factories;
using CharacterGuesser.Domain.Interfaces;
using CharacterGuesser.Infra.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace CharacterGuesser.Domain.Entities
{
    public class Game : IGame
    {
        /// <summary>
        /// Used at all game navigation.
        /// </summary>
        public IQuestionTree CurrentQuestion { get; set; }

        const string QuestionDatabase = "questions.txt";

        public Game()
        {
            Load();

            Reset();
        }

        public Game(CultureInfo cultureInfo) : base()
        {
            ResourceHelper.SetCulture(cultureInfo);
        }
        public void Reset()
        {
            //Reset the tree
            CurrentQuestion = CurrentQuestion.First();

            Console.Clear();
            Console.WriteLine(ResourceHelper.GetString("ThinkMessage"));
            Thread.Sleep(new TimeSpan(0, 0, 2));
        }

        public void Play()
        {
            //To control singular and plural on success message.
            bool moreThanOnceSuccess = false;

            //The game runs forever until user closes the window.
            while (true)
            {
                Console.Clear();

                //Print current question.
                Console.Write(CurrentQuestion);

                var key = Console.ReadKey();

                //Only accepting Yes or No
                if (key.Key != ConsoleKey.S && key.Key != ConsoleKey.N)
                    continue;

                if (key.Key == ConsoleKey.S)
                {
                    //If there are not childrens for current question, this is final answear
                    if (CurrentQuestion.IsLeaf())
                    {
                        Console.WriteLine($"\n{(!moreThanOnceSuccess ? MessageConstants.SUCCESS_MESSAGE_FIRST_TIME : MessageConstants.SUCCESS_MESSAGE)}");
                        Console.ReadKey();

                        moreThanOnceSuccess = true;

                        Reset();
                    }
                    else
                        CurrentQuestion = CurrentQuestion.GetPositiveQuestion();
                }
                else
                {
                    if (CurrentQuestion.IsLeaf())
                    {
                        Learn();
                        Reset();
                    }
                    else
                        CurrentQuestion = CurrentQuestion.GetNegativeQuestion();
                }
            }
        }

        public void Learn()
        {
            Console.Write($"\n\n{ResourceHelper.GetString("LearningWhat")} {MessageConstants.SUFIX_ENTER_MESSAGE}");

            var newPlate = ConsoleHelper.ReadWholeWord();

            Console.Write(string.Format(ResourceHelper.GetString("ThinkDiffers"), newPlate, CurrentQuestion.GetText()) + $"{MessageConstants.SUFIX_ENTER_MESSAGE}");
            var differenceBetweenLastPlate = ConsoleHelper.ReadWholeWord();

            //Creating new question
            IQuestionTree newPlateQuestion = QuestionFactory.CreateQuestion(differenceBetweenLastPlate, CurrentQuestion.GetParent());
            newPlateQuestion.SetPositiveQuestion(newPlate);

            //Setting old parent to point to this newest question
            if (CurrentQuestion.GetParent().GetPositiveQuestion().Equals(CurrentQuestion))
                CurrentQuestion.GetParent().SetPositiveQuestion(newPlateQuestion);
            else
                CurrentQuestion.GetParent().SetNegativeQuestion(newPlateQuestion);

            //The new node negative question must pointing to current question.
            newPlateQuestion.SetNegativeQuestion(CurrentQuestion);

            //Save to file
            Save();
        }

        public void Save()
        {
            var file = new List<string>();

            var saveNode = CurrentQuestion.First();
            file.Add($"{saveNode.GetText()}|{saveNode.GetPositiveQuestion()?.GetText()}|{saveNode.GetNegativeQuestion()?.GetText()}");

            var rightNode = saveNode.GetPositiveQuestion();
            while (rightNode != null)
            {
                file.Add($"{rightNode.GetText()}|{rightNode.GetPositiveQuestion()?.GetText()}|{rightNode.GetNegativeQuestion()?.GetText()}");
                rightNode = rightNode.GetPositiveQuestion();
            }

            var leftNode = saveNode.GetNegativeQuestion();
            while (leftNode != null)
            {
                file.Add($"{leftNode.GetText()}|{leftNode.GetPositiveQuestion()?.GetText()}|{leftNode.GetNegativeQuestion()?.GetText()}");
                leftNode = leftNode.GetNegativeQuestion();
            }

            File.WriteAllLines(QuestionDatabase, file);
        }

        public void Load()
        {
            if (File.Exists(QuestionDatabase))
            {
                var questions = File.ReadAllLines(QuestionDatabase);

                foreach (var question in questions)
                {
                    var splitted = question.Split('|');

                    string rootNode = splitted[0];
                    string yesNode = splitted.Length > 1 ? splitted[1] : string.Empty;
                    string noNode = splitted.Length > 2 ? splitted[2] : string.Empty;

                    var node = CurrentQuestion?.Find(rootNode);
                    if (node == null)
                    {
                        node = QuestionFactory.CreateQuestion(rootNode);
                        CurrentQuestion = node;
                    }

                    if (!string.IsNullOrEmpty(yesNode))
                        node.SetPositiveQuestion(yesNode);

                    if (!string.IsNullOrEmpty(noNode))
                        node.SetNegativeQuestion(noNode);
                }
            }
            else
            {
                CurrentQuestion = QuestionFactory.CreateQuestion("flyer");

                CurrentQuestion.SetPositiveQuestion("Thor");
                CurrentQuestion.SetNegativeQuestion("Hulk");
            }
        }
    }
}
