using System;
using System.Collections.Generic;

namespace RepositoryEF.Models;

public partial class Game
{
    public int Id { get; set; }

    public int? GenreId { get; set; }

    public string? GameName { get; set; }

    public virtual ICollection<GamePublisher> GamePublishers { get; } = new List<GamePublisher>();

    public virtual Genre? Genre { get; set; }
}
