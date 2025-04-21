using ClientWebRP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text.Json;

namespace ClientWebRP.Pages
{
    public class DetailsGameModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        [BindProperty]
        public Game? Game { get; set; }
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
				var json = await response.Content.ReadAsStringAsync();
				Game = JsonSerializer.Deserialize<Game>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}
			else
            {
                _logger.LogError("Error fetching game details: {StatusCode}", response.StatusCode);
                ErrorMessage = "Error fetching game details.";
			}
			return Page();

		}
        public async Task<IActionResult> OnPostVoteAsync(int id)
        {
			var client = _httpClientFactory.CreateClient("ApiGameJam");
			var response = await client.GetAsync($"api/Games/{id}");

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				Game = JsonSerializer.Deserialize<Game>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				if (Game == null)
				{
					ErrorMessage = "No s'ha trobat el joc.";
				}
				return Page();
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				ErrorMessage = "No s'ha trobat el joc.";
				return Page();
			}
			else
			{
				ErrorMessage = $"Error carregant el joc: {response.StatusCode}";
				_logger.LogError(ErrorMessage);
				return Page();
			}
		}
    }
}
