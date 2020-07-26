using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591.Common
{
    public class InvalidSettingException : Exception
    {
        public InvalidSettingException(string message)
            : base(message) { }

        public InvalidSettingException(string message, Exception inner)
            : base(message, inner) { }
    }
}
