using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace OnboardingGame.Models
{
    public class Achievement
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AchievementType { get; set; }
        public long CurrentAmount { get; set; }
        public long RequiredAmount { get; set; }
        public bool Status (){
            return (CurrentAmount >= RequiredAmount);
        }
    }
}
