namespace TrelloManagmentSystem.Models
{
	public class Project:BaseModel
	{
        public Project()
        {
            Tasks = new List<Tasks>();
        }
        public string Title  { get; set; }
		public string Description  { get; set; }
		public string Statues  { get; set; }
		public DateTime DateCreated  { get; set; }
		public string UserId  { get; set; }
        public AppUser? User { get; set; }
        public ICollection<Tasks>? Tasks { get; set; }
    }
}
