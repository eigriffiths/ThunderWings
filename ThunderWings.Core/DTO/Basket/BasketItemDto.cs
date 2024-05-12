﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderWings.Core.DTO.Basket
{
    public class BasketItemDto
    {
        public int AircraftId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}
