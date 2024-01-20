﻿using System;
using System.Collections.Generic;

namespace StoreApi.Domain.Model;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }
}
