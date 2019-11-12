using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskaholic.DataBaseModels
{
    public interface IUserDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
