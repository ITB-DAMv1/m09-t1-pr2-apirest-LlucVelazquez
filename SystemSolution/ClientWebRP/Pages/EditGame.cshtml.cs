using ClientWebRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientWebRP.Pages
{
    public class EditGameModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger _logger;
        [BindProperty]
        public GameDTO Game { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public EditGameModel(IHttpClientFactory httpClient, ILogger<AddGameModel> logger)
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
            var token = HttpContext.Session.GetString("AuthToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsJsonAsync($"api/Games/{Game.Id}", Game);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Game updated successfully");
                return RedirectToPage("/Index");
            }
            else
            {
                _logger.LogInformation("Failed to update game");
                ErrorMessage = "Failed to update game.";
            }
            return Page();
        }
    }
}
