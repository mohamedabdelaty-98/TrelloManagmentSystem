namespace TrelloManagmentSystem.Models
{
	public class Project:BaseModel
	{
		public string Title  { get; set; }
		public string Description  { get; set; }
		public string Statues  { get; set; }
		public DateTime DateCreated  { get; set; }
		public int UserId  { get; set; }
 	}
}
