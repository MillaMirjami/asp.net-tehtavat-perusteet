using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("another")]

public class AnotherController
{
    [HttpGet]
    public async Task<ActionResult<string>> GetPrice([FromQuery] string price) {
        return price + " is very cheap";
    }

    [HttpGet("{price}")]
    public async Task<ActionResult<double>> GetPrice2(string price) {
        return Convert.ToDouble(price);
    }
}

