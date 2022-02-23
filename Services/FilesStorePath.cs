namespace PrepTeach.Services
{
    public class FilesStorePath : IFilesStorePath
    {
        private readonly IWebHostEnvironment env;
        private readonly char pch = Path.DirectorySeparatorChar;
        public FilesStorePath(IWebHostEnvironment _env)
        {
            env = _env;
        }

        public string GetStorePath(string pth)
        {
            string dir = Environment.CurrentDirectory;
            string path = $"{dir}{pch}wwwroot{pch}media{pch}{pth}";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }
    }

    public interface IFilesStorePath
    {
        string GetStorePath(string pth = "");
    }
}
