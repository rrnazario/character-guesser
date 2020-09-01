using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace CharacterGuesser.Domain.Entities
{
    public class Tree
    {
        public QuestionNode Current { get; set; }

        public QuestionNode GetNextNode()
        {
            var node = Current;

            Console.Write(node);

            var key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.Y:
                    node = Current.Yes;
                    break;
                case ConsoleKey.N:
                    node = Current.No;
                    break;
                default:
                    break;
            }

            return node;
        }
    }
}
