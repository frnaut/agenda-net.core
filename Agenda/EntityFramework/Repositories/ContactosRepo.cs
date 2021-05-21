using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.EtityFramework.Repositories
{
    public class ContactosRepo : Repository<Contactos>
    {
        public ContactosRepo(Context context) : base(context)
        {
        }
    }
}
