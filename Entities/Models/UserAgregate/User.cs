using Entities.Enums;
using Entities.Models.Base;
using Entities.Models.Exceptions;
using Entities.Models.UserAgregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models
{
    public class User : AgregateRoot<int>
    {
        public long ChatId { get; private set; }

        public string UserName { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public UserContext UserContext { get; private set; }

        public NotificationContext NotificationContext { get; private set; }

        private List<FollowedLink> _followedLinks;

        public IReadOnlyCollection<FollowedLink> FollowedLinks => _followedLinks.AsReadOnly();

        private User() { }

        public User (long chatId, string userName, string name, string surname)
        {
            ChatId = chatId;
            UserName = userName;
            Name = name;
            Surname = surname;
            UserContext = new UserContext(40000, 25000, 10, 1);
            NotificationContext = new NotificationContext(NotificationType.Default, true);
        }

        public void ChangeMinimumPrice(decimal price)
        {
            if (UserContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            UserContext.ChangeMinimumPrice(price);
        }

        public void ChangeMaximumPrice(decimal price)
        {
            if (UserContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            UserContext.ChangeMaximumPrice(price);
        }
        public void DisableNotifications()
        {
            if (NotificationContext == null)
                throw new ArgumentNullException(nameof(NotificationContext));

            NotificationContext.Disable();
        }

        public void EnableNotifications()
        {
            if (NotificationContext == null)
                throw new ArgumentNullException(nameof(NotificationContext));

            NotificationContext.Enable();    
        }

        public void ChangeNotificationType(NotificationType notificationType)
        {
            if (NotificationContext == null)
                throw new ArgumentNullException(nameof(NotificationContext));

            NotificationContext.ChangeNotificationType(notificationType);
        }

        public void ChangTimeToMetro(int time)
        {
            if (UserContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            UserContext.ChangeTimeToMetro(time);
        }

        public void UpdateFollowedLinks(string source)
        {
            if (_followedLinks == null)
                throw new DomainNullRefException(_followedLinks);

            var link = _followedLinks.FirstOrDefault(x => x.Source == source);

            if (link == null)
                _followedLinks.Add(new FollowedLink(source, 1));
            else
                link.UpdateCountOn(1);
        }
       
    }
}
