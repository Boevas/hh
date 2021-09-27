using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using System.Collections.Concurrent;
namespace WebApplication2.Models
{

    //TODO
    /*
    public class UsersRepositoryStatic: IUsersRepository<User>
    {
        private static ConcurrentDictionary<int, User> users = new();
        static UsersRepositoryStatic()
        {
            try
            {
                users.TryAdd(1, new User { Id = 1, Name = "User1", Email = "user1@email.ru", PhoneNumber = 81111111111 });
                users.TryAdd(2, new User { Id = 2, Name = "User2", Email = "user2@email.ru", PhoneNumber = 82222222222 });
                users.TryAdd(3, new User { Id = 3, Name = "User3", Email = "user3@email.ru", PhoneNumber = 83333333333 });
                users.TryAdd(4, new User { Id = 4, Name = "User4", Email = "user4@email.ru", PhoneNumber = 84444444444 });
                users.TryAdd(5, new User { Id = 5, Name = "User5", Email = "user5@email.ru", PhoneNumber = 85555555555 });
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
            }
        }

        public List<User> Get()
        {
            try
            { 
                return users.Values.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }
        public User Get(int Id)
        {
            try
            { 
                if (true == users.TryGetValue(Id, out User res))
                    return res;

                return null;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return null;
            }
        }
        public bool Post(User user)
        {
            try
            { 
                user.Id = users.Keys.Max() + 1;
                return users.TryAdd(user.Id, user);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return false;
            }
        }
        public bool Put(User user_new)
        {
            try
            {
                if (users.TryGetValue(user_new.Id, out User user_old))
                    return users.TryUpdate(user_new.Id, user_new, user_old);

                return false;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return false;
            }
        }
        public bool Delete(User user)
        {
            try
            { 
                return users.TryRemove(user.Id, out User res);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.ToString());
                return false;
            }
        }
    }
    */
}
