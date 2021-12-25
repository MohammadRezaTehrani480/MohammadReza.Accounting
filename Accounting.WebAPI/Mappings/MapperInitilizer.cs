using Accounting.Shared.ViewModels;
using Accounting.Shared.ViewModels.AccountViewModels;
using Accounting.Shared.ViewModels.CashViewModels;
using Accounting.Shared.ViewModels.DocumentsViewModels;
using Accounting.Shared.ViewModels.LegalPersonViewModels;
using Accounting.Shared.ViewModels.LookupViewModels;
using Accounting.Shared.ViewModels.PersonViewModels;
using Accounting.Shared.ViewModels.RealPersonViewModels;
using Accounting.WebAPI.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Mappings
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()

        {
            CreateMap<Lookup, LookupCreationDTO>().ReverseMap();
            CreateMap<Lookup, LookupDTO>().ReverseMap();
            CreateMap<Lookup, LookupUpdateDTO>().ReverseMap();


            CreateMap<Person, PersonDTO>().ReverseMap();


            CreateMap<RealPerson, RealPersonCreationDTO>().ReverseMap();
            CreateMap<RealPerson, RealPersonDTO>().ReverseMap();
            CreateMap<RealPerson, RealPersonUpdateDTO>().ReverseMap();


            CreateMap<LegalPerson, LegalPersonCreationDTO>().ReverseMap();
            CreateMap<LegalPerson, LegalPersonDTO>().ReverseMap();
            CreateMap<LegalPerson, LegalPersonUpdateDTO>().ReverseMap();


            CreateMap<Cash, CashCreationDTO>().ReverseMap();
            CreateMap<Cash, CashDTO>().ReverseMap();
            CreateMap<Cash, CashUpdateDTO>().ReverseMap();


            CreateMap<Document, DocumentCreationDTO>().ReverseMap();
            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<Document, DocumentUpdateDTO>().ReverseMap();


            CreateMap<ApiUser, UserDTO>().ReverseMap();
            CreateMap<ApiUser, LoginUserDTO>().ReverseMap();
        }
    }
}
