using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ToDo
{
    class Program
    {
        private static List<ToDoItem> _toDoList;
        static void Main(string[] args)
        {
            if (args.Any())
                switch (args[0].ToLower())
                {
                    case "add":
                    case "-a":
                        if (args.Count() == 2)
                            AddNewItem(args[1]);
                        else
                            OutputNotSupported();
                        break;

                    case "list":
                    case "-l":
                        if (args.Count() == 1)
                            ListItems();
                        else
                            OutputNotSupported();
                        break;
                    case "remove":
                    case "-r":
                        if (args.Count() == 2)
                            RemoveItem(args[1]);
                        else
                            OutputNotSupported();
                        break;
                    default:
                        OutputNotSupported();
                        break;
                }
        }

        private static void OutputNotSupported()
        {
            OutputError("Not supported function!");
        }

        private static void Output(string text)
        {
            Console.WriteLine(text);
        }

        private static void RemoveItem(string index)
        {
            Init();
            if (_toDoList.Count == 0)
            {
                OutputError("List is empty!");
            }
            int i;
            int.TryParse(index, out i);
            if (i > 0)
            {
                if (i <= _toDoList.Count)
                {
                    var title = _toDoList.Select(c => c.Title).ElementAt(i - 1);
                    _toDoList.RemoveAt(i - 1);
                    Output(string.Format("Item '{0}' was deleted!", title));
                    Close();
                }
                else
                    OutputError("You're trying to delete an item that does not exist!");
                return;
            }
            OutputError("You didn't proivde a valid index for a ToDo item");
        }

        private static void OutputError(string text)
        {
            Output(string.Format("ERROR: {0}", text));
        }

        private static void ListItems()
        {
            Init();
            foreach (var toDoItem in _toDoList)
            {
                OutputToDoItem(toDoItem);
            }
        }

        private static void OutputToDoItem(ToDoItem toDoItem)
        {
            Output(string.Format("{0}\t{1}", _toDoList.IndexOf(toDoItem) + 1, toDoItem.Title));
        }

        private static void Init()
        {
            if (!File.Exists(ToDoJsonFilePath))
                File.Create(ToDoJsonFilePath);

            _toDoList = JsonConvert.DeserializeObject<List<ToDoItem>>(File.ReadAllText(ToDoJsonFilePath)) ?? new List<ToDoItem>();

        }

        private static string ToDoJsonFilePath
        {
            get
            {
                const string toDoJsonFileName = "todolist.json";
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return string.Format(Path.Combine(appDataPath, toDoJsonFileName));
            }
        }

        private static void AddNewItem(string title)
        {
            Init();
            if (string.IsNullOrEmpty(title))
            {
                Output("Task can not have an empty title!");
                Close();
            }
            _toDoList.Add(new ToDoItem(title));
            Output(string.Format("{0}\tCreated!", title));
            Close();
        }

        private static void Close()
        {
            File.WriteAllText(ToDoJsonFilePath, JsonConvert.SerializeObject(_toDoList));
        }

        public class ToDoItem
        {
            public ToDoItem(string title)
            {
                Title = title;
                Category = "Defalut";
                CreationDate = DateTime.Now;
                DueDate = DateTime.Now.AddDays(7);
                Priority = 1;
            }

            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime DueDate { get; set; }
            public string Category { get; set; }
            public int Priority { get; set; }
            public int Status { get; set; }
        }
    }
}
