using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClone.Entity
{
    public class Media
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
