using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.EtityFramework.Repositories
{
    public class DireccionesRepo : Repository<Direcciones>
    {
        public DireccionesRepo(Context context) : base(context)
        {
        }
    }
}
