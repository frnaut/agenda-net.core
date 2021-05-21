using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class Contactos : BaseClass
    {
        public string Contacto { get; set; }
        public TipoContacto Tipo { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
    }


    public enum TipoContacto
    {
        email = 1,
        telefono = 2,
        celular = 3
    }
}

