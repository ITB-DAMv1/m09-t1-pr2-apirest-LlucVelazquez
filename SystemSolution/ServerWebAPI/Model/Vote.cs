using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerWebAPI.Model
{
	public class Vote
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int GameId { get; set; }
		public string UserId { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		[ForeignKey("GameId")]
		public Game Game { get; set; }

		[ForeignKey("UserId")]
		public ApplicationUser User { get; set; }

	}
}
