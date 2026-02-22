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
			    var feedbackMsg = await clientService.CreateClientAsync(clientRequest);
			    RedirectToAction("AllClients");
		    }
		    catch (Exception exception)
		    {

		    }
		    
		    return View();
	    }
    }
}
