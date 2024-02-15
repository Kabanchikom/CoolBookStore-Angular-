using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Web.Areas.Product.Models;
using Web.Models;

namespace Web.Areas.Author.Models;

public class AuthorCard : Entity<Guid>
{
    [Display(Name = "Фамилия")] 
    public string? Surname { get; set; }
    
    
    [Display(Name = "Имя")] 
    public string Name { get; set; }

    [Display(Name = "Отчество")]
    public string? Patronimic { get; set; }
    
    [JsonIgnore]
    public List<BookCard> Books { get; set; } = new();

    public override string ToString()
    {
        return $"{Name} {Patronimic} {Surname}";
    }

    /// <summary>
    /// Инициалы.
    /// </summary>
    [JsonIgnore]
    public string Initials
    {
        get
        {
            if (Surname == null)
            {
                return Name;
            }

            var initials = Name[0] + ".";

            if (Patronimic != null)
            {
                initials += Patronimic[0] + ".";
            }

            initials += " " + Surname;

            return initials;
        }
    }
}