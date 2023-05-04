namespace Project.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; } = false;
        public DateTime createDate { get; set; } = DateTime.Now;
    }
}
