using CharacterGuesser.Infra.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterGuesser.Domain.Entities
{
    public abstract class QuestionNode
    {
        public QuestionNode Yes { get; set; }
        public QuestionNode No { get; set; }
        public string Text { get; set; }
        public override string ToString() => string.Format(ResourceHelper.GetString("Questioning"), Text);
        public abstract QuestionNode Learn();
    }
}
