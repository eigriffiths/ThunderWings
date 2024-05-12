using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderWings.Core.DTO.Basket
{
    public class BasketDto
    {
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
