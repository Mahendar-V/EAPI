using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAPI.Exceptions
{
    public class DuplicateRecordError:Exception
    {
        public DuplicateRecordError(string message):base(message)
        {

        }
    }
}
