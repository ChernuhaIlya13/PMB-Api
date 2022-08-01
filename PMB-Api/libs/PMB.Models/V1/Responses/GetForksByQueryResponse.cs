using System;

namespace PMB.Models.V1.Responses
{
    public class GetForksByQueryResponse
    {
        public long TotalCount { get; set; }

        public Fork[] Forks { get; set; }
        
        public class Fork
        {
            public long Id { get; set; }

            public long ForkId { get; set; }

            public int UpdateCount { get; set; }

            public long Lifetime { get; set; }

            public decimal Profit { get; set; }

            public string Sport { get; set; }

            public string BetType { get; set; }

            public string Bookmakers { get; set; }

            public string Teams { get; set; }

            public string Other { get; set; }

            public DateTimeOffset CreatedAt { get; set; }

            public Bet FirstBet { get; set; }

            public Bet SecondBet { get; set; }

            public long FirstBetId { get; set; }

            public long SecondBetId { get; set; }

            public string CridId { get; set; }

            public string K1 { get; set; }

            public string K2 { get; set; }

            public string ElId { get; set; }

            public class Bet
            {
                public long Id { get; set; }

                public string Bookmaker { get; set; }

                public decimal Coefficient { get; set; }

                public string Direction { get; set; }

                public string BetType { get; set; }

                public string BetId { get; set; }

                public string Sport { get; set; }

                public double Parameter { get; set; }

                public string BetValue { get; set; }

                public int ForksCount { get; set; }

                public string EvId { get; set; }

                public string OtherData { get; set; }

                public string Teams { get; set; }

                public string MatchData { get; set; }

                public string Url { get; set; }

                public bool IsReq { get; set; }

                public bool IsInitiator { get; set; }
            }
        }
    }
}