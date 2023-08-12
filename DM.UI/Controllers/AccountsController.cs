using System.Net;
using DM.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DM.UI.Controllers;

public class AccountsController : Controller
{
    private static HttpClient httpClient = new HttpClient();
    
    public IActionResult LoginForm()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Account(string token)
    {
        using var responce = await httpClient.GetAsync($"http://localhost:5025/api/Auth/{token}");
        if (responce.StatusCode == HttpStatusCode.Unauthorized)
        {
            return View("NonAuthorized", "Accounts");
        }
        if (responce.StatusCode == HttpStatusCode.OK)
        {
            var data = await responce.Content.ReadFromJsonAsync<User>();
            return View(data);
        }

        return View("BadRequest", "Accounts");

    }
    
    
    [HttpPost]
    public async  Task<IActionResult> LoginForm(AuthModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        using var responce = await httpClient.PostAsJsonAsync($"http://localhost:5025/api/Auth", model);
        if (responce.StatusCode == HttpStatusCode.Unauthorized)
        {
            return View("NonAuthorized", "Accounts");
        }
        if (responce.StatusCode == HttpStatusCode.OK)
        {
            var data = await responce.Content.ReadFromJsonAsync<Session>();
            return RedirectToAction("Account", data);
        }

        return View("BadRequest","Accounts");
    }
}