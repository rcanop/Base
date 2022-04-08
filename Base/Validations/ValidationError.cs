using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Validations
{
    public enum ValidationError
    {
        OK = 0,
        None = 0,
        LengthError,
        BadFormat,
        DCError,
        Error = 100
    }
}
