using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingGame.Models
{
    public class ToDoList
    {
        public ToDoList() {
            Name = "";
            TaskItem = null;
        }

        public ToDoList(string name, List<TaskItem> items) {
            Name = name;
            TaskItem = items;
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }

        [OneToMany("ListID", CascadeOperations = CascadeOperation.All)]
        public List<TaskItem> TaskItem { get; set; }
    }
}
