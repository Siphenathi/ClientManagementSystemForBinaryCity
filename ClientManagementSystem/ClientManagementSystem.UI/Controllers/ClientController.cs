using ClientManagementSystem.Model;
using ClientManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagementSystem.UI.Controllers
{
    public class ClientController(IClientService clientService) : Controller
    {
	    public async Task<IActionResult> AllClients(string feedback)
        {
	        var clients = await clientService.GetAllClientsAsync(false);
	        ViewBag.FeedbackMsg = feedback;
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
				return RedirectToAction("AllClients", "Client", new { feedBack });
			}
		    catch (Exception exception)
		    {
			    ViewBag.FeedbackMsg = $"{exception.Message}. {exception.InnerException?.Message}";
		    }
		    return View(clientRequest);
	    }

	    public async Task<IActionResult> LinkContacts(string clientCode, string? feedBack)
	    {
		    var clientContacts = await clientService.GetClientContacts(clientCode);
		    ViewBag.FeedbackMsg = feedBack;
			return View(clientContacts);
	    }

		[HttpPost]
	    public async Task<IActionResult> LinkContacts(LinkContactsToClient linkContactsToClient)
	    {
		    var feedBack = await clientService.CreateClientContactAsync(linkContactsToClient);
		    return RedirectToAction("LinkContacts", "Client", new { clientCode = linkContactsToClient.ClientCode, feedBack });
	    }

	    public async Task<IActionResult> Delete(string clientCode)
	    {
		    var client = await clientService.GetClientAsync(clientCode);
		    return View(client);
	    }

	    [HttpPost, ActionName("Delete")]
	    [ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string clientCode)
	    {
		    _ = await clientService.DeleteClientAsync(clientCode);
		    return RedirectToAction("AllClients");
	    }
    }
}