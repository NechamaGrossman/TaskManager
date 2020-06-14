using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Data
{
    public class AccountRepository
    {
        string _connectionString;
        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user, string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using (var context = new UserTaskContext(_connectionString))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

        }
        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (isValidPassword)
            {
                return user;
            }
            return null;
        }
        public User GetByEmail(string email)
        {
            using (var context = new UserTaskContext(_connectionString))
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }
        public bool EmailAvailable(string email)
        {
            using (var context = new UserTaskContext(_connectionString))
            {
                bool isUsed = context.Users.Any(u => u.Email == email);
                return isUsed;
            }
        }
        public User GetById(int id)
        {
            using (var context = new UserTaskContext(_connectionString))
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }
    }
}
