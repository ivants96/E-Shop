using AutoMapper;
using E_Shop.Data.Models;
using E_Shop.Models.PersonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Classes
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonEditViewModel, PersonDetail>();
            CreateMap<PersonEditViewModel, Address>();


            CreateMap<PersonDetail, PersonEditViewModel>();
            CreateMap<Address, PersonEditViewModel>();

            CreateMap<PersonRegisterViewModel, PersonDetail>();
            CreateMap<PersonRegisterViewModel, Address>();

            CreateMap<PersonDetail, PersonRegisterViewModel>();
            CreateMap<Address, PersonRegisterViewModel>();
           





        }
    }
}
