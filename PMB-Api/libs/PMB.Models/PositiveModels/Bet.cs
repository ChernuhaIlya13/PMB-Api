namespace PMB.Models.PositiveModels
{
    public class Bet
    {
        public long Id { get; set; }
        
        public Bookmaker Bookmaker { get; set; }

        public decimal Coefficient { get; set; }

        public Direction Direction { get; set; }

        public BetType BetType { get; set; }

        public string BetId { get; set; }
    
        public Sport Sport { get; set; }

        public double Parameter { get; set; }

        public string BetValue { get; set; }

        public int ForksCount { get; set; }

        public string PositiveEvId { get; set; }
        
        public string EvId { get; set; }

        public string OtherData { get; set; }

        public string Teams { get; set; }

        public MatchDataInfo MatchData { get; set; }

        public string Url { get; set; }

        public bool IsReq { get; set; }

        public bool IsInitiator { get; set; }
        
        public class MatchDataInfo
        {
            public string Liga { get; set; }
            
            public string Score { get; set; }
            
            public string[] PreviousScores { get; set; }
            
            /// <summary>
            /// Если есть данные,какой сейчас период,тайм,четверть
            /// </summary>
            public string AdditionalData { get; set; }
            
            public string Time { get; set; }
        }
    }
}