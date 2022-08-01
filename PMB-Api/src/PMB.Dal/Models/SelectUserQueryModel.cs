namespace PMB.Dal.Models
{
    public class SelectUserQueryModel
    {
        public string Login { get; set; }
        
        public bool IncludeKeys { get; set; }
        
        public bool IncludeRoles { get; set; }
    }
}