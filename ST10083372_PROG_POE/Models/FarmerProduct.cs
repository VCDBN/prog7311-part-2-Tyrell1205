using System;
using System.Collections.Generic;

namespace ST10083372_PROG_POE.Models;

public partial class FarmerProduct
{
    public int FarmerProductId { get; set; }

    public int FarmerId { get; set; }

    public int ProductId { get; set; }

    public virtual Farmer Farmer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
