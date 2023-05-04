namespace Project.Models
{
    public class News : BaseEntity
    {
        public string NewsTittle { get; set; }
        public int NewsOwnerId { get; set; }
        public NewsOwner NewsOwner { get; set; }
    }
}
