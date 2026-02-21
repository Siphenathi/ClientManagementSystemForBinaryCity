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
    }
}
