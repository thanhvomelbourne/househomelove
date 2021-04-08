using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Model
{
    public partial class Entities : IEntities
    {
        public Entities(string connectionString) : base("name=" + connectionString)
        {
        }
    }
}
