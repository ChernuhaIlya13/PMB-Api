namespace PMB.Models.V1.Requests
{
    public class GetUserRequest
    {
        public bool IncludeKeys { get; set; }
        
        public bool IncludeRoles { get; set; }
    }
}