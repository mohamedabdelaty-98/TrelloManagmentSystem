namespace TrelloManagmentSystem.Models
{
	public class Tasks:BaseModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Statues { get; set; }
		public DateTime DateCreated { get; set; }
		public Project Projects { get; set; } = null!;
	}
}
