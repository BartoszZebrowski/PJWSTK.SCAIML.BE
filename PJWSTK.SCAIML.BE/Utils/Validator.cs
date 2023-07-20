using PJWSTK.SCAIML.BE.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Utils
{
    public class Validator
    {
        public static bool Validate(Func<bool> validateLogic, string errorMessage)
        {
            if (validateLogic.Invoke() == false)
                throw new BadRequestException(errorMessage);
            
            return true;
        }
    }
}
