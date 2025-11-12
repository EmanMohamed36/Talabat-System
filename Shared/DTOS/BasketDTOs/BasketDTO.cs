using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.BasketDTOs
{
    public class BasketDTO
    {

        public string Id { get; set; } //GUID Created From [front-end]
        public ICollection<BasketItemDTO> Items { get; set; }
    }
}
