using System;

namespace ToDo
{
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

        public ToDoItem(string title, string category, int priority)
        {
            Title = title;
            Category = category;
            CreationDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(7);
            Priority = priority;
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
