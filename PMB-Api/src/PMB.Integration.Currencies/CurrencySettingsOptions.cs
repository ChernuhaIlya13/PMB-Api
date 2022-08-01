using System;

namespace PMB.Integration.Currencies
{
    public class CurrencySettingsOptions
    {
        public string[] Currencies { get; set; }
        
        public string[] CryptoCurrencies { get; set; }
        
        public CbrUpdateTimeSettings CbrUpdate { get; set; }
        
        public class CbrUpdateTimeSettings
        {
            public TimeSpan Time { get; set; }
            
            public TimeSpan Offset { get; set; }
        }
    }
}