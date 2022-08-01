namespace PMB.Dal.Models
{
    public class GetTelegramUsersQueryModel
    {
        public string[] BotKeys { get; set; }
        
        public bool? IsActive { get; set; }
    }
}