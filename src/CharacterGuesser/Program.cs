using CharacterGuesser.Domain.Entities;
using CharacterGuesser.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CharacterGuesser.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameSimpler();
            //IGame game = new Game(new System.Globalization.CultureInfo("en-US"));

            game.Play();            
        }

    }
}
