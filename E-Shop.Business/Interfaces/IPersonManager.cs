using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface IPersonManager
    {
        void AddPerson(
           PersonDetail personDetail,
           Address address,
           Address deliveryAddress,
           bool deliveryAddressIsAddress,
           string userId = null
           );

        void EditPerson(
          PersonDetail personDetail,
          Address address,
          Address deliveryAddress,
          string userId
          );

        Person FindById(int id);
        Person FindByUserId(string id);
    }
}
