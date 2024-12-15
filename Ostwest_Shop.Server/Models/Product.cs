using System;
using System.Collections.Generic;

namespace Ostwest_Shop.Server.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public byte[]? Img { get; set; }

    public virtual Magazine? Magazine { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
