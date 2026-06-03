using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ApiMiniApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class UIEventsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    private async Task<List<EventReturnDto>> GetEventsAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var token = HttpContext.Request.Cookies["AuthToken"];
        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("http://localhost:5097/api/Event");
        if (!response.IsSuccessStatusCode)
            return new List<EventReturnDto>();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<EventReturnDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<EventReturnDto>();
    }
    
    public UIEventsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Request.Cookies["AuthToken"];
            var refreshToken = HttpContext.Request.Cookies["RefreshToken"];

            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            var response = await client.GetAsync("http://localhost:5097/api/Event");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var events = JsonSerializer.Deserialize<List<EventReturnDto>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(events ?? new List<EventReturnDto>());
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to load events from api";
                return View(new List<EventReturnDto>());
            }
        }
        catch (Exception exception)
        {
            TempData["ErrorMessage"] = $"An error occurred: {exception.Message}";
            return View(new List<EventReturnDto>());
        }
    }

    [HttpGet]
    [Produces("application/json")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> Create(CreateEventVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Title), "Title");
            formData.Add(new StringContent(model.Description), "Description");
            formData.Add(new StringContent(model.Location), "Location");
            formData.Add(new StringContent(model.Date.ToString()), "Date");
            formData.Add(new StringContent(model.OrganizerId.ToString()), "OrganizerId");

            var response = await client.PostAsync("http://localhost:5097/api/Event", formData);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Event created successfully!";
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ModelState.AddModelError("Title", "Event with same title already exists");
                return View(model);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "You need to login to create event";
                ModelState.AddModelError("Login", "UIAccount");
                return View(model);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to create an event:{errorContent}");
                return View(model);
            }
        }
        catch (Exception exception)
        {
            TempData["ErrorMessage"] = $"An error occurred: {exception.Message}";
            return View(model);
        }
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> CreateFile()
    {
        ViewBag.Events = await GetEventsAsync();
        return View(new CreateEventFileVm());
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> CreateFile(int eventId, CreateEventFileVm model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Events = await GetEventsAsync();
            return View(model);
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            using var formData = new MultipartFormDataContent();

            if (model.File != null && model.File.Length > 0)
            {
                var fileContent = new StreamContent(model.File.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.File.ContentType);
                formData.Add(fileContent, "File", model.File.FileName);
            }

            var response = await client.PostAsync($"http://localhost:5097/api/Event/{eventId}/banner", formData);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Event file successfully!";
                return RedirectToAction("Index");
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "You need to login to create event";
                ModelState.AddModelError("Login", "UIAccount");
                return View(model);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to create an event:{errorContent}");
                return View(model);
            }
        }
        catch (Exception exception)
        {
            TempData["ErrorMessage"] = $"An error occurred: {exception.Message}";
            return View(model);
        }
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Request.Cookies["AuthToken"];

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"http://localhost:5097/api/Event/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var events = JsonSerializer.Deserialize<EventReturnDto>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(events);
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to load event from api";
                return View(new EventReturnDto());
            }
        }
        catch (Exception exception)
        {
            TempData["ErrorMessage"] = $"An error occurred: {exception.Message}";
            return View(new EventReturnDto());
        }
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Edit()
    {
        ViewBag.Events = await GetEventsAsync();
        return View();
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> Edit(int id, EditEventVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Name), "Title");
            formData.Add(new StringContent(model.Description), "Description");
            formData.Add(new StringContent(model.Location), "Location");
            formData.Add(new StringContent(model.Date.ToString()), "Date");
            formData.Add(new StringContent(model.OrganizerId.ToString()), "OrganizerId");

            var response = await client.PatchAsync($"http://localhost:5097/api/Event/{id}", formData);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Event created successfully!";
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ModelState.AddModelError("Title", "Event with same title already exists");
                return View(model);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["ErrorMessage"] = "You need to login to create event";
                ModelState.AddModelError("Login", "UIAccount");
                return View(model);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to create an event:{errorContent}");
                return View(model);
            }
        }
        catch (Exception exception)
        {
            TempData["ErrorMessage"] = $"An error occurred: {exception.Message}";
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Request.Cookies["AuthToken"];

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.DeleteAsync($"http://localhost:5097/api/Event/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var events = JsonSerializer.Deserialize<EventReturnDto>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(new EventReturnDto());
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete event from api";
                return View(new EventReturnDto());
            }
        }
        catch (Exception exception)
        {
            TempData["ErrorMessage"] = $"An error occurred: {exception.Message}";
            return View(new EventReturnDto());
        }
    }
}