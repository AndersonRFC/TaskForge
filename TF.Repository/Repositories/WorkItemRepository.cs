using FirebirdSql.Data.FirebirdClient;
using TF.Domain.Models;
namespace TF.Repository.Repositories;



public class WorkItemRepository
{
	private readonly FbConnection _connection;

	public WorkItemRepository(FbConnection connection)
	{
		_connection = connection;
	}

	public WorkItem? Get(int id)
	{
		WorkItem? workItem = null;

		string sql = "SELECT * FROM WORKITEM WHERE ID = @Id;";

		using (FbCommand command = new FbCommand(sql, _connection))
		{
			command.Parameters.AddWithValue("@id", id);
			_connection.Open();

			using(FbDataReader reader = command.ExecuteReader())
			{
				if(reader.Read())
				{
					workItem = new WorkItem()
					{
						Id = reader.GetInt32(0),
						Name = reader.GetString(1),
						Description = reader.GetString(2),
						CreatedAt = reader.GetDateTime(3),
						Completed =	reader.GetBoolean(4),
						ItemOrder = reader.GetInt32(5),
					};
				}
			}
			_connection.Close();
		}
		return workItem;
	}

	public IEnumerable<WorkItem> GetAll()
	{
		List<WorkItem> workItems = new List<WorkItem>();

		string sql = "SELECT * FROM WORKITEM;";

		using(FbCommand command = new FbCommand(sql, _connection))
		{
			_connection.Open();

			using(FbDataReader reader = command.ExecuteReader())
			{
				while(reader.Read())
				{
					workItems.Add(new WorkItem()
					{
						Id = reader.GetInt32(0),
						Name = reader.GetString(1),
						Description = reader.GetString(2),
						CreatedAt = reader.GetDateTime(3),
						Completed = reader.GetBoolean(4),
						ItemOrder = reader.GetInt32(5),
					});
				}
			}
			_connection.Close();
		}
		return workItems;
	}

	public void Add(WorkItem workItem)
	{
		string sql = "INSERT INTO WORKITEM (NAME, DESCRIPTION, CREATEDAT, COMPLETED, ITEMORDER) VALUES (@Name, @Description, @CreatedAt, @Completed, @ItemOrder);";

		using(FbCommand command = new FbCommand(sql, _connection))
		{
			command.Parameters.AddWithValue("@Name", workItem.Name);
			command.Parameters.AddWithValue("@Description", workItem.Description);
			command.Parameters.AddWithValue("@CreatedAt", workItem.CreatedAt.ToString("yyyy-MM-dd"));
			command.Parameters.AddWithValue("@Completed", workItem.Completed);
			command.Parameters.AddWithValue("@ItemOrder", workItem.ItemOrder);

			_connection.Open();
			command.ExecuteNonQuery();
			_connection.Close();
		}
	}

	public void Update(WorkItem workItem)
	{
		string sql = "UPDATE WORKITEM SET NAME = @Name, DESCRIPTION = @Description, CREATEDAT = @CreatedAt, COMPLETED = @Completed, ITEMORDER = @ItemOrder WHERE ID = @Id;";

		using(FbCommand command = new FbCommand(sql, _connection))
		{
			command.Parameters.AddWithValue("@Description", workItem.Description);
			command.Parameters.AddWithValue("@CreatedAt", workItem.CreatedAt.ToString("yyyy-MM-dd"));
			command.Parameters.AddWithValue("@Completed", workItem.Completed);
			command.Parameters.AddWithValue("@ItemOrder", workItem.ItemOrder);
			
			command.Parameters.AddWithValue("@Id", workItem.Id);

			_connection.Open();
			command.ExecuteNonQuery();
			_connection.Close();
		}

	}
	public void Remove(WorkItem workItem)
	{
		string sql = "DELETE FROM WORKITEM WHERE ID = @Id;";

		using(FbCommand command = new FbCommand(sql, _connection))
		{
			command.Parameters.AddWithValue("@Id", workItem.Id);

			_connection.Open();
			command.ExecuteNonQuery();
			_connection.Close();
		}
	}
	

}
