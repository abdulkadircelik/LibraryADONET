using LibraryADONET.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryADONET.Abstract
{
    public interface ICategoryDal 
        : IEntityRepository<Category>
    {
       
    }
}
