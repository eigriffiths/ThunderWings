﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderWings.Repo.DAL.Interfaces;

namespace ThunderWings.Repo.Models
{
    public class BasketItem : IEntity
    {
        public int Id { get; set; }
        public int AircraftId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
