using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Web.Areas.Account.Exceptions;

public class IdentityException: Exception
{
    public List<IdentityError> Errors { get; }
    
    public IdentityException(IdentityError error)
    {
        Errors = new List<IdentityError>{error};
    }
    
    public IdentityException(List<IdentityError> errors)
    {
        Errors = errors;
    }

    public override string ToString() => JsonConvert.SerializeObject(Errors);
}