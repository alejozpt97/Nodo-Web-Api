using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using System.Reflection;

namespace WebApi.Repositories

{
    public class UserRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext db =context;


        public async Task<List<User>> GetAllUsers()
        {
            return await db.User.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await db.User.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> CreateUser(User user)
        {
          var newUser= db.User.Add(user);
            await db.SaveChangesAsync();
            return newUser.Entity;
        }

        public async Task<User?> UpdateUserById(int id, User user)
        {
            var userToUpdate = await this.GetUserById(id);
            if (userToUpdate == null)
            {
                return null;
            }

            user.Id = userToUpdate.Id;

           var userUpdate = UpdateObject(userToUpdate, user);
            db.User.Update(userUpdate);

            await db.SaveChangesAsync();

            return userToUpdate;
        }

        private static T UpdateObject<T>(T current, T newObject)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
               var newValue = prop.GetValue(newObject);
               if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                 continue; 
               prop.SetValue(current, newValue);
            }
            return current;
        }

        public async Task<User?> DeleteUserById(int id)
        {
            var userToDelete = await this.GetUserById(id);
            if (userToDelete == null)
            {
                return null;
            }
            db.User.Remove(userToDelete);
            await db.SaveChangesAsync();
            return userToDelete;
        }
   
    }

}