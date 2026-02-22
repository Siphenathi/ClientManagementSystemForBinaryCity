using ClientManagementSystem.Model;
using ClientManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagementSystem.UI.Controllers
{
    public class ClientController(IClientService clientService) : Controller
    {
	    public async Task<IActionResult> AllClients()
        {
	        var clients = await clientService.GetAllClientsAsync(false);
            return View(clients);
        }

	    public IActionResult Create()
	    {
		    return View();
	    }

		[HttpPost]
	    public async Task<IActionResult> Create(CreateClientRequest clientRequest)
	    {
		    try
		    {
			    var feedBack = await clientService.CreateClientAsync(clientRequest);
			    if (!string.IsNullOrWhiteSpace(feedBack))
				    ViewBag.FeedbackMsg = feedBack;
				else
					return RedirectToAction("AllClients");
		    }
		    catch (Exception exception)
		    {
			    ViewBag.FeedbackMsg = $"{exception.Message}. {exception.InnerException?.Message}";
		    }
		    return View();
	    }

	    public async Task<IActionResult> Details(string clientCode)
	    {
		    var clientCodeDetails = await clientService.GetClientContact(clientCode);
		    return View(clientCodeDetails);
	    }
    }
}
