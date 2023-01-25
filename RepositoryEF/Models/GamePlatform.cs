using System;
using System.Collections.Generic;

namespace RepositoryEF.Models;

public partial class GamePlatform
{
    public int Id { get; set; }

    public int? GamePublisherId { get; set; }

    public int? PlatformId { get; set; }

    public int? ReleaseYear { get; set; }

    public virtual GamePublisher? GamePublishers { get; set; }

    public virtual Platform? Platform { get; set; }

    public virtual ICollection<RegionSale> RegionSales { get; } = new List<RegionSale>();
}
