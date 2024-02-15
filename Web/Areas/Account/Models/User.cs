using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Web.Areas.Account.Models;

public class User : BaseUser
{
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

    // /// <summary>
    // /// Номер телефона.
    // /// </summary>
    // [Phone]
    // [Required]
    // [DisplayName("Номер телефона")]
    // public string PhoneNumber { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    [Required]
    [DisplayName("Дата рождения")]
    public DateTime DateOfBirth { get; set; }
}