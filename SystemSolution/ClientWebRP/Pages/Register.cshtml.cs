using ClientWebRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientWebRP.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger _logger;

        [BindProperty]
        public RegisterDTO Register { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public RegisterModel(IHttpClientFactory httpClient, ILogger<RegisterModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = _httpClient.CreateClient("ClientWebRP");
            var response = await client.PostAsJsonAsync("api/Auth/Register", Register);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("AuthToken", token);
                    _logger.LogInformation("Register successful");
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                _logger.LogInformation("Register failed");
                ErrorMessage = "Invalid register attempt.";
            }
            return Page();
        }
    }
}
