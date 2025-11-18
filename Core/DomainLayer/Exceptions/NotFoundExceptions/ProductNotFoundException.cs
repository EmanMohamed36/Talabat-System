using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.NotFoundExceptions
{
    public sealed class ProductNotFoundException(int id)
                                    :NotFoundException($"Product With Id {id} Not Found")
    {
    }
}
