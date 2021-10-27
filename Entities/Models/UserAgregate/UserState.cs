using Entities.Enums;
using System.Collections;
using System.Collections.Generic;

namespace Entities.Models
{
    public class UserState : Entity<int>
    {
        public UserStates State { get; private set; }

        public int UserContextId { get; private set; }

        public UserContext UserContext { get; private set; }

        private UserState() { }

        public UserState(UserStates state)
        {
            State = state;
        }

        public void ChangeState(UserStates state)
        {
            State = state;
        }
    }
}
