using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.NotFoundExceptions
{
    public class UserNotFoundException(string email) 
        :NotFoundException($"User With this email : {email} is not found")
    {
    }
}
