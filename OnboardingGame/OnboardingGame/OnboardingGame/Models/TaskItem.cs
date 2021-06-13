using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;

namespace OnboardingGame.Models
{
    public class TaskItem
    {
        public TaskItem() {
            title = "";
            description = "";
            points = 0;
            status = -1;
        }

        public TaskItem(string name, string description, int points) {
            title = name;
            this.description = description;
            this.points = points;
            status = -1;
        }
        
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int status { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int points { get; set; }

        [ForeignKey(typeof(ToDoList))]
        public int ListID { get; set; }

        public void UpdateStatus(int i) {
            if (i < 0)
            {
                status = -1;
            }
            else if (i == 0)
            {
                status = 0;
            }
            else if (i > 0) {
                status = 1;
            }
        }
    }
}
