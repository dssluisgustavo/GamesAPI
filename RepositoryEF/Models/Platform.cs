using System;
using System.Collections.Generic;

namespace RepositoryEF.Models;

public partial class Platform
{
    public int Id { get; set; }

    public string? PlatformName { get; set; }

    public virtual ICollection<GamePlatform> GamePlatforms { get; } = new List<GamePlatform>();
}
