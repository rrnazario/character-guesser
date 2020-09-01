using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterGuesser.Domain.Entities
{
    public class Question : QuestionNode
    {
        public override QuestionNode Learn()
        {
            return this;
        }
    }
}
