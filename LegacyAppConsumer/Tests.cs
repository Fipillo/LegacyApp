using System;
using System.Runtime.InteropServices;
using LegacyApp;


namespace LegacyApp
{
    public class Tests
    {
        static void Main(string[] args)
        {
            var userService = new LegacyApp.UserService();

            var ifAdded1 = userService.AddUser("tom", "Riddle", "tom.riddle@hg.com", DateTime.Parse("2020-12-18"), 1);
            Console.WriteLine(ifAdded1);
            var ifAdded2 = userService.AddUser(null, "Riddle", "tom.riddle@hg.com", DateTime.Parse("2000-12-18"), 2);
            Console.WriteLine(ifAdded2);
            var ifAdded3 = userService.AddUser("", "Riddle", "tom.riddle@hg.com", DateTime.Parse("1999-12-18"), 3);
            Console.WriteLine(ifAdded3);
            var ifAdded4 = userService.AddUser("tom", "Malewski", "tom.riddle@hg.com", DateTime.Parse("1999-12-18"), 4);
            Console.WriteLine(ifAdded4);
            var ifAdded5 = userService.AddUser("tom", "Smith", "tom.riddle@hg.com", DateTime.Parse("1999-12-18"), 5);
            Console.WriteLine(ifAdded5);

        }
    }
}