using System.Linq;

namespace ToDo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var manager = new Manager())
            {
                if (args.Any())
                    switch (args[0].ToLower())
                    {
                        case "add":
                        case "-a":
                            if (args.Count() == 2)
                                manager.AddNewItem(args[1]);
                            else
                                ToDoOutputWriter.OutputNotSupported();
                            break;

                        case "list":
                        case "-l":
                            if (args.Count() == 1)
                                manager.ListItems();
                            else
                                ToDoOutputWriter.OutputNotSupported();
                            break;
                        case "remove":
                        case "-r":
                            if (args.Count() == 2)
                                if (args[1] == "--all")
                                    manager.RemoveAll();
                                else
                                    manager.RemoveItem(args[1]);
                            else
                                ToDoOutputWriter.OutputNotSupported();
                            break;
                        case "help":
                        case "-h":
                            ToDoOutputWriter.OutputHelp();
                            break;
                        default:
                            ToDoOutputWriter.OutputNotSupported();
                            break;
                    }
                else
                    ToDoOutputWriter.Output("This is the command for managing your ToDo's, use argument help or -h for more info.");
            }
        }


    }
}
