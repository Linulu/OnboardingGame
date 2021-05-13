using OnboardingGame.REST_Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingGame.Models
{
    public class JSON_Data
    {
        public string name { get; set; }
        public List<Task> tasks { get; set; }
    }
}