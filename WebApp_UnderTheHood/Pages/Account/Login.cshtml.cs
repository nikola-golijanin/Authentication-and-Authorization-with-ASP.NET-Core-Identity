using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages.Account;

public class Login : PageModel
{
    [BindProperty] public Credential Credential { get; set; } = new Credential();

    public void OnGet()
    {
    }

    public void OnPost()
    {
        
    }
}

public class Credential
{
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}