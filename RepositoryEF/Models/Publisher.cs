using System;
using System.Collections.Generic;

namespace RepositoryEF.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string? PublisherName { get; set; }

    public virtual ICollection<GamePublisher> GamePublishers { get; } = new List<GamePublisher>();
}
