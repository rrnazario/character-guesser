using CharacterGuesser.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterGuesser.Domain.Entities
{
    public class GameSimpler
    {
        public Tree QuestionTree { get; set; }

        public void Play()
        {
            while (true)
            {
                var node = QuestionTree.GetNextNode();

                node.Learn();
            }
        }
    }
}
