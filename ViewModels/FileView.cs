using System.ComponentModel.DataAnnotations;

namespace PrepTeach.ViewModels
{
    public class FileView
    {
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; } = null!;
    }
}
