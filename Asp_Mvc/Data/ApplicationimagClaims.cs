using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Asp_Mvc.Data
{
    public class ApplicationimagClaims : UserClaimsPrincipalFactory<ImageEntity, IdentityRole>
    {


        private readonly ApplicationDbContext _context;

        public ApplicationimagClaims(ApplicationDbContext context, UserManager<ImageEntity> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _context = context;
        }


        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ImageEntity image)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(image);
            var profileImageEntity = await _context.Users.Include(x =>x.Id).FirstOrDefaultAsync(x => x.Id == image.UserId);

            claimsIdentity.AddClaim(new Claim("Id", image.UserId ?? ""));
           
            return claimsIdentity;
        }

    }
}
