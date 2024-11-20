using Azure;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace UserInterface.Controllers;

public class CourseController : InfraController
{

    [HttpGet]
    public IActionResult Index()
    {

        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Course/GetAll").Result;
        List<Course> courses = new List<Course> { };
        if (response.IsSuccessStatusCode)
        {
            // Step 3: Read the content as a string
            string jsonData = response.Content.ReadAsStringAsync().Result;

            // Step 4: Deserialize the JSON data into a list of Course objects
            courses = JsonConvert.DeserializeObject<List<Course>>(jsonData);

        }
            return View(courses);
    }


    public IActionResult CourseDetails()
    {
        return View();
    }
}
