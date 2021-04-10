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
            Title = "";
            Description = "";
            Points = 0;
            Status = -1;
        }

        public TaskItem(string name, string description, int points) {
            Title = name;
            Description = description;
            Points = points;
            Status = -1;
        }
        
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }

        [ForeignKey(typeof(ToDoList))]
        public int ListID { get; set; }

        public void UpdateStatus(int i) {
            if (i < 0)
            {
                Status = -1;
            }
            else if (i == 0)
            {
                Status = 0;
            }
            else if (i > 0) {
                Status = 1;
            }
        }
    }
}
