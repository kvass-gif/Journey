using System.ComponentModel.DataAnnotations;

namespace Journey.Entities
{
    public class Record
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
