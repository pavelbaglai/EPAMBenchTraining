using System;
using System.Linq;

namespace XboxStatistics
{
    class Program
    {
        private static readonly MyXboxOneGames Xbox = new MyXboxOneGames();

        static void Main(string[] args)
        {
            Question("How many games do I have?", HowManyGamesDoIHave);
            Question("How many games have I completed?", HowManyGamesHaveICompleted);
            Question("How much Gamerscore do I have?", HowMuchGamescoreDoIHave);
            Question("How many days did I play?", HowManyDaysDidIPlay);
            Question("Which game have I spent the most hours playing?", WhichGameHaveISpentTheMostHoursPlaying);
            Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
            Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
            Question("List all of my games where I have earned a rare achievement", ListAllOfMyGamesWhereIHaveEarnedARareAchievement);
            Question("List the top 3 games where I have earned the most rare achievements", ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements);
            Question("Which is my rarest achievement?", WhichIsMyRarestAchievement);

            Console.ReadLine();
        }

        static void Question(string question, Func<string> answer)
        {
            Console.WriteLine($"Q: {question}");
            Console.WriteLine($"A: {answer()}");
            Console.WriteLine();
        }

        static string HowManyGamesDoIHave()
        {
            return Xbox.MyGames.Count().ToString();
        }

        static string HowManyGamesHaveICompleted()
        {
            //HINT: you need to count the games where I reached the maximum Gamerscore
            return Xbox.MyGames.Count(a => a.CurrentGamerscore == a.MaxGamerscore).ToString();
        }

        static string HowManyDaysDidIPlay()
        {
            //HINT: there's a game stat  property called MinutesPlayed, and as the name suggests it stored total minutes
            return ((double)Xbox.GameStats.Values.Sum(sum => sum
            .Any(a => a.Name == "MinutesPlayed" && !string.IsNullOrEmpty(a.Value)) ? 
            double.Parse(sum.First(a => a.Name == "MinutesPlayed").Value) : 0) / 1440).ToString("0.##");
        }

        static string WhichGameHaveISpentTheMostHoursPlaying()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            return Xbox.GameStats.OrderByDescending(o => o.Value
            .Any(a => a.Name == "MinutesPlayed" && !string.IsNullOrEmpty(a.Value)) 
            ? int.Parse(o.Value.First(a => a.Name == "MinutesPlayed").Value) : 0)
                .Select(s => Xbox.MyGames.First(a => a.TitleId == s.Key).Name + 
                " -> " + (int.Parse(s.Value.First(a => a.Name == "MinutesPlayed").Value) / 60).ToString() + " hours").First();
        }

        static string HowMuchGamescoreDoIHave()
        {
            return Xbox.MyGames.Sum(s => s.CurrentGamerscore).ToString() + "G";
        }

        static string InWhichGameDidIUnlockMyLatestAchievement()
        {
            return Xbox.MyGames.OrderByDescending(o => o.LastUnlock).Select(s => s.Name + " on " + s.LastUnlock.ToString("yyyy-MM-dd HH:mm")).First();
        }

        static string ListAllOfMyStatisticsInBindingOfIsaac()
        {
            return string.Join(Environment.NewLine, Xbox.GameStats[Xbox.MyGames.First(a=>a.Name.ToLower().StartsWith("the binding of isaac")).TitleId]
                .Select(s => $"{s.Name} = {s.Value}"));
        }

        static string HowManyAchievementsDidIEarnPerYear()
        {
            //HINT: unlocked achievements have an "Achieved" progress state
            return string.Join(Environment.NewLine, Xbox.Achievements.Values
                .SelectMany(s => s.Select(ss => new { ss.Progression.TimeUnlocked.Year, ss.ProgressState }))
                .Where(a => a.ProgressState == "Achieved")
                .GroupBy(g => g.Year).OrderBy(o=>o.Key).Select(s => $"{s.Key}: {s.Count()}"));
        }

        static string ListAllOfMyGamesWhereIHaveEarnedARareAchievement()
        {
            //HINT: rare achievements have a rarity category called "Rare"
            return string.Join(Environment.NewLine, Xbox.Achievements
                .Where(a => a.Value.Any(aa => aa.Rarity.CurrentCategory == "Rare" && aa.ProgressState == "Achieved"))
                .Select(s => Xbox.MyGames.First(a => a.TitleId == s.Key).Name).OrderBy(o=>o));
        }

        static string ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements()
        {
            return string.Join(Environment.NewLine, Xbox.Achievements
                .Select(s => new {
                    Xbox.MyGames.First(a => a.TitleId == s.Key).Name,
                    Sum = s.Value.Count(a => a.Rarity.CurrentCategory == "Rare" && a.ProgressState == "Achieved") })
                .OrderByDescending(o => o.Sum).Select(s=> $"{s.Name}: ({s.Sum})").Take(3));
        }

        static string WhichIsMyRarestAchievement()
        {
            return Xbox.Achievements.Select(s => new
            {
                Xbox.MyGames.First(a => a.TitleId == s.Key).Name,
                RarestAchievement = s.Value.Where(a => a.ProgressState == "Achieved")
                .Select(ss => new { ss.Name, ss.Rarity.CurrentPercentage }).OrderBy(o => o.CurrentPercentage).FirstOrDefault()
            }).Where(a=>a.RarestAchievement != null)
                .OrderBy(o => o.RarestAchievement.CurrentPercentage)
                .Select(s => $"You are among the {s.RarestAchievement.CurrentPercentage}% of gamers who earned the \"{s.RarestAchievement.Name}\" in {s.Name}")
                .First();
        }
    }
}