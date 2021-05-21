using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class Direcciones : BaseClass
    {
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }

    }
}
