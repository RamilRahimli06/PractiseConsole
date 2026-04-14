using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Helpers
{
    public static class MenuInputHelper
    {
        public static bool TryReadMenuChoice<TEnum>(string prompt, out TEnum choice)
            where TEnum : struct, Enum
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int value) && Enum.IsDefined(typeof(TEnum), value))
            {
                choice = (TEnum)Enum.ToObject(typeof(TEnum), value);
                return true;
            }

            choice = default;
            return false;
        }
    }
}
