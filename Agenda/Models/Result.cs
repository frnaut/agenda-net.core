using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class Result<T>
    {
        public  T Response { get; set; }
        public string MessageError { get; set; }
    }
}
