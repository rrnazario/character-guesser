using System;
using System.Threading;

namespace CharacterGuesser.Infra.Helpers
{
    public class ConsoleHelper
    {
        /// <summary>
        /// Read a whole word typed on console
        /// </summary>
        /// <returns></returns>
        public static string ReadWholeWord() => Console.ReadLine();

        /// <summary>
        /// Clear screen data.
        /// </summary>
        public static void ResetScreen()
        {
            Console.Clear();
            Console.WriteLine(ResourceHelper.GetString("ThinkMessage"));
            Thread.Sleep(new TimeSpan(0, 0, 2));
        }
    }
}
