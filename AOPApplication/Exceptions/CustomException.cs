using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPApplication.Exceptions
{
    public class CustomNetworkException : Exception
    {
        public CustomNetworkException(string message)
            : base(message)
        {

        }
    }
    public class CustomSqlException : Exception
    {
        public CustomSqlException(string message)
            : base(message)
        {

        }
    }
}
