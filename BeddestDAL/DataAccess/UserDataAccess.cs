using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeddestDAL
{
    public static class UserDataAccess
    {
        public static User FindUser(string name, string password)
        {
            User result;
            using (var context = new BeddestModel())
            {
                context.Users.Load();
                result = (from user in context.Users
                          where user.Name == name && user.Password == password
                          select user).FirstOrDefault();
            }
            return result;
        }

        public static void AddUser(string userName, string password)
        {
            if (CheckUserExists(userName))
                return;

            using (var context = new BeddestModel())
            {
                context.Users.Load();
                context.Users.Add(new User(userName, password));
                context.SaveChanges();
            }
        }

        public static bool CheckUserExists(string userName)
        {
            User userFound;
            using (var context = new BeddestModel())
            {
                context.Users.Load();
                userFound = (from user in context.Users
                             where user.Name == userName
                             select user).FirstOrDefault();
            }
            return userFound != null;
        }
    }
}
