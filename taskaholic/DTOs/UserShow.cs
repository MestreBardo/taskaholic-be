﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskaholic.DTOs
{
    public class UserShow
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
