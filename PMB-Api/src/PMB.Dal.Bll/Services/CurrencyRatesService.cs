using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PMB.Dal.Bll.Dtos;
using PMB.Integration.Currencies;
using PMB.Integration.Currencies.Binance;
using PMB.Integration.Currencies.Cbr;

namespace PMB.Dal.Bll.Services
{
    public class CurrencyRatesService
    {
        private const string CbrRates = nameof(CbrRates);
        
        private readonly CbrClient _cbrClient;
        private readonly BinanceClient _binanceClient;
        private readonly CurrencySettingsOptions _currencySettingsOptions;
        private readonly IMemoryCache _cache;
        
        public CurrencyRatesService(CbrClient cbrClient, 
            BinanceClient binanceClient, 
            IOptions<CurrencySettingsOptions> options, 
            IMemoryCache cache)
        {
            _cbrClient = cbrClient;
            _binanceClient = binanceClient;
            _cache = cache;
            _currencySettingsOptions = options.Value;
        }

        public async Task<RatesDto> GetRates(DateTimeOffset dt, CancellationToken token)
        {
            dt = dt.ToLocalTime();
            var cbrRates = await GetOrCreateRatesFromCache(dt, token).ConfigureAwait(false);
            
            var cryptoRates = await _binanceClient.GetCryptoCurrencyRates(token);

            return new RatesDto
            {
                CbrRates = cbrRates.Where(x => _currencySettingsOptions.Currencies.Contains(x.CharCode)).Select(x =>
                    new RatesDto.Rate
                    {
                        Name = x.Name,
                        Nominal = x.Nominal,
                        Value = x.Value,
                        CharCode = x.CharCode
                    }).ToArray(),
                CryptoRates = cryptoRates.Where(x => _currencySettingsOptions.CryptoCurrencies.Contains(x.Symbol)).Select(x =>
                    new RatesDto.CryptoRate
                    {
                        Price = x.Price,
                        Symbol = x.Symbol
                    }).ToArray()
            };
        }

        private async Task<Rate[]> GetOrCreateRatesFromCache(DateTimeOffset dt, CancellationToken token)
        {
            return await _cache.GetOrCreateAsync(CbrRates + $"_{dt:dd_MM_yyyy}", async entry =>
            {
                var cbrUpdateSettings = _currencySettingsOptions.CbrUpdate;

                var now = DateTimeOffset.Now;
                var nextDay = now.TimeOfDay > cbrUpdateSettings.Time ? 1 : 0;
                var newDate = DateTimeOffset.Now.AddDays(nextDay);
                
                entry.AbsoluteExpiration = new DateTimeOffset(newDate.Year, newDate.Month, newDate.Day,
                    cbrUpdateSettings.Time.Hours, cbrUpdateSettings.Time.Minutes, cbrUpdateSettings.Time.Seconds, 
                    cbrUpdateSettings.Offset);
                
                return await _cbrClient.GetCurrencyRates(dt, token);
            });
        }
    }
}