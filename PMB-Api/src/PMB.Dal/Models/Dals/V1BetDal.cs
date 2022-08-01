namespace PMB.Dal.Models.Dals
{
    public class V1BetDal
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