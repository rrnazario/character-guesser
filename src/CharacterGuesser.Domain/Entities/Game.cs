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

        public Game() => Load();

        public Game(CultureInfo cultureInfo)
        {
            ResourceHelper.SetCulture(cultureInfo);

            Load();
        }

        public void Play()
        {
            ConsoleHelper.ResetScreen();
            
            //The game runs forever until user closes the window.
            while (true)
            {
                Console.Clear();

                //Print current question.
                Console.Write(CurrentQuestion);

                CurrentQuestion = CurrentQuestion.GetNextQuestion();
            }
        }

        private void Load()
        {
            if (File.Exists(GeneralConstants.QUESTION_DATABASE))
            {
                var questions = File.ReadAllLines(GeneralConstants.QUESTION_DATABASE);

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

            //Reset the tree
            CurrentQuestion = CurrentQuestion.First();
        }
    }
}
