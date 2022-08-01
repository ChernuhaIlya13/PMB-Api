using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using PMB.Models.PositiveModels;

namespace PMB.Integration.PositiveBet
{
    public static class PositiveParser
    {
        private static readonly HtmlParser Parser = new();

        private const string Csrf = "YII_CSRF_TOKEN";

        public static async Task<string> GetCsrfToken(string content)
        {
            var html = await FromString(content);
            return html.GetElementsByName(Csrf)?.FirstOrDefault()?.Attributes["value"]?.Value;
        }

        public static async Task<List<Fork>> GetForks(string content)
        {
            var html = await FromString(content);
            
            var forkTags = html.GetElementById("gridBets")?
                .Children?
                .FirstOrDefault()?
                .Children?
                .FirstOrDefault(x => x.TagName == "TBODY")?
                .Children?
                .Where(x => x.Id != "0")
                .Select(x =>
                {
                    var firstTd = x.GetFirst();
                    var secondTd = x.Children.Skip(1).FirstOrDefault();
                    var fourthTd = x.Children.Skip(3).FirstOrDefault();
                    var fifthTd = x.Children.Skip(4).FirstOrDefault();
                    
                    var thirteenthTd = x.Children.Skip(12).FirstOrDefault();
                    var fourteenthTd = x.Children.Skip(13).FirstOrDefault();
                    
                    var fifteenthTd = x.Children.Skip(14).FirstOrDefault();
                    var sixteenthTd = x.Children.Skip(15).FirstOrDefault();

                    var bookmakers = x.Children.Skip(2).FirstOrDefault();
                    var coeffs = x.Children.Skip(5).FirstOrDefault();
                    return new Fork
                    {
                        ForkId = long.Parse(x.Id),
                        Sport = firstTd.Children.Skip(2).First().Attributes.GetNamedItem("title").Value.ParseSport(),
                        Lifetime = ParseLifetime(firstTd.Children?.FirstOrDefault(t => t.TagName == "DIV")?.Children?.FirstOrDefault()?.TextContent),
                        Profit = decimal.Parse(secondTd!.Children.FirstOrDefault()!.TextContent.Replace(".",",")),
                        K1 = x.Children.Skip(20).FirstOrDefault()!.TextContent,
                        K2 = x.Children.Skip(21).FirstOrDefault()!.TextContent,
                        Elid = fifthTd?.GetFirst(2)?.Attributes?.GetNamedItem("href")?.Value.Split('/').Reverse().Skip(2).First(),
                        FirstBet = new Bet
                        {
                            Bookmaker = bookmakers.GetFirst().TextContent.ParseBookmaker(),
                            Coefficient = coeffs.GetFirst().Children.Any(el => el.Attributes.GetNamedItem("data-original-title").Value == "Инициатор") ? decimal.Parse(coeffs.GetFirst().Children.Skip(1).FirstOrDefault().TextContent.Replace(".",",")) : decimal.Parse(coeffs.GetFirst().ChildNodes[1].TextContent.Replace(".",",")),
                            Direction = coeffs.GetFirst(3).Attributes.GetNamedItem("alt").Value.ParseDirection(),
                            BetType = fifthTd!.Children.First().Children.First().TextContent.ToLower().ParseBetType(),
                            Sport = firstTd.Children.Skip(2).First().Children.First().Attributes.GetNamedItem("alt").Value.ParseSport(),
                            BetValue = fifthTd.Children.First().Children.First().TextContent,
                            ForksCount = int.Parse(coeffs!.Children.First().Children.Last().TextContent.Replace(".",",")),
                            PositiveEvId = fifteenthTd?.TextContent,
                            OtherData = thirteenthTd?.TextContent,
                            Teams = fourthTd?.Children?.Skip(1).FirstOrDefault()?.TextContent,
                            MatchData = ParseMatchData(TrimMatchData(fourthTd?.Children?.Skip(2).FirstOrDefault()?.TextContent),firstTd.Children.Skip(2).First().Children.First().Attributes.GetNamedItem("alt").Value.ParseSport()),
                            IsInitiator = coeffs.GetFirst().Children.Any(a => a.Attributes.GetNamedItem("data-original-title").Value == "Инициатор"),
                        },
                        SecondBet = new Bet
                        {
                            Bookmaker = bookmakers!.Children.Last().TextContent.ParseBookmaker(),
                            Coefficient = coeffs.Children.Last().Children.Any(el => el.Attributes.GetNamedItem("data-original-title").Value == "Инициатор") ? decimal.Parse(coeffs.Children.Last().Children.Skip(1).FirstOrDefault().TextContent.Replace(".",",")) : decimal.Parse(coeffs.Children.Last().ChildNodes[1].TextContent.Replace(".",",")),
                            Direction = coeffs.Children.Last().Children.First().Children.First().Attributes.GetNamedItem("src").Value.ParseDirection(),
                            BetType = fifthTd.Children.Last().Children.First().TextContent.ToLower().ParseBetType(),
                            Sport = firstTd.Children.Skip(2).First().Children.First().Attributes.GetNamedItem("alt").Value.ParseSport(),
                            BetValue = fifthTd.Children.Last().Children.First().TextContent,
                            ForksCount = int.Parse(coeffs.Children.Last().Children.Last().TextContent.Replace(".",",")),
                            PositiveEvId = sixteenthTd?.TextContent,
                            OtherData = fourteenthTd?.TextContent,
                            Teams = fourthTd?.Children?.Skip(5).FirstOrDefault()?.TextContent,
                            MatchData = ParseMatchData(TrimMatchData(fourthTd?.Children?.Last().TextContent),firstTd.Children.Skip(2).First().Children.First().Attributes.GetNamedItem("alt").Value.ParseSport()),
                            IsInitiator = coeffs.Children.Last().Children.Any(a => a.Attributes.GetNamedItem("data-original-title").Value == "Инициатор")
                        }
                        
                    };
                });
            
            return forkTags?.ToList();
        }


