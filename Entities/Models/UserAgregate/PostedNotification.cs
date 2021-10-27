using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.UserAgregate
{
    public class PostedNotification : Entity<int>
    {
        public int UserContextId { get; private set; }

        public long CianId { get; private set; }

        private PostedNotification() { }

        public PostedNotification(long cianId)
        {
            CianId = cianId;
        }

        public static implicit operator PostedNotification(long cianId)
        {
            return new PostedNotification(cianId);
        }
    }
}
