﻿using System;
using System.Collections.Generic;

namespace CodeChallenge.Models.DAO;

public partial class Director
{
    public Guid Uuid { get; set; }

    public string Name { get; set; }

    public DateTime Birthdate { get; set; }

    public virtual ICollection<Movie> Movies { get; set; }
}
