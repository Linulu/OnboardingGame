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
                    Name = "Slow and steady",
                    Description = "Earn enought hearts to reach level 2",
                    AchievementType = App.AchievementType.EXP,
                    CurrentAmount = (await App.Database.GetPlayerProfile()).EXP,
                    RequiredAmount = 5,
                    Status = false
                },
                new Achievement{
                    Name = "Welcome aboard!",
                    Description = "Reach your start date",
                    AchievementType = App.AchievementType.Date,
                    CurrentAmount = DateTime.Now.Ticks,
                    RequiredAmount = (await App.Database.GetPlayerProfile()).StartDate.Ticks,
                    Status = false
                },
                new Achievement{
                    Name = "Just getting started",
                    Description = "Finish a task in any list",
                    AchievementType = App.AchievementType.List,
                    CurrentAmount = await App.Database.GetAllDoneTasks(),
                    RequiredAmount = 1,
                    Status = false
                },
                new Achievement{
                    Name = "Half way there",
                    Description = "Finish half of all the available tasks",
                    AchievementType = App.AchievementType.List,
                    CurrentAmount = await App.Database.GetAllDoneTasks(),
                    RequiredAmount = ((await App.Database.GetTaskItem()).Count)/2,
                    Status = false
                },
                new Achievement{
                    Name = "True Completionist",
                    Description = "Finish all tasks within every list",
                    AchievementType = App.AchievementType.List,
                    CurrentAmount = await App.Database.GetAllDoneTasks(),
                    RequiredAmount = (await App.Database.GetTaskItem()).Count,
                    Status = false
                }
            };

            List<ToDoList> tList = await App.Database.GetToDoListAsync();
            foreach (ToDoList element in tList) {
                aList.Add(new Achievement
                {
                    Name = element.Name + ", Completionist",
                    Description = "Finish all tasks in the " + element.Name + " list",
                    AchievementType = App.AchievementType.List,
                    TargetID = element.ID,
                    CurrentAmount = await App.Database.GetAllDoneTasks(element.ID),
                    RequiredAmount = (await App.Database.GetTasksFromListAsync(element.ID)).Count,
                    Status = false
                });
            }

            return aList;
        }
    }
}
