using ClientManagementSystem.Model;
using ClientManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagementSystem.UI.Controllers
{
    public class ContactController(IContactService contactService) : Controller
    {
		public async Task<IActionResult> AllContacts(string feedback)
		{
			var clients = await contactService.GetAllContactsAsync();
			ViewBag.FeedbackMsg = feedback;
			return View(clients);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateContactRequest contactRequest)
		{
			try
			{
				var feedBack = await contactService.CreateContactAsync(contactRequest);
				return RedirectToAction("AllContacts", "Contact", new {feedBack});
			}
			catch (Exception exception)
			{
				ViewBag.FeedbackMsg = $"{exception.Message}. {exception.InnerException?.Message}";
			}
			return View(contactRequest);
		}

		public async Task<IActionResult> LinkClients(int contactId, string? feedBack)
		{
			var contactClients = await contactService.GetContactClients(contactId);
			ViewBag.FeedbackMsg = feedBack;
			return View(contactClients);
		}

		//[HttpPost]
		//public async Task<IActionResult> LinkContacts(LinkContacts linkContacts)
		//{
		//	var feedBack = await clientService.CreateClientContactAsync(linkContacts);
		//	return RedirectToAction("LinkContacts", "Client", new { clientCode = linkContacts.ClientCode, feedBack });
		//}

		//public async Task<IActionResult> Delete(string clientCode)
		//{
		//	var client = await clientService.GetClientAsync(clientCode);
		//	return View(client);
		//}

		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(string clientCode)
		//{
		//	_ = await clientService.DeleteClientAsync(clientCode);
		//	return RedirectToAction("AllClients");
		//}
	}
}
