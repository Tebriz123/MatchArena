using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Notification:BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }

        //reletion

        public string UserId { get; set; }
        public AppUser User { get; set; }


    }
}
