using OT.Authorization;
using OT.Data;
using OT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using OT.Areas.Identity.Data;
using OT.Data;
using OT.Models;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Authorization;

namespace OT.Pages.Contacts
{
    [Authorize]
    #region snippet
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
            OTContext context,
            IAuthorizationService authorizationService,
            UserManager<OTUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await Context.Post.FirstOrDefaultAsync(
                                                 m => m.Id == id);

            if (Post == null)
            {
                return NotFound();
            }
 var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Post,
                                                      ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Contact from DB to get OwnerID.
            var contact = await Context
                .Post.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contact == null)
            {
                return NotFound();
            }
 var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, contact,
                                                      ContactOperations.Update);


            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Post.OwnerID = contact.OwnerID;

            Context.Attach(Post).State = EntityState.Modified;

            if (Post.Status == ContactStatus.Approved)
            {
                // If the contact is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
var canApprove = await AuthorizationService.AuthorizeAsync(User, Post, ContactOperations.Approve);


                if (!canApprove.Succeeded)
                {
                    Post.Status = ContactStatus.Submitted;
                }
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
    #endregion
}
