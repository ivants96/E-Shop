﻿using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context) { }

        public Person FindByUserId(string id)
        {
            return dbSet.FirstOrDefault(p => p.User.Id == id);
        }

        public Person GetSeller()
        {
            return dbSet.Single(p => p.User.Email == "admin@sparky.sk");
        }
    }
}
