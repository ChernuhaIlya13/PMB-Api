using System;

namespace PMB.Dal.Models.Dals
{
    public class V1ForkDal
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

        public string Other {get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public V1BetDal FirstBet { get; set; }
        
        public V1BetDal SecondBet { get; set; }
        
        public long FirstBetId { get; set; }
        
        public long SecondBetId { get; set; }

        public string CridId { get; set; }

        public string K1 { get; set; }

        public string K2 { get; set; }

        public string ElId { get; set; }
    }
}