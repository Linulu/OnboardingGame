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
            this.name = "";
            tasks = null;
        }

        public ToDoList(string name, List<TaskItem> items) {
            this.name = name;
            tasks = items;
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }

        [OneToMany("ListID", CascadeOperations = CascadeOperation.All)]
        public List<TaskItem> tasks { get; set; }
    }
}
