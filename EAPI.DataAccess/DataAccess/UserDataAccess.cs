using EAPI.Data;
using EAPI.DataAccess.Entities;
using EAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.DataAccess.Interfaces
{
    public class UserDataAccess : IUserDataAccess
    {
        private EAPIContext _dbContext;
        public UserDataAccess(EAPIContext context)
        {
            _dbContext = context;
        }

        public List<UserItem> GetUsers()
        {
            return _dbContext.Users.Select(s=> 
                new UserItem()
                {
                    UserId = s.UserId,
                    Name = s.UserName,
                    Address = s.Address,
                    Email = s.Email
                }
            ).ToList();
        }

        public UserItem Register(UserItem user)
        {
           
                if (_dbContext.Users.Any(s => s.Email == user.Email))
                    throw new DuplicateRecordError("Email is already exists");
                _dbContext.Users.Add(new User()
                {

                    UserName = user.Name,
                    Email = user.Email,
                    Address = user.Address
                });
                _dbContext.SaveChanges();
               var savedUser= _dbContext.Users.FirstOrDefault(s => s.Email == user.Email);
                user.UserId = savedUser.UserId;
                return user;
            
            
        }
    }
}
