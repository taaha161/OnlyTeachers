using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OT.Pages.Contacts
{
    public class SubscriptionModel : PageModel
    {
        [BindProperty]
        public string ErrorMessage { get; set; } // Property to hold error message

        public void OnGet()
        {
            // Initialize error message
            ErrorMessage = "";
        }

        public IActionResult OnPost(string stripeToken)
        {
            // Check if there's a problem with the credit card information
            if (string.IsNullOrEmpty(stripeToken))
            {
                // Set error message
                ErrorMessage = "There was an issue with your payment. Please check your card details and try again.";

                // Return PageResult with error message
                return Page();
            }

            // If everything is fine, continue with the subscription process
            // For example:
            // var stripeToken = Request.Form["stripeToken"];
            // Use the token to create a new subscription with Stripe API.

            // Redirect to a different page if the subscription is successful
            return RedirectToPage("/Contacts/Index");
        }
    }
}
