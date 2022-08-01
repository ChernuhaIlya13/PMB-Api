using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMB.Integration.Currencies.Cbr
{
    public class CbrParser
    {
        private const string Valute = "Valute";
        
        public async Task<Rate[]> ParseCbrRates(Stream xmlStream, CancellationToken token)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            await using (xmlStream)
            {
                var xDoc = await XDocument.LoadAsync(xmlStream, LoadOptions.None, token);
                return xDoc.Root!.Elements(Valute).Select(x => new Rate
                {
                    Name = x.Element(nameof(Rate.Name))!.Value,
                    Nominal = Int32.Parse(x.Element(nameof(Rate.Nominal))!.Value),
#if DEVELOPMENT
                    Value = decimal.Parse(x.Element(nameof(Rate.Value))!.Value),
#else
                    Value = decimal.Parse(x.Element(nameof(Rate.Value))!.Value.Replace(",", ".")),
#endif
                    CharCode = x.Element(nameof(Rate.CharCode))!.Value
                }).ToArray();
            }
        }
    }
}