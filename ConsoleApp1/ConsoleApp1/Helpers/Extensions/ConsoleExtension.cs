using System;

namespace ConsoleApp1.Helpers.Extensions
{
    public static class ConsoleExtensions
    {
        public static void WriteColor(this string text, ConsoleColor color)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine(text);

            Console.ForegroundColor = old;
        }

        public static void WriteInfo(this string text)
            => text.WriteColor(ConsoleColor.Cyan);

        public static void WriteSuccess(this string text)
            => text.WriteColor(ConsoleColor.Green);

        public static void WriteWarning(this string text)
            => text.WriteColor(ConsoleColor.Yellow);

        public static void WriteError(this string text)
            => text.WriteColor(ConsoleColor.Red);

        public static void WriteTitle(this string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
