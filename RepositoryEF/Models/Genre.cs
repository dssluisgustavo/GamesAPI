using System;
using System.Collections.Generic;

namespace RepositoryEF.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Game> Games { get; } = new List<Game>();
}
