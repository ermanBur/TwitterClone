using System.ComponentModel.DataAnnotations;

namespace TwitterCloneApplication.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "E-Mail")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }


    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Accept Terms & Conditions")]
    [MustBeTrue(ErrorMessage = "You must accept the terms and conditions.")]
    public bool AcceptTerms { get; set; }
}

public class MustBeTrueAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value is bool b && b;
    }
}
