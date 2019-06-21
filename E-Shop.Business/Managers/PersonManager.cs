using E_Shop.Business.Interfaces;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Managers
{
    public class PersonManager : IPersonManager
    {
        IPersonRepository _personRepository;
        IPersonDetailRepository _personDetailRepository;
        IAddressRepository _addressRepository;


        public PersonManager(
              IPersonRepository personRepository,
              IPersonDetailRepository personDetailRepository,
              IAddressRepository addressRepository)
        {
            _personRepository = personRepository;
            _personDetailRepository = personDetailRepository;
            _addressRepository = addressRepository;

        }

        public Person AddPerson(PersonDetail personDetail, Address address, Address deliveryAddress, bool deliveryAddressIsAddress, string userId = null)
        {
            if (deliveryAddressIsAddress == true)
            {
                deliveryAddress.StreetNameAndHouseNumber = address.StreetNameAndHouseNumber;
                deliveryAddress.City = address.City;
                deliveryAddress.PostalCode = address.PostalCode;
                deliveryAddress.Country = address.Country;
            }
            _personDetailRepository.Add(personDetail);
            _addressRepository.Add(address);
            _addressRepository.Add(deliveryAddress);

            Person person = new Person()
            {
                PersonDetailId = personDetail.PersonDetailId,
                AddressId = address.AddressId,
                DeliveryAddressId = deliveryAddress.AddressId,
                UserId = userId
            };

            _personRepository.Add(person);
            return person;
        }

        public void EditPerson(PersonDetail personDetail, Address address, Address deliveryAddress, string userId = null, int? personId = null)
        {
            var person = _personRepository.FindByUserId(userId);
            if (userId == null)
            {
                person = _personRepository.FindById(personId.Value);
            }
            personDetail.PersonDetailId = person.PersonDetailId;
            address.AddressId = person.AddressId;
            deliveryAddress.AddressId = person.DeliveryAddressId;

            _addressRepository.Update(address);
            _addressRepository.Update(deliveryAddress);
            _personDetailRepository.Update(personDetail);
            _personRepository.Update(person);
        }

        public Person FindById(int id) => _personRepository.FindById(id);

        public Person FindByUserId(string id) => _personRepository.FindByUserId(id);

        public void InsertOrEdit(Person person) => _personRepository.Update(person);

    }
}
