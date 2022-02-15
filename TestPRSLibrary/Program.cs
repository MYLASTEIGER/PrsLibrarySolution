using PrsLibrary.Controllers;
using PrsLibrary.Models;
using System;

namespace TestPRSLibrary {

    public class Program {

        static void Main(string[] args) {

            var context = new PrsDbContext();

            var userCtrl = new UsersController(context);

            var user = userCtrl.Login("sa", "sax");

            if(user is null) {
                Console.WriteLine("User not Found");
            } else {
                Console.WriteLine(user.Username);
            }


            //var prodCtrl = new ProductsController(context);


            //var products = prodCtrl.GetAll();
            //foreach(var p in products) {
            //    Console.WriteLine($"{p.Id,-5} {p.PartNbr,-12} {p.Name,-12} {p.Price,10:c} {p.Vendor.Name,-15}");
            //}

            //var product = prodCtrl.GetByPk(2);
            //if (product is not null) {
            //    Console.WriteLine($"{product.Id,-5} {product.PartNbr,-12} {product.Name,-12} {product.Price, 10:c} {product.Vendor.Name, -15}");
            //}





            //var userCtrl = new UsersController(context);
            //var newUser = new User() {
            //    Id = 0, Username = "yy", Password = "XX", Firstname = "User", Lastname = "yy", Phone = "311", Email = "yy@user.com", 
            //    IsReviewer = false, IsAdmin = false
            //};
            ////userCtrl.Create(newUser);

            //var user3 = userCtrl.GetByPk(3);
            //user3.Lastname = "user3";
            //userCtrl.Change(user3);

            //if(user3 is null) {
            //    Console.WriteLine("User not found.");
            //} else {
            //    Console.WriteLine($"User3: {user3.Firstname} {user3.Lastname}");
            //}

            //userCtrl.Remove(4);

            //var users = userCtrl.GetAll(); 

            //foreach (var user in users) {
            //    Console.WriteLine($"{user.Id} {user.Firstname} {user.Lastname}");
            //}
            
            
        }
    }
}
