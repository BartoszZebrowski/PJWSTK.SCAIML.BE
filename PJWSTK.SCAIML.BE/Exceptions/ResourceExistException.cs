﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Exceptions
{
    public class ResourceExistException : Exception
    {
            public ResourceExistException(string message) 
                : base(message) {}
    }
}
