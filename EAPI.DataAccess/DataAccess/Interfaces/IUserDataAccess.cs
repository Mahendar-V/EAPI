using EAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.DataAccess
{
   public interface IUserDataAccess
    {
        UserItem Register(UserItem user);
        List<UserItem> GetUsers();
    }
}
