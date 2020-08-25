using CharacterGuesser.Domain.Entities;
using CharacterGuesser.Domain.Interfaces;

namespace CharacterGuesser.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame game = new Game();

            game.Play();
        }
    }
}
