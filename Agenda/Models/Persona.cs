using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class Persona : BaseClass
    {
        public Persona()
        {
            Direcciones = new HashSet<Direcciones>();
            Contactos = new HashSet<Contactos>();
        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public virtual ICollection<Direcciones> Direcciones { get; set; }
        public virtual ICollection<Contactos> Contactos { get; set; }
    }
}
