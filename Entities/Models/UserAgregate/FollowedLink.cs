namespace Entities.Models.UserAgregate
{
    public class FollowedLink : Entity<int>
    {
        public int UserId { get; private set; }
        
        public string Source { get; private set; }

        public int Count { get; private set; }

        private FollowedLink() { }

        public FollowedLink(string source, int count)
        {
            Source = source;
            Count = count;
        }

        public void UpdateCountOn(int count)
        {
            Count += count;
        }
    }
}
