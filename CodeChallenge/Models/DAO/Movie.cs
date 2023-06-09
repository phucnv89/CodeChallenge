﻿using System;
using System.Collections.Generic;

namespace CodeChallenge.Models.DAO;

public partial class Movie
{
    public Guid Uuid { get; set; }

    public string Title { get; set; }

    public Guid DirectorUuid { get; set; }

    public DateTime ReleaseDate { get; set; }

    public short? Rating { get; set; }

    public virtual Director Director { get; set; }
}
