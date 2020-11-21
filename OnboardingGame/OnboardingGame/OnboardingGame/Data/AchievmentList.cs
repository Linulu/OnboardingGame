using System;
using System.Collections.Generic;
using System.Text;
using OnboardingGame.Models;
using System.Threading.Tasks;

namespace OnboardingGame.Data
{
    public class AchievmentList
    {
        public static async Task<List<Achievement>> GetAchievementsAsync()
        {
            List<Achievement> aList = new List<Achievement> {
                new Achievement{
                    Name = "Reach Level 2",
                    Description = "Reach Level 2",
                    CurrentAmount = (await App.Database.GetPlayerProfile()).EXP,
                    RequiredAmount = 5
                },
                new Achievement{
                    Name = "Start at Phoniro",
                    Description = "Start at Phoniro",
                    CurrentAmount = DateTime.Now.Ticks,
                    RequiredAmount = (await App.Database.GetPlayerProfile()).StartDate.Ticks
                },
                new Achievement{
                    Name = "Finish a task",
                    Description = "Finish a task in any list",
                    CurrentAmount = await App.Database.GetAllDoneTasks(),
                    RequiredAmount = 1
                }
            };

            /*
            List<ToDoList> list = await App.Database.GetToDoListAsync();

            foreach (ToDoList i in list) {
                int nrOfTasks = (await App.Database.GetTasksFromListAsync(i.ID)).Count;
                aList.Add(new Achievement
                {
                    Name = "Finished a task in " + i.Name,
                    Description = "Finish at least one task in " + i.Name + " list.",
                    AchievementType = (int)App.AchievementType.List,
                    TargetID = i.ID,
                    CurrentAmount = 0,
                    RequiredAmount = 1,
                    Status = false
                });
                aList.Add(new Achievement
                {
                    Name = "Completly finish " + i.Name,
                    Description = "Completely finish all tasks in " + i.Name + " list.",
                    AchievementType = (int)App.AchievementType.List,
                    TargetID = i.ID,
                    CurrentAmount = 0,
                    RequiredAmount = nrOfTasks,
                    Status = false
                });
            }

            int pID = (await App.Database.GetPlayerProfile()).ID;
            long ticks = (await App.Database.GetPlayerProfile()).StartDate.Ticks;
            aList.Add(new Achievement
            {
                Name = "Reach Level 2",
                Description = "Earn enought hearts to reach Level 2",
                AchievementType = (int)App.AchievementType.PlayerEXP,
                TargetID = pID,
                CurrentAmount = 0,
                RequiredAmount = 5,
                Status = false
            });
            aList.Add(new Achievement
            {
                Name = "Start at Phoniro",
                Description = "Start at Phoniro",
                AchievementType = (int)App.AchievementType.Date,
                TargetID = pID,
                CurrentAmount = 0,
                RequiredAmount = ticks,
                Status = false
            });
            */
            return aList;
        }
    }
}
