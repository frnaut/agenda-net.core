using Agenda.EtityFramework;
using Agenda.EtityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.EntityFramework
{
    public class WorkSpace
    {
        private Context _context;
        public WorkSpace(Context context)
        {
            _context = context;
        }
        private PersonaRepo _personaRepo;
        public PersonaRepo PersonaRepo { 
            get { 
                if(_personaRepo == null)
                {
                    _personaRepo = new PersonaRepo(_context);
                }
                return _personaRepo;
            } 
        }
        private DireccionesRepo _direccionesRepo;
        public DireccionesRepo DireccionesRepo {
            get
            {
                if(_direccionesRepo == null)
                {
                    _direccionesRepo = new DireccionesRepo(_context);
                }

                return _direccionesRepo;
            }
        }
        private ContactosRepo _contactosRepo;
        public ContactosRepo ContactosRepo {
            get
            {
                if(_contactosRepo == null)
                {
                    _contactosRepo = new ContactosRepo(_context);
                }
                return _contactosRepo;
            } 
        }


        public string ErrorMessage(Exception ex)
        {
            string message = ex.Message;
            Exception inner = ex.InnerException;

            while(inner != null)
            {
                message = inner.Message;
                inner = inner.InnerException;
            }

            return message;
        }

    }
}
