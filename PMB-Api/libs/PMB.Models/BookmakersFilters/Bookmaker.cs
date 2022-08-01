using System;

namespace PMB.Models.BookmakersFilters
{
    public class Bookmaker
    {
        /// <summary>
        /// Выбран ли букмекер пользователем
        /// </summary>
        public bool IsActive { get; set; }
        
        public BrowserOptions BrowserOptions { get; set; }
        /// <summary>
        /// Имя букмекера
        /// </summary>
        public string BookmakerName { get; set; }

        /// <summary>
        /// Адрес зеркала
        /// </summary>
        public string Uri { get; set; }
        
        /// <summary>
        /// Валюта
        /// </summary>
        public (string Currency, string RoundRule, bool UseCommonRuleRound) Currency { get; set; }
        
        /// <summary>
        /// Прибыль вилки
        /// </summary>
        public (int Start, int Finish, bool UseCommonRuleRound) ProfitFork { get; set; }
        
        /// <summary>
        /// Коэффициент вилки
        /// </summary>
        public (int Start, int Finish, bool UseCommonRuleRound) CoefficientFork { get; set; }
        
        /// <summary>
        /// Время жизни вилки
        /// </summary>
        public (int Start, int Finish, bool UseCommonRuleRound) TimeOfLifeFork { get; set; }
        
        /// <summary>
        /// Пауза после успешной попытки проставить
        /// </summary>
        public (int Start,int Finish, bool UseCommonRuleRound) PauseAfterSuccessfulAttemptPutDown { get; set; }
        
        /// <summary>
        /// Количество вилок в одном событии 
        /// </summary>
        public (int CountForks, bool UseCommonRuleRound) CountForksInOneEvent { get; set; }
        
        /// <summary>
        /// количество идентичных вилок в одном событии
        /// </summary>
        public (int CountForks, bool UseCommonRuleRound) CountOfIdenticalSurebetsInOneEvent { get; set; }
        
        /// <summary>
        /// Остановка бота при балансе на бк
        /// </summary>
        public (int Start, int Finish) StopBotBalance { get; set; }
        
        /// <summary>
        /// Использовать ограничения сумм
        /// </summary>
        public (int PutStakeLess, int PutStakeMore, bool UseCommonRuleRound) RestrictSum { get; set; }
        
        /// <summary>
        /// Проверка минимумов и максимумов в букмекерке
        /// </summary>
        public bool CheckMaxMin { get; set; }
        
        /// <summary>
        /// Ограничение инициатора
        /// </summary>
        public string RestrictInitiator { get; set; }
        
        /// <summary>
        /// Умное замедление
        /// </summary>
        public bool CleverSlowing { get; set; }
    }
}