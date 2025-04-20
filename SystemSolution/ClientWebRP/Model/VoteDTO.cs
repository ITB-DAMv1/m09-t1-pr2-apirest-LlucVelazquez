namespace ClientWebRP.Model
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string UserName { get; set; }
        public int VoteValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
