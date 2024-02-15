using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Web.Areas.Account.Models;

namespace Web.Models;

public abstract class Entity<T>
{
    [Key]
    [JsonProperty(Order = -100)]
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    // public User? CreatedBy { get; set; }
    // public string CreatedById { get; set; }
    //
    // public DateTime? ModifiedAt { get; set; }
    // public User? ModifiedBy { get; set; }
    // public string ModifiedById { get; set; }

    public bool IsActive { get; set; } = true;
}