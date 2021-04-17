using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF_FirebaseCrud.Models;

namespace XF_FirebaseCrud.Services
{
    public class UserService
    {
        FirebaseClient client;

        public UserService()
        {
            client = new FirebaseClient("https://xamarinrealtimeteste-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> IsUserExist(string name)
        {
            var user = (await client.Child("Users").OnceAsync<User>())
                .Where(u => u.Object.Username == name)
                .FirstOrDefault();

            //user != null = true else false 
            return (user != null);
        }

        public async Task<bool> RegisterUser(string name, string password)
        {
            if (await IsUserExist(name) == false && name != null)
            {
                await client.Child("Users")
                    .PostAsync(new User() 
                    { 
                        Username = name,
                        Password = password 
                    });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginUser(string name, string password)
        {
            var user = (await client.Child("Users").OnceAsync<User>())
                .Where(u => u.Object.Username == name)
                .Where(s => s.Object.Password == password)
                .FirstOrDefault();
            
            return (user != null);
        }
    }
}
