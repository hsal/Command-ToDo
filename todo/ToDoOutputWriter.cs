using System;

namespace ToDo
{
    public static class ToDoOutputWriter
    {
        public static void OutputError(string text)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("ERROR: {0}", text);
            Console.WriteLine();

            Console.ResetColor();
        }

        public static void OutputHelp()
        {
            Output("Help is not ready yet!");
        }

        public static void OutputNotSupported()
        {
            OutputError("Not supported function!");
        }

        public static void Output(string text)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(text);
            Console.WriteLine();

            Console.ResetColor();
        }
    }
}
