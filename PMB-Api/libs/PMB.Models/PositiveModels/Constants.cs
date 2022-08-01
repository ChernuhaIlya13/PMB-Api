using System.Collections.Generic;

namespace PMB.Models.PositiveModels
{
    public static class Constants
    {
        public static readonly List<string> WinMatch = new()
        {
            "1",
            "1x",
            "2",
            "2x",
            "п1",
            "п2",
            "12"
        };
        
        public static readonly List<string> TotalMatch = new ()
        {
            "тм",
            "тб"
        };
        
        public static readonly List<string> IndTotalMatch = new ()
        {
            "команда1 тб",
            "команда1 тм",
            "команда2 тб",
            "команда2 тм",
            "обе команды забьют",
            "хотя бы одна не забьет",
            "команда1 забьет",
            "команда2 забьет",
        };
        
        public static readonly List<string> ForaMatch = new ()
        {
            "ф1",
            "ф2",
            "без ничьей 1",
            "без ничьей 2",
            "против ф1",
            "против ф2",
        };
    }
}