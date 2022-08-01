using System;
using System.Collections.Generic;

namespace PMB.Abb.Models.Models
{
    public class AbbFork
    {
        public ArbsDto AbbDto { get; set; }
        
        public List<BetDto> Bets { get; set; }
    }
    
     public class ArbsDto 
    {
        /// <summary>
        /// Surebet ID/Айди ставки
        /// </summary>
        public string Id { get; set; }
    
        /// <summary>
        /// Event ID (reference)//Айди События
        /// </summary>
        public long EventId { get; set; }
    
        /// <summary>
        /// Surebet formula ID
        /// </summary>
        public int ArbFormulaId { get; set; }
    
        /// <summary>
        /// Surebet’s yield percentage
        /// </summary>
        public float Percent { get; set; }
    
        /// <summary>
        /// First outcome ID in the surebet formula
        /// </summary>
        public string Bet1Id { get; set; }
    
        /// <summary>
        /// Second outcome ID in the surebet formula
        /// </summary>
        public string Bet2Id { get; set; }
    
        /// <summary>
        /// Third outcome ID in the surebet formula (for 3way surebets)
        /// </summary>
        public string Bet3Id { get; set; }
    
        /// <summary>
        /// Surebet type (sport ID and outcome type, separated by a colon) (for example: arb_type=8:1)
        /// </summary>
        public string ArbType { get; set; }
    
        /// <summary>
        /// Minimum betting odds in the surebet
        /// </summary>
        public float MinCoefficient { get; set; }
    
        /// <summary>
        /// Maximum betting odds in the surebet
        /// </summary>
        public float MaxCoefficient { get; set; }
    
        /// <summary>
        /// Bit mask of event ID, sport ID and outcome ID in the surebet
        /// </summary>
        public int F { get; set; }
    
        /// <summary>
        /// ROI (return of investments)
        /// </summary>
        public long Roi { get; set; }
    
        /// <summary>
        /// Битовая маска ID букмекеров, участвующих в вилке
        /// </summary>
        public long[] BkIds { get; set; }
    
        /// <summary>
        /// Surebet generation time - Unix timestamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
    
        /// <summary>
        /// Last surebet update time - Unix timestamp
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    
        /// <summary>
        /// Match start time - Unix timestamp
        /// </summary>
        public DateTimeOffset StartedAt { get; set; }
    
        /// <summary>
        /// Middle value
        /// </summary>
        public float MiddleValue { get; set; }
    
        /// <summary>
        /// Corners score (for football)
        /// </summary>
        public string CornerScore { get; set; }

        public string GameScore { get; set; }
        
        /// <summary>
        /// League name (reference)
        /// </summary>
        public string League { get; set; }
    
        /// <summary>
        /// League ID (reference)
        /// </summary>
        public long LeagueId { get; set; }
    
        /// <summary>
        /// Sport IDа
        /// </summary>
        public int SportId { get; set; }
    
        /// <summary>
        /// Country ID for the event league
        /// </summary>
        public int CountryId { get; set; }
    
        /// <summary>
        /// true - Event in a break
        /// </summary>
        public bool Paused { get; set; }
    
        /// <summary>
        /// Home team ID (reference)
        /// </summary>
        public long HomeId { get; set; }
    
        /// <summary>
        /// Away team ID (reference)
        /// </summary>
        public long AwayId { get; set; }
    
        /// <summary>
        /// true - live event, false - prematch event
        /// </summary>
        public bool IsLive { get; set; }
    
        /// <summary>
        /// Home team name
        /// </summary>
        public string Home { get; set; }
    
        /// <summary>
        /// Away team name
        /// </summary>
        public string Away { get; set; }
    
        /// <summary>
        /// Reference event name
        /// </summary>
        public string Name { get; set; }
    
        public int? FId { get; set; }
        
        public IDictionary<string, object> AdditionalProperties { get; set; }
    }
     
    public class BetDto 
    {
        /// <summary>
        /// Home team name (on the bookmaker's website)
        /// </summary>
       public string Home { get; set; }
    
        /// <summary>
        /// Away team name (on the bookmaker's website)
        /// </summary>
       public string Away { get; set; }
    
        /// <summary>
        /// Start time of the match (on the bookmaker's website) - Unix timestamp
        /// </summary>
        public long StartedAt { get; set; }
    
        /// <summary>
        /// League name (on the bookmaker's website)
        /// </summary>
        public string League { get; set; }
    
        /// <summary>
        /// League ID (on the bookmaker's website
        /// </summary>
        public long BookmakerLeagueId { get; set; }
    
        /// <summary>
        /// Sport ID
        /// </summary>
        public long SportId { get; set; }
    
        /// <summary>
        /// Home team ID in the bookmaker's system
        /// </summary>
        public long HomeId { get; set; }
    
        /// <summary>
        /// Away team ID in the bookmaker's system
        /// </summary>
        public long AwayId { get; set; }
    
        /// <summary>
        /// Home team ID (reference)
        /// </summary>
        public long TeamHomeId { get; set; }
    
        /// <summary>
        /// Away team ID (reference)
        /// </summary>
        public long TeamAwayId { get; set; }
    
        /// <summary>
        /// League ID (reference)
        /// </summary>
        public long LeagueId { get; set; }
    
        /// <summary>
        /// Outcome update time - Unix timestamp
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    
        /// <summary>
        /// false - the order of teams on the bookmaker's website has been changed, true - the order of teams coincides with the reference event
        /// </summary>
        public bool SwapTeams { get; set; }
    
        /// <summary>
        /// Event name (on the bookmaker's website)
        /// </summary>
        public string BookmakerEventName { get; set; }
    
        /// <summary>
        /// Event name (reference)
        /// </summary>
        public string EventName { get; set; }
    
        /// <summary>
        /// Outcome ID
        /// </summary>
        public string Id { get; set; }
    
        /// <summary>
        /// Outcome odds
        /// </summary>
        public float Coefficient { get; set; }
    
        /// <summary>
        /// Direction of the odds change (0 - did not change, 1- increased, 2- decreased)
        /// </summary>
        public double Diff { get; set; }
    
        /// <summary>
        /// Event ID in the bookmaker's system
        /// </summary>
        public long BookmakerEventId { get; set; }
    
        /// <summary>
        /// Reference event ID
        /// </summary>
        public long EventId { get; set; }
    
        /// <summary>
        /// Bookmaker ID
        /// </summary>
        public int BookmakerId { get; set; }
    
        /// <summary>
        /// Period ID
        /// </summary>
        public int PeriodId { get; set; }
    
        /// <summary>
        /// Data for searching a bet on the bookmaker's website
        /// </summary>
        public string DirectLink { get; set; }
    
        /// <summary>
        /// LAY betting odds (if applicable)
        /// </summary>
        public float CoefficientLay { get; set; }
    
        /// <summary>
        /// 1 - Bet converted from LAY, 0 - regular bet
        /// </summary>
        public double IsLay { get; set; }
    
        /// <summary>
        /// Market Variations, described at this page https://www.betburger.com/api/entity_ids (Variations)
        /// </summary>
        public int MarketAndBetType { get; set; }
    
        /// <summary>
        /// Value for variation, if applicable (spreads and totals)
        /// </summary>
        public float MarketAndBetTypeParam { get; set; }
    
        /// <summary>
        /// Current event score at the bookmaker
        /// </summary>
        public string CurrentScore { get; set; }
    
        /// <summary>
        /// Data for searching an event on the bookmaker's website
        /// </summary>
        public string BookmakerEventDirectLink { get; set; }
        
        public IDictionary<string, object> AdditionalProperties { get; set; }
    }
}