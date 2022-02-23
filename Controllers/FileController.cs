using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrepTeach.Services;
using PrepTeach.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace PrepTeach.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Fayllar bilan ishlash")]
    public class FileController : Controller
    {
        private readonly IFilesStorePath filesStore;

        public FileController(IFilesStorePath _fileStore)
        {
            filesStore = _fileStore;
        }

        [HttpPost()]
        [Consumes("multipart/form-data")]
        [SwaggerOperation("Faylni Saqlab Url Olish")]
        public async Task<ResponseView<string>> Post([FromForm] FileView fileView)
        {
            ResponseView<string> response = new();
            try
            {
                string dir = filesStore.GetStorePath();
                string extension = Path.GetExtension(fileView.File.FileName);
                string fileName = $"{Guid.NewGuid()}{extension.ToLower()}";
                using (FileStream? stream = new(Path.Combine(dir, fileName), FileMode.Create))
                {
                    await fileView.File.CopyToAsync(stream);
                }
                string path = $"/media/{fileName}";
                response.Data = path;
                response.Status = 1;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
