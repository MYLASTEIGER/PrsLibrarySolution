using PrsLibrary.Controllers;
using PrsLibrary.Models;
using System;

namespace TestPRSLibrary {

    class Program {

        static void Main(string[] args) {
            var context = new PrsDbContext();
            var userCtrl = new UsersController(context);
            var newUser = new User() {
                Id = 0, Username = "yy", Password = "XX", Firstname = "User", Lastname = "yy", Phone = "311", Email = "yy@user.com", 
                IsReviewer = false, IsAdmin = false
            };
            //userCtrl.Create(newUser);

            var user3 = userCtrl.GetByPk(3);
            user3.Lastname = "user3";
            userCtrl.Change(user3);

            if(user3 is null) {
                Console.WriteLine("User not found.");
            } else {
                Console.WriteLine($"User3: {user3.Firstname} {user3.Lastname}");
            }

            userCtrl.Remove(4);

            var users = userCtrl.GetAll(); 

            foreach (var user in users) {
                Console.WriteLine($"{user.Id} {user.Firstname} {user.Lastname}");
            }
            
            
        }
    }
}
