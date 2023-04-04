using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleAccount.Dto;

namespace SimpleAccount.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AuthRequestDto authRequestDto)
    {

        return Ok(authRequestDto.Token);
    }
}