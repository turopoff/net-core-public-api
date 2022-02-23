using Microsoft.EntityFrameworkCore;
using PrepTeach.Models;

namespace PrepTeach.Extensions
{
    public static class InitialDatabaseBuilder
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            #region User
                new User
                {
                    Id = 1,
                    FirstName = "admin",
                    LastName = "admin",
                    FatherName = "admin",
                    Login = "admin",
                    Password = "09"

                }
                #endregion
            );
        }
    }
}