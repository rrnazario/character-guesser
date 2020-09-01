using CharacterGuesser.Domain.Factories;
using CharacterGuesser.Domain.Interfaces;
using CharacterGuesser.Infra.Helpers;
using System;
using System.Globalization;

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
                Console.Write(CurrentQuestion);

                CurrentQuestion = CurrentQuestion.GetNextQuestion();
            }
        }

        private void Load() => CurrentQuestion = QuestionFactory.CreateTree();
    }
}
