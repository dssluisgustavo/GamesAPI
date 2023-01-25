using System;
using System.Collections.Generic;

namespace RepositoryEF.Models;

public partial class RegionSale
{
    public int Id { get; set; }

    public int? RegionId { get; set; }

    public int? GamePlatformId { get; set; }

    public decimal? NumSales { get; set; }

    public virtual GamePlatform? GamePlatform { get; set; }

    public virtual Region? Region { get; set; }
}
