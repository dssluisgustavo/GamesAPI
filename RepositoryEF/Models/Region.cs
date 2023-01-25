using System;
using System.Collections.Generic;

namespace RepositoryEF.Models;

public partial class Region
{
    public int Id { get; set; }

    public string? RegionName { get; set; }

    public virtual ICollection<RegionSale> RegionSales { get; } = new List<RegionSale>();
}
