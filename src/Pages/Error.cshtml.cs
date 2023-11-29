using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents the model for the error page.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        public string RequestId { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Gets a value indicating whether to show the request identifier.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        private readonly ILogger<ErrorModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the ErrorModel class.
        /// </summary>
        /// <param name="logger">The logger used for error logging.</param>
        public ErrorModel(ILogger<ErrorModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// This method is executed when the HTTP GET request is made to the error page.
        /// It sets the RequestId based on the current activity or trace identifier.
        /// </summary>
        public void OnGet()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Message = "The resource you're trying to find is either missing or moved.";
            if (request.Query.ContainsKey("errorLocation"))
            {
                var location = request.Query["errorLocation"];
                if(location.Contains("Read"))
                {
                    Message = "Invalid product selected for Read operation.";
                }
                if (location.Contains("Update"))
                {
                    Message = "Invalid product selected for Update operation.";
                }
                if (location.Contains("Delete"))
                {
                    Message = "Invalid product selected for Delete operation.";
                }
            }
        }
        /// <summary>
        /// This method redirects to the index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            return RedirectToPage("/Index");
        }
    }
}