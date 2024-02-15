using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Account.DTO;

public class LoginRequestDto
{
    [Required]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; }
         
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}