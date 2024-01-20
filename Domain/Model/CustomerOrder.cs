using System;
using System.Collections.Generic;

namespace StoreApi.Domain.Model;

public partial class CustomerOrder
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}
