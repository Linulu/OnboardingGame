using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingGame.Models
{
    public class ToDoList
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int EXP { get; set; }
        public string Name { get; set; }
        public bool RestrictionDate { get; set; }
    }
}
