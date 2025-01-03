using System;
using System.Collections.Generic;

namespace Ostwest_Shop.Server.Models;

public partial class UserDatum
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateOnly BirthDate { get; set; }
    
    public string Email { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
