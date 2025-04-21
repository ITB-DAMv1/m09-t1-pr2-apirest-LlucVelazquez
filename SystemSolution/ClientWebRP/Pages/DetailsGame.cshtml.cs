using ClientWebRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientWebRP.Pages
{
    public class DetailsGameModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        [BindProperty]
        public GameDTO Game { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public DetailsGameModel(IHttpClientFactory httpClientFactory, ILogger<DetailsGameModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("GameApi");
            var response = await client.GetAsync($"api/Game/{id}");
            if (response.IsSuccessStatusCode)
            {
                Game = await response.Content.ReadFromJsonAsync<GameDTO>();
                return Page();
            }
            else
            {
                _logger.LogError("Error fetching game details: {StatusCode}", response.StatusCode);
                ErrorMessage = "Error fetching game details.";
                return Page();
            }
        }
        public async Task<IActionResult> OnPostVoteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("GameApi");
            var response = await client.PostAsJsonAsync($"api/Game/{id}/vote", Game);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                _logger.LogError("Error voting for game: {StatusCode}", response.StatusCode);
                ErrorMessage = "Error voting for game.";
                return Page();
            }
        }
    }
}
