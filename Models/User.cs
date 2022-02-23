using System.ComponentModel.DataAnnotations;

namespace PrepTeach.Models
{
    public class User : BaseModel
    {
        [StringLength(20)]
        public string Login { get; set; } = null!;

        [StringLength(64)]
        public string FirstName { get; set; } = null!;
        

        [StringLength(64)]
        public string LastName { get; set; } = null!;
        

        [StringLength(64)]
        public string FatherName { get; set; } = null!;

        [StringLength(20)]
        public string Password { get; set; } = null!;
    }
}
