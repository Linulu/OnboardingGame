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
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey(typeof(ToDoList))]
        public int ListID { get; set; }

        public string StatusMessage {
            get {
                if (Status < 0)
                {
                    return "Not Started";
                }
                else if (Status > 0)
                {
                    return "Completed";
                }
                else if (Status == 0)
                {
                    return "Started";
                }
                return "No status set";
            }
        }
    }
}
