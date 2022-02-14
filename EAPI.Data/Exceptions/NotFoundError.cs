using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.DataModels.Exceptions
{
    public class NotFoundError:Exception
    {
        public NotFoundError(string message) : base(message)
        {

        }
    }
}
