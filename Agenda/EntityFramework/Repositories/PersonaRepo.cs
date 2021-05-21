using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.EtityFramework.Repositories
{
    public class PersonaRepo : Repository<Persona>
    {
        public PersonaRepo(Context context) : base(context)
        {
        }
    }
}
