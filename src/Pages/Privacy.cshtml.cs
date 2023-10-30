using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents the model for the Privacy page.
    /// </summary>
    public class PrivacyModel : PageModel
    {
        /// <summary>
        /// Initializes a new instance of the PrivacyModel class.
        /// </summary>
        /// <param name="logger">The logger used for logging in the PrivacyModel.</param>
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// This method is executed when the HTTP GET request is made to the Privacy page.
        /// </summary>
        public void OnGet()
        {
            // Implementation for handling the GET request can be added here if needed.
        }
    }
}
