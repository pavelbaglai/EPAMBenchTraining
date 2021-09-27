using System;
using System.Linq;

namespace XboxStatistics
{
    class Program
    {
        private static readonly MyXboxOneGames Xbox = new MyXboxOneGames();

        static void Main(string[] args)
        {
            //var gg = new XboxOneGetter();
            //var games = gg.GetMyGames();
            //foreach (var game in games.Titles)
            //{
            //    var gameDetails = gg.GetGameDetails(game.TitleId);
            //    var gameStats = gg.GetGameStats(game.TitleId);
            //    var gameAchievements = gg.GetGameAchievements(game.TitleId);
            //}

            Question("How many games do I have?", HowManyGamesDoIHave);
            Question("How many games have I completed?", HowManyGamesHaveICompleted);
            Question("How many days did I play?", HowManyDaysDidIPlay);
            Question("Which game did I play the most hours?", WhichGameDidIPlayTheMostHours);
            Question("How much Gamerscore do I have?", HowMuchGamescoreDoIHave);
            Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
            Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
            Question("List all of my games where I have earned a rare achievement", ListAllOfMyGamesWhereIHaveEarnedARareAchievement);
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
            return Xbox.MyGames.Length.ToString();
        }

        static string HowManyGamesHaveICompleted()
        {
            //HINT: you need to count the games where I reached the maximum Gamerscore
            return Xbox.MyGames.Count(g => g.CurrentGamerscore == g.MaxGamerscore).ToString();
        }

        static string HowManyDaysDidIPlay()
        {
            //HINT: there's a game stat property called MinutesPlayed, but be aware that it stores seconds!
            var sum = Xbox.GameStats
                          .SelectMany(s => s.Value)
                          .Where(s => s.Name == "MinutesPlayed")
                          .Select(s => int.TryParse(s.Value, out int res) ? res : 0)
                          .Sum();
            return TimeSpan.FromSeconds(sum).TotalDays.ToString();
        }

        static string WhichGameDidIPlayTheMostHours()
        {
            //HINT: there's a game stat property called MinutesPlayed, but be aware that it stores seconds!
            var stats = Xbox.MyGames.ToDictionary(g => g, g =>
            {
                if (!Xbox.GameStats.ContainsKey(g.TitleId)) return 0;
                var stat = Xbox.GameStats[g.TitleId].FirstOrDefault(s => s.Name == "MinutesPlayed");
                return int.TryParse(stat?.Value, out int res) ? res : 0;
            });
            var max = stats.OrderByDescending(s => s.Value).First();
            return $"{max.Key.Name} -> {Math.Floor(TimeSpan.FromSeconds(max.Value).TotalMinutes)} minutes";
        }

        static string HowMuchGamescoreDoIHave()
        {
            return Xbox.MyGames.Sum(g => g.CurrentGamerscore).ToString();
        }

        static string InWhichGameDidIUnlockMyLatestAchievement()
        {
            var latest = Xbox.MyGames.OrderByDescending(g => g.LastUnlock).First();
            return $"{latest.Name} on {latest.LastUnlock:yyyy-MM-dd HH:mm}";
        }

        static string ListAllOfMyStatisticsInBindingOfIsaac()
        {
            var issac = Xbox.MyGames.First(g => g.Name == "The Binding of Isaac: Rebirth");
            var stats = Xbox.GameStats[issac.TitleId].Select(s => $"{s.Name} = {s.Value}");
            return string.Join(Environment.NewLine, stats);
        }

        static string HowManyAchievementsDidIEarnPerYear()
        {
            //HINT: unlocked achievements have an "Achieved" progress state
            return string.Join(Environment.NewLine, Xbox.Achievements
                                                         .SelectMany(a => a.Value)
                                                         .Where(a => a.ProgressState == "Achieved")
                                                         .GroupBy(a => a.Progression.TimeUnlocked.Year)
                                                         .OrderBy(g => g.Key)
                                                         .Select(g => $"{g.Key}: {g.Count()}"));
        }

        static string ListAllOfMyGamesWhereIHaveEarnedARareAchievement()
        {
            //HINT: rare achievements have a rarity category called "Rare"
            var list = from g in Xbox.MyGames
                join a in Xbox.Achievements on g.TitleId equals a.Key
                where a.Value.Any(aa => aa.ProgressState == "Achieved" && aa.Rarity.CurrentCategory == "Rare")
                select g.Name;
            return string.Join(Environment.NewLine, list);
        }

        static string WhichIsMyRarestAchievement()
        {
            var r = (from g in Xbox.MyGames
                join a in Xbox.Achievements on g.TitleId equals a.Key
                let rarest = a.Value.Where(aa => aa.ProgressState == "Achieved").OrderBy(aa => aa.Rarity.CurrentPercentage).First()
                select new
                {
                    Game = g.Name,
                    AchievementName = rarest.Name,
                    Percentage = rarest.Rarity.CurrentPercentage
                }).OrderBy(a => a.Percentage)
                .First();

            return $"You are among the {r.Percentage}% of gamers who earned the \"{r.AchievementName}\" achievement in {r.Game}";
        }
    }
}