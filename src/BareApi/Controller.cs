using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// [Controller] class atrribute merkkaa käännösvaiheessa (dotnet build)

[ApiController]
[Route("")]

// 1. pitäisi olla public
// 2. pitäisi periä ControllerBase
// 3. pitäisi olla namespace
public class Controller
{
    // Get juuri-polusta (root path)
    [HttpGet]
    public async Task<ActionResult<string>> GetGreeting() {
        return "It works!";
    }

    [HttpGet("/Milla")]
    public ActionResult<string> GetMilla() {
        return "Milla Milla Milla";
    }
}