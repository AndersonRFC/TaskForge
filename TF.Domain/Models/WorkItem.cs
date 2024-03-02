namespace TF.Domain.Models;

public class WorkItem
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; } = "";
	public DateTime CreatedAt { get; set; }
	public bool Completed { get; set; } = false;
	public int ItemOrder { get; set; }

	public override string ToString()
	{
		return Id + " " + Name + " [" + (Completed? "v" : " ").ToString() + "]" ;
	}
}
