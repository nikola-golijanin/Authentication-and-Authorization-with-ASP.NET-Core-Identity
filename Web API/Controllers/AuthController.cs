using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public IActionResult Authenticate([FromBody] Credential credential)
    {
        if (credential.UserName == "admin" && credential.Password == "admin")
        {
            // Creating the security context
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "admin@admin.com"),
                new Claim("Department","HR"),
                new Claim("Admin","true"),
                new Claim("Manager","true"),
                new Claim("EmploymentDate", "2023-01-01")
            };
                
            var exipersAt = DateTime.UtcNow.AddMinutes(10);

            return Ok(new {
                access_token = "",
                expires_at = exipersAt
                })
                
        }

        ModelState.AddModelError("Unauthorized", "You are not authorized to access the endpoint");
        return Unauthorized(ModelState);
    }

    private string CreateToken(IEnumerable<Claim> claims, DateTime expireAt){
        //Generate JWT
    }

}

public class Credential {

    public string UserName { get; set; }

    public string Password { get; set; }
}