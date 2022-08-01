namespace PMB.Dal.Models.Dals
{
    public class TelegramUserDal
    {
        public long TelegramChatId { get; set; }
        
        public string BotKey { get; set; }
        
        public bool IsActive { get; set; }
        
        public string BotSettings { get; set; }
    }
}