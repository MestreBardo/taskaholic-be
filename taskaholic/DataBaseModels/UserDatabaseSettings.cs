﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskaholic.DataBaseModels
{
    public class UserDatabaseSettings : IUserDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
