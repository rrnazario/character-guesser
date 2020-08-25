using CharacterGuesser.Domain.Entities;
using CharacterGuesser.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterGuesser.Domain.Factories
{
    /// <summary>
    /// Question creator. It can be based on some implementation, dependency injection, etc.
    /// </summary>
    public class QuestionFactory
    {
        public static IQuestionTree CreateQuestion(string text) => new QuestionTree(text);
        public static IQuestionTree CreateQuestion(string text, IQuestionTree parent) => new QuestionTree(text, parent);
    }
}
