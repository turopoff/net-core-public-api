using System.ComponentModel.DataAnnotations;

namespace PrepTeach.Models
{
    public class BaseModel
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }
}
