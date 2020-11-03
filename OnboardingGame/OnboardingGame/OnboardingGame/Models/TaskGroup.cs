using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingGame.Models
{
    public class TaskGroup : List<TaskItem>
    {
        public string Name { get; private set; }

        public TaskGroup(string name, List<TaskItem> items) : base(items) {
            Name = name;
        }
    }
}
