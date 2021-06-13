using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace OnboardingGame.Models
{
    public class PlayerProfile
    {
        public PlayerProfile() { 
            
        }

        public void AddPoints(int points) {
            if (points > 0) {
                EXP += points;
            }
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int EXP { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime StartDate { get; set; }
        public string Title { get; set; }
    }
}
