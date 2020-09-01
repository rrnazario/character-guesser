using CharacterGuesser.Domain.Constants;
using CharacterGuesser.Domain.Entities;
using CharacterGuesser.Domain.Interfaces;
using System.IO;

namespace CharacterGuesser.Domain.Factories
{
    /// <summary>
    /// Question creator. It can be based on some implementation, dependency injection, etc.
    /// </summary>
    public class QuestionFactory
    {
        public static IQuestionTree CreateQuestion(string text) => new QuestionTree(text);
        public static IQuestionTree CreateQuestion(string text, IQuestionTree parent) => new QuestionTree(text, parent);

        public static IQuestionTree LoadFromFile()
        {
            var path = GeneralConstants.QUESTION_DATABASE;

            if (!File.Exists(path)) return null;

            var questions = File.ReadAllLines(path);
            IQuestionTree node = null, current = null;

            foreach (var question in questions)
            {
                var splitted = question.Split('|');

                string rootNode = splitted[0];
                string yesNode = splitted.Length > 1 ? splitted[1] : string.Empty;
                string noNode = splitted.Length > 2 ? splitted[2] : string.Empty;
                
                node = current?.Find(rootNode);
                if (node == null)
                {
                    node = CreateQuestion(rootNode);
                    current = node;
                }

                if (!string.IsNullOrEmpty(yesNode))
                    node.SetPositiveQuestion(yesNode);

                if (!string.IsNullOrEmpty(noNode))
                    node.SetNegativeQuestion(noNode);
            }

            return current.First();
        }

        public static IQuestionTree LoadDefault()
        {
            var CurrentQuestion = CreateQuestion("flyer");

            CurrentQuestion.SetPositiveQuestion("Thor");
            CurrentQuestion.SetNegativeQuestion("Hulk");

            return CurrentQuestion.First();
        }

        /// <summary>
        /// Load questions from a stored place or default information.
        /// </summary>
        /// <returns></returns>
        public static IQuestionTree CreateTree() => LoadFromFile() ?? LoadDefault();
        
    }
}
