using FirebirdSql.Data.FirebirdClient;
using TF.Repository.Repositories;
using TF.Domain.Models;

namespace TaskForge;

class Program
{
	public static void Main(string[] args)
	{
		var connectionString = "User=SYSDBA;Password=masterkey;Database=C:\\data\\TaskForge\\TASKFORGE.FB5;DataSource=localhost;Port=3055;Dialect=3;Charset=UTF8;Pooling=true;";

		using (var connection = new FbConnection(connectionString))
		{
			var workItemRepository = new WorkItemRepository(connection);

			var workItem1 = new WorkItem()
			{
				Name = "Lavar",
				Description = "Lavar a moto",
				CreatedAt = DateTime.Now,
				Completed = true,
				ItemOrder = 5,
			};

			var workItem2 = new WorkItem()
			{
				Name = "Lavar",
				Description = "Lavar as roupas",
				CreatedAt = DateTime.Now,
				Completed = true,
				ItemOrder = 2,
			};

			workItemRepository.Add(workItem1);
			workItemRepository.Add(workItem2);

			var workItems = workItemRepository.GetAll();

			Console.WriteLine("\nTODOS alunos: \n");
			foreach (var item in workItems)
			{
				Console.WriteLine(item.ToString());
			}

			workItemRepository.Remove(workItems.Last());
			workItemRepository.Remove(workItems.Last());

			Console.WriteLine("After Delete:\n");

			workItems = workItemRepository.GetAll();

			Console.WriteLine("\nTODOS alunos: \n");
			foreach (var item in workItems)
			{
				Console.WriteLine(item.ToString());
			}

		}
	}
}
/*public class WorkItem
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; } = "";
	public DateTime CreatedAt { get; set; }
	public bool Completed { get; set; } = false;
	public int ItemOrder { get; set; }
}*/