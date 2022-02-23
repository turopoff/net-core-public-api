using PrepTeach.Models;

namespace PrepTeach.ViewModels
{
    public class UserView
    {
        public string Login { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FatherName { get; set; } = null!;

        public UserView(User user)
        {
            Login = user.Login;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FatherName = user.FatherName;
        }
    }
}
