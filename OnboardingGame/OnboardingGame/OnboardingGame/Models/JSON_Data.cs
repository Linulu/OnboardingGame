using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingGame.Models
{
    public class JSON_Data
    {
        public List<TaskItem> TaskItems { get; set; }
        public List<ToDoList> ListItems { get; set; }
    }
}
