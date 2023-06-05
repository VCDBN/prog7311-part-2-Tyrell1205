using System;
using System.Collections.Generic;

namespace ST10083372_PROG_POE.Models;

public partial class Farmer
{
    public int FarmerId { get; set; }

    public string FarmerName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

   

    public virtual ICollection<FarmerProduct> FarmerProducts { get; set; } = new List<FarmerProduct>();
}
