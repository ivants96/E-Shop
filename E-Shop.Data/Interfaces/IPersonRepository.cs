﻿
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetSeller();
        Person FindByUserId(string id);
    }
}
