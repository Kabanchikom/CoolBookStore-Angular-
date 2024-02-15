using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Account.DTO;

public class RegisterRequestDto
{
    [Required]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
 
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    [Required]
    [DisplayName("Фамилия")]
    public string LastName { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    [Required]
    [DisplayName("Имя")]
    public string FirstName { get; set; }

    /// <summary>
    /// Отчество/среднее имя.
    /// </summary>
    [DisplayName("Отчество")]
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    [Required]
    [DisplayName("Дата рождения")]
    public DateTime DateOfBirth { get; set; }
}