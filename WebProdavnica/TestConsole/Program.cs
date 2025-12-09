using DAL.Abstract;
using DAL.Impl;
using Entities;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
           IUserRepository userRepository = new UserRepository();

            User user = new User
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@gmail.com",
                PasswordHash = "pass",
                Status = true,
                IdRole = 1
            };
            if (userRepository.Add(user)==true) {
                Console.WriteLine("Uspeno");
            
            }
            else
            {
                Console.WriteLine("Greska");
            }
   
        }
    }
}
