namespace PMB.Models.V1.Responses
{
    public class CurrencyRatesResponse
    {
        public Rate[] CbrRates { get; set; }
        
        public CryptoRate[] CryptoRates { get; set; }
        
        public class Rate
        {
            public int Nominal { get; set; }
        
            public string Name { get; set; }
        
            public decimal Value { get; set; }
        
            public string CharCode { get; set; }
        }
        
        public class CryptoRate
        {
            public string Symbol { get; set; }
        
            public decimal Price { get; set; }
        }
    }
}