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
            Output("usage: todo <command> [<args>]");
            Output();
            Output("The current supported commands are:");
            Output("add|-a\t\tAdd a new todo item.");
            Output("remove|-r\tRemove a todo item by providing the index of the item. Use --all to remove all items.");
            Output("list|-l\t\tList all the todo items.");
        }

        public static void OutputNotSupported()
        {
            OutputError("Not supported function!");
        }

        public static void Output(string text = "")
        {
            Console.WriteLine(text);
        }
    }
}
