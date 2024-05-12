using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderWings.Repo.DAL.Interfaces;

namespace ThunderWings.Repo.Models
{
    public class Basket : IEntity
    {
        public int Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public int Total { get; set; }
    }
}
