using Asp_Mvc.Data;
using Asp_Mvc.Models;

namespace Asp_Mvc.Views.ViewModels
{
    public class ProfileViewModel
    {
        public EditProfileForm profileForm { get; set; }
         public ApplicationUserAddress Address { get; set; }
          public ApplicationUser User { get; set; }
        public UserProfileForm  userProfileForm { get; set; }
        public ImageModel imagmodel { get; set; }
         public UserProfileImage profileImage { get; set; }

    }
}
