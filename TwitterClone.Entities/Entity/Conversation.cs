using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClone.Entity
{
    public class Conversation
    {
        public int Id { get; set; }
        public int UserOneId { get; set; }
        public int UserTwoId { get; set; }
    }
}
