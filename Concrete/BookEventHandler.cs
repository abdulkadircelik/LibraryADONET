using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryADONET.Concrete
{
    public class BookEventHandler : EventArgs
    {
        public Book Book { get; set; }
    }
}
