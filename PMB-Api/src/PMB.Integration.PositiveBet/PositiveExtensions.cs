using System;
using System.Linq;
using PMB.Models.PositiveModels;

namespace PMB.Integration.PositiveBet
{
    public static class PositiveExtensions
    {
        
        
        public static Bookmaker ParseBookmaker(this string value)
        {
            var bName = "_" + value.ToLower().Replace(" ", "").Replace(".", "").Replace("-","");
            var result = Enum.TryParse<Bookmaker>(bName, true, out var bookmaker) ? bookmaker : Bookmaker.None;
            if (result == Bookmaker.None)
            {
                Console.WriteLine("Bookmaker_None");
                Console.WriteLine(bName);
            }
            return result;
        }

        public static Direction ParseDirection(this string value) =>
            Enum.TryParse<Direction>(value, true, out var direction) ? direction : Direction.None;

        public static Sport ParseSport(this string value) =>
            value.ToLower() switch
            {
                "бадминтон" => Sport.Badminton,
                "баскетбол" => Sport.Basketball,
                "бейсбол" => Sport.Baseball,
                "волейбол" => Sport.VolleyBall,
                "гандбол" => Sport.Handball,
                "настольный теннис" => Sport.TableTennis,
                "теннис" => Sport.Tennis,
                "футбол" => Sport.Football,
                "футзал" => Sport.Futsal,
                "хоккей" => Sport.Hockey,
                "киберспорт" => Sport.Cyber,
                "киберфутбол" => Sport.CyberFootball,
                _ => Sport.None
            };

        public static BetType ParseBetType(this string value) =>
            value switch
            {
                { } when WinInSet(value) => BetType.WinInSet,
                { } when WinInGame(value) => BetType.WinInGame,
                { } when WinPoints(value) => BetType.WinPoints,
                { } when Win(value) => BetType.Win,
                { } when IndTotal(value) => BetType.IndTotal,
                { } when Total(value) => BetType.Total,
                { } when Fora(value) => BetType.Fora,
                { } when Corner(value) => BetType.Corner,
                { } when Goals(value) => BetType.Goals,
                _ => BetType.None
            };

        private static bool Win(string value) =>
            Constants.WinMatch.Any(value.StartsWith);

        private static bool Total(string value) => 
            Constants.TotalMatch.Any(value.StartsWith);

        private static bool IndTotal(string value) =>
            Constants.IndTotalMatch.Any(value.StartsWith);
        
        private static bool Fora(string value) => 
            Constants.ForaMatch.Any(value.StartsWith);

        private static bool Corner(string value) =>
            value.Contains("угл");

        private static bool WinInSet(string value) =>
            value.Contains("п1") || value.Contains("п2") && value.Contains("сет");

        private static bool WinInGame(string value) =>
            value.Contains("п1") || value.Contains("п2") && value.Contains("гейм");

        private static bool WinPoints(string value) =>
             value.Contains("п1") || value.Contains("п2") && value.Contains("очко");
        //TODO:Победа в текущем гейме и победа в гейме не отличаются по виду ставки(нужно найти чем они отличаются и дописать)
        /*public static bool WinInCurrentGame(string value)
        {
            
            List<string> totalMatch = new List<string>()
            {
                
            };
            return InnerCheckTemplate(totalMatch, value);
        }*/

        private static bool Goals(string value) => 
            //TODO: Посмотреть как называются ставки на позитиве
            value.Contains("гол");
    }
}