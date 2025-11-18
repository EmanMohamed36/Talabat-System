using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.NotFoundExceptions
{
    public sealed class BasketNotFoundException(string id)
                                    : NotFoundException($"Basket With Id {id} Not Found")
    {
    }
}
