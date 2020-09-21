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
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public int Status { get; set; }
        public string Description { get; set; }

        [ForeignKey(typeof(ToDoList))]
        public int ListID { get; set; }

        public const int NOT_STARTED = -1;
        public const int STARTED = 0;
        public const int COMPLETED = 1;
    }
}
