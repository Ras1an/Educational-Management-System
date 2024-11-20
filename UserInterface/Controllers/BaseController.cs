using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

using Dto;

using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net.Http.Headers;
using Models;

namespace UserInterface.Controllers;

public class BaseController : InfraController
{


    //public IActionResult Request(string ApiUrl)
    //{
    //    HttpWebRequest request = HttpWebRequest.CreateHttp(ApiUrl);
    //    var response = request.GetResponse();
    //    StreamReader reader = new StreamReader(response.GetResponseStream());
    //    string json = reader.ReadToEnd();

    //    return Json(json);

    //}
    //public IActionResult UserVerification(string Username, string Password)
    //{
    //    // https://localhost:7209/api/Student/GetUser?username=das

    //    StringBuilder ApiUrl = new StringBuilder();
    //    ApiUrl.Append(WebsiteUrl);
    //    ApiUrl.Append($"api/Student/GetUser?username=AMaher");

    //    var json = Request(ApiUrl.ToString());
    //    return View(json);
    //}

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignIn(LoginDto dto)
    {

        string jsonDto = JsonSerializer.Serialize(dto);

        var content = new StringContent(jsonDto, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/Account/Login", content);


        ViewBag.Message = "Username or password not correct!";
        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            HttpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(60)
                
        }) ;


            return RedirectToAction("Profile", "Base", new { dto.username });

        }



        return View();
    }



    public async Task<IActionResult> Profile(string username)
    {
        var token = HttpContext.Request.Cookies["JwtToken"];
        if (string.IsNullOrEmpty(token))
        {
            // Redirect to login if token is missing or expired
            return RedirectToAction("SignIn", "Base");
        }
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //https://localhost:7209/api/User/GetUser?username=mo123
        HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/User/GetProfile");
        if (response.IsSuccessStatusCode)
        {
            var profileData = await response.Content.ReadAsStringAsync();
            var profile = JsonConvert.DeserializeObject<User>(profileData);

            // Pass profile data to the view
            return View(profile);
        }

        return RedirectToAction("SignIn", "Base");
        
    }


    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> SignUp(Registerdto dto)
    {

        string jsonDto = JsonSerializer.Serialize(dto);

        var content = new StringContent(jsonDto, Encoding.UTF8, "application/json");

        HttpResponseMessage respone = _client.PostAsync(_client.BaseAddress + $"/Account/RegistrationAsync", content).Result;


        ViewBag.Message = "That username is taken. try another";
        if (respone.IsSuccessStatusCode)
        {
            //string data = respone.Content.ReadAsStringAsync().Result;
            //var d = JsonConvert.DeserializeObject<string>(data);
            ViewBag.Message = "You have Sign Up successfully";

        }


        return View();
    }


    public IActionResult ContactUs()
    {
        return View();
    }

}