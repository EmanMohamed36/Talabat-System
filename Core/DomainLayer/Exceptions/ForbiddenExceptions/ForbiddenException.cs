using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.ForbiddenExceptions
{
    public abstract class ForbiddenException(string msg) 
        :Exception(msg)
    {
    }
}
