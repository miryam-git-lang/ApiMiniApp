using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class UIAccountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UIAccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            var registerDto = new
            {
                fullName = model.FullName,
                username = model.Username,
                email = model.Email,
                password = model.Password,
                rePassword = model.RePassword
            };

            var jsonContent = JsonSerializer.Serialize(registerDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5097/api/Account/register", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Successfully registered! Please check your email to confirm.";
                return RedirectToAction("Login");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"{response.StatusCode}: {errorContent}");
                return View(model);
            }
        }
        catch (Exception exception)
        {
            ModelState.AddModelError(string.Empty, $"Error: {exception.Message}");
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = _httpClientFactory.CreateClient();

        var jsonContent = JsonSerializer.Serialize(model);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("http://localhost:5097/api/Account/login", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponseDto>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
            {
                Response.Cookies.Append("AuthToken", tokenResponse.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddHours(24)
                });
                TempData["SuccessMessage"] = "Successfully LoggedIn";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to retrieve authentication token");
                return View(model);
            }
        }

        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"{response.StatusCode}: {errorContent}");
            return View(model);
        }
    }
}