using System;

namespace CharacterGuesser.Infra.Helpers
{
    public class ConsoleHelper
    {
        /// <summary>
        /// Read a whole word typed on console
        /// </summary>
        /// <returns></returns>
        public static string ReadWholeWord() => Console.ReadLine();
    }
}
