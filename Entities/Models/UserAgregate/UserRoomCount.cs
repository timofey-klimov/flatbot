using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.UserAgregate
{
    public class UserRoomCount : Entity<int>
    {
        public int UserContextId { get; private set; }

        public int RoomCount { get; private set; }

        private UserRoomCount() { }

        public UserRoomCount(int count)
        {
            RoomCount = count;
        }

        public static implicit operator UserRoomCount(int roomCount)
        {
            return new UserRoomCount(roomCount);
        }
    }
}
