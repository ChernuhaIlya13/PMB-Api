namespace PMB.Dal.Bll.Dtos.TelegramUser
{
    public class TelegramUserDto
    {
        public long TelegramChatId { get; set; }
        
        public string BotKey { get; set; }
        
        public bool IsActive { get; set; }
        
        public string BotSettings { get; set; }
    }
}