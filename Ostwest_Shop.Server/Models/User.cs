﻿using System;
using System.Collections.Generic;

namespace Ostwest_Shop.Server.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual UserDatum? UserDatum { get; set; }
    
}
