namespace Entities.Models
{
    public class Image : Entity<int>
    {
        public long CiandId { get; set; }

        public string Source { get; set; }

        public byte[] Data { get; set; }
    }
}
