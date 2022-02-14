using EAPI.Behaviour.Interfaces;
using EAPI.Data;
using EAPI.DataAccess;
using EAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.Behaviour
{
    public class UserControlBehaviour : IUserControlBehaviour
    {
        private IUserDataAccess _userDataAccess;
        public UserControlBehaviour(IUserDataAccess dataAccess)
        {
            _userDataAccess = dataAccess;
        }
        public List<UserItem> GetUsers()
        {
            return _userDataAccess.GetUsers();
        }

        public UserItem Register(UserItem item)
        {
            if (item != null)
            {
                if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Email))
                    throw new ArgumentNullException("Name & Email Should not be empty.");

                return _userDataAccess.Register(item);
            }
            throw new ArgumentNullException("Name & Email Should not be empty");
        }
    }
}
