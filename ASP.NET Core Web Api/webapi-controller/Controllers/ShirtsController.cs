using Microsoft.AspNetCore.Mvc;

namespace webapi_controller.Controllers;

[ApiController]
[Route("[controller]")]
public class ShirtsController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Get all shirts";
    }

    [HttpGet("{id}")]
    public string GetByID(int id)
    {
        return $"Get shirt with ID: {id}";
    }

    [HttpPost]
    public string Post()
    {
        return "Post a new shirt ";
    }

    [HttpPut("{id}")]
    public string Put(int id)
    {
        return $"Update shirt with ID: {id}";
    }

    [HttpDelete("{id}")]
    public string Delete(int id)
    {
        return $"Delete shirt with ID: {id}";
    }
}