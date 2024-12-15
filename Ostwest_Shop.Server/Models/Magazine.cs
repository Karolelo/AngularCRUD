using System;
using System.Collections.Generic;

namespace Ostwest_Shop.Server.Models;

public partial class Magazine
{
    public int ProductId { get; set; }

    public int Quanity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
