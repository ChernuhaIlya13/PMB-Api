using System.Collections.Generic;
using PMB.Models.BookmakersFilters;

namespace PMB.Models.Messages
{
    public class ForksFilterMessage
    {
        /// <summary>
        /// Пауза после успешной попытки проставить
        /// </summary>
        public int PauseAfterSuccessfulAttemptPutDown { get; set; }
        
        /// <summary>
        /// Пауза после неудачной попытки проставить
        /// </summary>
        public int PauseAfterUnsuccessfulAttemptPutDown { get; set; }
        
        /// <summary>
        /// ожидание перекрытия плеча
        /// </summary>
        public int WaitingShoulderOverlap { get; set; }
        
        /// <summary>
        /// Максимальный минус
        /// </summary>
        public int MaxMinus { get; set; }
        
        /// <summary>
        /// Количество вилок в одном событии 
        /// </summary>
        public int CountForksInOneEvent { get; set; }
        
        /// <summary>
        /// количество идентичных вилок в одном событии
        /// </summary>
        public int CountOfIdenticalSurebetsInOneEvent { get; set; }
        
        /// <summary>
        /// количество допустимых неперекрытых в одном событии
        /// </summary>
        public int CountAdmissibleNonOverlappedInOneEvent { get; set; }
        
        /// <summary>
        /// Доходность вилки
        /// </summary>
        public Range Profit { get; set; }
        
        /// <summary>
        /// Коэффициент вилки
        /// </summary>
        public Range Coefficient { get; set; }
        
        /// <summary>
        /// Время жизни вилки
        /// </summary>
        public Range TimeOfLife { get; set; }
        
        /// <summary>
        /// Умная проставка
        /// </summary>
        public SmartSpacer CleverStake { get; set; }
        
        /// <summary>
        /// Пропустить ставку,если максимально возможная ставка в конторе меньше чем ...
        /// </summary>
        public int MaxSumStakeInBk { get; set; }
        
        public record Range(int Start, int Finish);
        /// <summary>
        /// Выбирается случайное число в диапозоне умной проставки
        /// Либо процент от лимита на ставку,либо процент от баланса на БК
        /// </summary>
        
        public record SmartSpacer(int Start, int Finish, decimal PercentLimitOnStake) : Range(Start, Finish);
        /// <summary>
        /// Список букмекеров
        /// </summary>
       
        public List<Bookmaker> Bookmakers { get; set; }
        
        public record SequenceBookmakers(string First, string Second, int Id);
        
        ///
        /// Правила очередности
        ///
        public List<SequenceBookmakers> SequenceRulesBookmakers { get; set; }
    }
}