        public static async Task<int> GetFilterId(string content)
        {
            var html = await FromString(content);
            
            var filterTag = html?.GetElementById("ddFilters")?.Children?.FirstOrDefault();
            if (filterTag == null)
            {
                throw new ArgumentException("Ошибка. Фильтр отсутствует");
            }
            
            if (filterTag.TagName?.ToLower() != "option")
                throw new ArgumentException("Ошибка.Выбран не <option> ! ");
            if (!filterTag.HasAttribute("value"))
                throw new ArgumentException("Ошибка. Почему то нет фильтра!");
            
            if (!int.TryParse(filterTag.Attributes["value"].Value, NumberStyles.Any, CultureInfo.GetCultureInfo("ru-Ru"), out var result))
                throw new ArgumentException("Ошибка. В фильтре не число!");

            return result;
        }

        private static string TrimMatchData(string matchData) => matchData.Substring(2);

        private static TimeSpan ParseLifetime(string lifetime)
        {
            var minSecs = lifetime.Replace(" мин ", "\t").Replace(" сек", "").Split('\t');

            return minSecs.Length > 1
                ? new TimeSpan(0, int.Parse(minSecs.First()), Int32.Parse(minSecs.Last())) 
                : new TimeSpan(0, 0, int.Parse(minSecs.First()));
        }
        
        private static Task<IHtmlDocument> FromString(string html) => Parser.ParseDocumentAsync(html);

        private static IElement GetFirst(this IElement el, int count = 1)
        {
            for (var i = count; i > 0; i--)
            {
                el = el.Children.First();
            }

            return el;
        }

        private static (string additionalData, string time) GetTimeAndAdditionalData(string[] strArray)
        {
            var addData = "";
            var time = "";
            if (strArray.Length == 3 || strArray.Length == 4)
            {
                addData = strArray.First() +  " " + strArray.Skip(1).First();
                if (strArray.Length == 4)
                {
                    time = strArray.Skip(2).First() +  " " + strArray.Last();
                }
                else
                {
                    time = strArray.Last();
                }
            }

            if (strArray.Length == 2)
            {
                addData = strArray.First() + " " + strArray.Last();
            }

            return (addData, time);
        }

        private static Bet.MatchDataInfo ParseMatchData(string content,Sport sport)
        {
            try
            {
                var firstParts = content.Split(")");
                var liga = firstParts.First().Trim();

                string score = String.Empty;
                string[] previousScores = new string[0];
                string matchTime = firstParts.Last().Trim();

                var partialsMatchTime = matchTime.Replace(",", "").Trim().Split(" ");
                (string additionalData, string time) dataTime;

                if (firstParts.Length == 1)
                {
                    dataTime = GetTimeAndAdditionalData(partialsMatchTime);

                    return new Bet.MatchDataInfo
                    {
                        Liga = liga,
                        Score = score,
                        PreviousScores = previousScores,
                        AdditionalData = dataTime.additionalData,
                        Time = dataTime.time
                    };
                }

                if (firstParts.Length == 2)
                {
                    dataTime = GetTimeAndAdditionalData(partialsMatchTime);

                    score = firstParts.Skip(1).First().Split('(').First().Trim().Split(",").First();
                    if (score.Contains(","))
                    {
                        score = score.Split(",").First().Trim();
                    }

                    matchTime = firstParts.Skip(1).First().Split(",").Last().Trim().Replace(" ", "")
                        .Replace("nbsp", "");
                    if (matchTime.Contains(","))
                    {
                        matchTime = matchTime.Split(",").Last().Trim();
                    }

                    return new Bet.MatchDataInfo
                    {
                        Liga = liga,
                        Score = score,
                        PreviousScores = previousScores,
                        AdditionalData = dataTime.additionalData,
                        Time = dataTime.time
                    };
                }

                dataTime = GetTimeAndAdditionalData(partialsMatchTime);

                score = firstParts.Skip(1).First().Split('(').First().Trim();
                previousScores = firstParts.Skip(1).First()
                    .Split('(').Last()
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToArray();

                matchTime = firstParts.Skip(2).First().Trim(',', ' ').Replace(" ", "").Replace("nbsp", "");
                if (matchTime.Contains(","))
                {
                    matchTime = matchTime.Split(",").Last().Trim();
                }

                return new Bet.MatchDataInfo
                {
                    Liga = liga,
                    Score = score,
                    PreviousScores = previousScores,
                    AdditionalData = dataTime.additionalData,
                    Time = dataTime.time
                };
            }
            catch 
            {
                Console.WriteLine("Произошла ошибка при парсинге MatchData");
                Debug.WriteLine("Произошла ошибка при парсинге MatchData");
            }
            return null;
        }
    }
}