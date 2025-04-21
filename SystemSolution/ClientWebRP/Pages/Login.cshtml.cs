using ClientWebRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientWebRP.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger _logger;
        [BindProperty]
        public LoginDTO Login { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public LoginModel(IHttpClientFactory httpClient, ILogger<LoginModel> logger)
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
            var client = _httpClient.CreateClient("GameApi");
            var response = await client.PostAsJsonAsync("api/Auth/login", Login);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("AuthToken", token);
                    _logger.LogInformation("Login susccesfull");
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                _logger.LogInformation("Login failed");
                ErrorMessage = "Invalid login attempt.";
            }

            return Page();
        }

    }
}
