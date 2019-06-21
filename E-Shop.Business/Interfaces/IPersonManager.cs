using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface IPersonManager
    {
        Person AddPerson(
           PersonDetail personDetail,
           Address address,
           Address deliveryAddress,
           bool deliveryAddressIsAddress,
           string userId = null
           );

        void EditPerson(PersonDetail personDetail,
            Address address,
            Address deliveryAddress,
            string userId = null,
            int? personId = null);

        Person FindById(int id);
        Person FindByUserId(string id);

        void InsertOrEdit(Person person);
              
       

          
    }
}
