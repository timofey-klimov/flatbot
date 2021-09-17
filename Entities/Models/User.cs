using System;

namespace Entities.Models
{
    public class User
    {
        public int Id { get; private set; }

        public long ChatId { get; private set; }

        public string UserName { get; private set; }

        public UserContext UserContext { get; private set; }

        private User() { }
        public User (long chatId, string userName)
        {
            ChatId = chatId;
            UserName = userName;
            UserContext = new UserContext();
        }

        public void SetMinimumPrice(int price)
        {
            if (UserContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            UserContext.MinimumPrice = price;
        }
    }
}
