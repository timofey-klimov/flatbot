﻿using System.Collections.Generic;

namespace Entities.Models
{
    public class Disctrict : Entity<int>
    {
        public string Name { get; set; }

        public ICollection<UserContext> UserContexts { get; set; }
    }
}