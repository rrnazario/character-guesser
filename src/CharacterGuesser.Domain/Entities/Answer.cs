using CharacterGuesser.Domain.Constants;
using CharacterGuesser.Infra.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterGuesser.Domain.Entities
{
    public class Answer : QuestionNode
    {
        public override QuestionNode Learn()
        {
            Console.Write($"\n\n{ResourceHelper.GetString("LearningWhat")} {MessageConstants.SUFIX_ENTER_MESSAGE}");

            var newChar = ConsoleHelper.ReadWholeWord();

            Console.Write(string.Format(ResourceHelper.GetString("ThinkDiffers"), newChar, this.Text) + $"{MessageConstants.SUFIX_ENTER_MESSAGE}");

            var differenceBetweenLastChar = ConsoleHelper.ReadWholeWord();

            var newQuestion = new Question() { Text =  differenceBetweenLastChar };
            var newAnswer = new Answer() { Text =  newChar };

            newQuestion.Yes = newAnswer;

            return newQuestion;
        }
    }
}
