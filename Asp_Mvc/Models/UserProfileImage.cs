namespace Asp_Mvc.Models
{
    public class UserProfileImage
    {
        public string FileName { get; set; }
        public string FriendlyFileName => FileName.Split("_")[1];
    }
}
