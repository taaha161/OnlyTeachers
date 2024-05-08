using OT.Authorization;
using OT.Data;
using OT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OT.Areas.Identity.Data;

namespace OT.Pages.Contacts
{
    #region snippet

    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
            OTContext context,
            IAuthorizationService authorizationService,
            UserManager<OTUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Post> Post { get; set; }

        public async Task OnGetAsync()
        {
         
            var contacts = from c in Context.Post
                           select c;

            var isAuthorized = User.IsInRole("Manager") ||
                               User.IsInRole("Admin");

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                contacts = contacts.Where(c => c.Status == ContactStatus.Approved
                                            || c.OwnerID == currentUserId);
            }

            Post = await contacts.ToListAsync();
        }
    }
    #endregion
}