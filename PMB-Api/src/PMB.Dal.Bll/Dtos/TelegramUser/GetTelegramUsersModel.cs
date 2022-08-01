namespace PMB.Dal.Bll.Dtos.TelegramUser
{
    public class GetTelegramUsersModel
    {
        public string[] BotKeys { get; set; }
        
        public bool? IsActive { get; set; }
    }
}