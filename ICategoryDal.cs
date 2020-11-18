﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryADONET
{
    public interface ICategoryDal
    {
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        List<Book> GetAll();
        Category GetById(int id);
    }
}