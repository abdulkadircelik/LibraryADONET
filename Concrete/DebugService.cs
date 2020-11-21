using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryADONET.Concrete
{
    public class DebugService
    {
        public void OnBookAdded(object source, BookEventHandler e)
        {
            Debug.WriteLine($"{e.Book.Title} has been added.");
        }
    }
}
