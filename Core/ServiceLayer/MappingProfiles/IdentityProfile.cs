using AutoMapper;
using DomainLayer.Models.IdentityModels;
using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<AddressDTO,Address>().ReverseMap();
        }
    }
}
