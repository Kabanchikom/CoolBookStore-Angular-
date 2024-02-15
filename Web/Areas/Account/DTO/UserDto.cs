using System.ComponentModel;

namespace Web.Areas.Account.DTO
{
    public class UserDto
    {
        public string Id { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Отчество")]
        public string? Patronymic { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime DateOfBirth { get; set; }
    }
}
