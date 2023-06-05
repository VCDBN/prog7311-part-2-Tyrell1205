using System;
using System.Collections.Generic;

namespace ST10083372_PROG_POE.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public DateTime SupplyDate { get; set; }

    public string ProductType { get; set; } = null!;

    public virtual ICollection<FarmerProduct> FarmerProducts { get; set; } = new List<FarmerProduct>();
}
