using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.Configuration
{
    public class ConnectionDataSource
    {
        public List<ConnectionStrings> ConnectionStrings { get; set; }
    }


    public class ConnectionStrings
    {
        public string Name { get; set; }
        public string connectionString { get; set; }
    }
}
