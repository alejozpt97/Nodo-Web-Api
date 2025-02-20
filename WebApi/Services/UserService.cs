using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Data;
using WebApi.Repositories;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class UserService(ApplicationDbContext context)
    {
         private readonly UserRepository userRepository = new(context);


         public async Task<List<User>> GetAllUsers()
        {
           return await userRepository.GetAllUsers();
        
        }

         public async Task<User?> GetUserById(int id)
    {
            return await userRepository.GetUserById(id);   
    }

        public async Task<User> CreateUser(User user)
    {
            validateUser(user);
    
         return await userRepository.CreateUser(user);
    }

       
        public async Task<User?> UpdateUserById(int id, User user)
    {
            validateUser(user);

         var existingUser = await userRepository.UpdateUserById(id, user);

          if (existingUser == null)
    {
        throw new KeyNotFoundException($"User with ID {id} not found");
    }

        return existingUser;
    }


         public async  Task<User?> DeleteUserById(int id)
    {
           return await userRepository.DeleteUserById(id);   
    }



         private static void validateUser(User user)
    {
        if (user.Age < 18)
    {
        throw new Exception("The user is underage"); 
    }
    }



 }

}   