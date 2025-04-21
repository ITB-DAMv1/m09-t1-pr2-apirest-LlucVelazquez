namespace ClientWebRP.Model
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DeveloperTeam { get; set; }
        public List<VoteDTO>? Votes { get; set; }
    }
}
