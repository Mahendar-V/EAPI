using EAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.Behaviour.Interfaces
{
    public interface IUserControlBehaviour
    {
        UserItem Register(UserItem item);
        List<UserItem> GetUsers();
    }
}
