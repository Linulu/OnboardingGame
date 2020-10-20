using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingGame.Models
{
    public class Catagory
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
