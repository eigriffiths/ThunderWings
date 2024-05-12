using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderWings.Core.DTO.Aircraft
{
    public class AircraftDto
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public int TopSpeed { get; set; }
        public int Price { get; set; }
    }
}
