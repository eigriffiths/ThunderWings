using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderWings.Repo.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int AircraftId { get; set; }
        public int Quantity { get; set; }
    }
}
