using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleAccount.Dto;
using SimpleAccount.Features.GoogleAuthentication;
using SimpleAccount.Services;

namespace SimpleAccount.Controllers;

[ApiController]
[Route("api/auth/google")]
public class AuthenticationController : Controller
{
    private IGoogleAuthProviderService _googleAuthProvider;

    public AuthenticationController(IGoogleAuthProviderService googleAuthProvider)
    {
        _googleAuthProvider = googleAuthProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AuthRequestDto authRequestDto)
    {
        var result = await _googleAuthProvider.ValidateGoogleJwt(authRequestDto.Token);
        
        return Ok(authRequestDto.Token);
    }
}