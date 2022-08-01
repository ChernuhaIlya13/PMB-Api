using System;

namespace PMB.Models.PositiveModels
{
    public class Fork
    {
        public long Id { get; set; }

        public long ForkId { get; set; }

        public int UpdateCount { get; set; }
        
        public TimeSpan Lifetime { get; set; }

        public decimal Profit { get; set; }

        public Sport Sport { get; set; }

        public string BetType => FirstBet.BetType.ToString();

        public string Bookmakers => FirstBet.Bookmaker + "|" + SecondBet.Bookmaker;

        public string Teams => FirstBet.Teams + "|" +  SecondBet.Teams;

        public string Other {get; set; }

        public DateTimeOffset CreatedAt => DateTimeOffset.Now;

        public Bet FirstBet { get; set; }

        public Bet SecondBet { get; set; }
        
        public string CridId => K1 + K2;

        public string K1 { get; set; }

        public string K2 { get; set; }

        public string Elid { get; set; }
    }
}