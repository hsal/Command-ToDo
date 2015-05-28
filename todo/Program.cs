using System.Linq;
using Fclp;

namespace ToDo
{
    class Program
    {
        private static int _priority;
        private static string _category;
        private static string _status;

        static void Main(string[] args)
        {
            using (var manager = new Manager())
            {
                if (args.Any())
                {
                    ParseArguments(args);
                    switch (args[0].ToLower())
                    {
                        case "add":
                        case "a":
                            if (args.Count() > 2 && !args[1].StartsWith("-"))
                                manager.AddNewItem(args[1], _category, _priority);

                            else
                                ToDoOutputWriter.OutputNotSupported();
                            break;

                        case "list":
                        case "l":
                            if (args.Count() == 1)
                                manager.ListItems();
                            else
                                ToDoOutputWriter.OutputNotSupported();
                            break;
                        case "remove":
                        case "r":
                            if (args.Count() == 2)
                                if (args[1] == "--all")
                                    manager.RemoveAll();
                                else
                                    manager.RemoveItem(args[1]);
                            else
                                ToDoOutputWriter.OutputNotSupported();
                            break;
                        case "help":
                        case "h":
                        case "?":
                            ToDoOutputWriter.OutputHelp();
                            break;
                        default:
                            ToDoOutputWriter.OutputNotSupported();
                            break;
                    }
                }
                else
                    ToDoOutputWriter.Output(
                        "This is the command for managing your ToDo's, use argument help or -h for more info.");
            }
        }

        static void ParseArguments(string[] args)
        {
            var fluentCommandLineParser = new FluentCommandLineParser();

            fluentCommandLineParser.Setup<int>('p')
                .Callback(p => _priority = p);

            fluentCommandLineParser.Setup<string>('c')
                .Callback(c => _category = c);

            fluentCommandLineParser.Setup<string>('s')
                .Callback(s => _status = s);

            fluentCommandLineParser.Parse(args);
        }
    }
}
