﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerWebAPI.Model
{
	public class Game
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string DeveloperTeam { get; set; }
		public List<Vote>? Votes { get; set; }
	}
}
