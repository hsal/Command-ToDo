using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ToDo
{
    public class Manager : IDisposable
    {
        private static List<ToDoItem> _toDoList;

        public Manager()
        {
            Init();
        }

        public void RemoveAll()
        {
            _toDoList = null;
            ToDoOutputWriter.Output("All ToDo items are deleted!");
        }

        public void RemoveItem(string index)
        {
            if (_toDoList.Count == 0)
            {
                ToDoOutputWriter.OutputError("List is empty!");
            }
            int i;
            int.TryParse(index, out i);
            if (i > 0)
            {
                if (i <= _toDoList.Count)
                {
                    var title = _toDoList.Select(c => c.Title).ElementAt(i - 1);
                    _toDoList.RemoveAt(i - 1);
                    ToDoOutputWriter.Output(string.Format("Item '{0}' was deleted!", title));
                }
                else
                    ToDoOutputWriter.OutputError("You're trying to delete an item that does not exist!");
                return;
            }
            ToDoOutputWriter.OutputError("You didn't proivde a valid index for a ToDo item");
        }

        public void ListItems()
        {
            foreach (var toDoItem in _toDoList)
            {
                OutputToDoItem(toDoItem);
            }
        }

        private static void OutputToDoItem(ToDoItem toDoItem)
        {
            ToDoOutputWriter.Output(string.Format("{0}\t{1}", _toDoList.IndexOf(toDoItem) + 1, toDoItem.Title));
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
                return Path.Combine(appDataPath, toDoJsonFileName);
            }
        }

        public void AddNewItem(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                ToDoOutputWriter.Output("Task can not have an empty title!");
            }
            _toDoList.Add(new ToDoItem(title));
            ToDoOutputWriter.Output(string.Format("{0}\tCreated!", title));
        }

        private static void Close()
        {
            File.WriteAllText(ToDoJsonFilePath, JsonConvert.SerializeObject(_toDoList));
        }

        public void Dispose()
        {
            Close();
        }
    }
}
