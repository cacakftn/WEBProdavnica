using DAL.Abstract;
using DAL.Impl;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
           IUserRepository userRepository = new UserRepository();

            var listaUser = userRepository.GetAll();
            foreach (var item in listaUser)
            {
                Console.WriteLine(item.FirstName+" "+item.LastName);
            }
        }
    }
}
