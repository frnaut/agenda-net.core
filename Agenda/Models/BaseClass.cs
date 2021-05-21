using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class BaseClass
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
    }
}
