using Microsoft.AspNetCore.Mvc;
using TF.Domain.Models;
using TF.Repository.Repositories;

namespace TF.Web.Controllers;

public class WorkItemController : Controller
{
	readonly private WorkItemRepository _workItemRepository;

    public WorkItemController(WorkItemRepository workItemRepository)
	{
		_workItemRepository = workItemRepository;
	}

    public IActionResult Index()
    {
        IEnumerable<WorkItem> workItems = _workItemRepository.GetAll();
        return View(workItems);
    }
}
