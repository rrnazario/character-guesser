using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace CharacterGuesser.Infra.Helpers
{
    public static class ResourceHelper
    {
        private static ResourceManager manager;
        private static CultureInfo currentCulture = new CultureInfo("pt-BR");

        static ResourceHelper() => SetResource("CharacterGuesser.Infra.Resources.Messages");
        public static void SetResource(string assemblyName) => manager = new ResourceManager(assemblyName, Assembly.GetExecutingAssembly());
        public static void SetCulture(CultureInfo culture) => currentCulture = culture;
        public static string GetString(string name) => manager.GetString(name, currentCulture);
    }
}
