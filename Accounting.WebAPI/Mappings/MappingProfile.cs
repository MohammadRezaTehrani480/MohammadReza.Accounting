using Accounting.Shared.ViewModels;
using Accounting.Shared.ViewModels.CashViewModels;
using Accounting.Shared.ViewModels.DocumentsViewModels;
using Accounting.Shared.ViewModels.LegalPersonViewModels;
using Accounting.Shared.ViewModels.RealPersonViewModels;
using Accounting.WebAPI.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()

        {
            CreateMap<LookupCreationDto, Lookup>().ReverseMap();
            CreateMap<LookupUpdateDto, Lookup>().ReverseMap();
            CreateMap<Lookup, LookupDTO>().ReverseMap(); ;



            CreateMap<RealPerson, RealPersonDTO>()
                .ForMember(c => c.Title,
                      opt => opt.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)))
                .ReverseMap();

            CreateMap<RealPersonCreationDto, RealPerson>().ReverseMap();
            CreateMap<RealPersonUpdateDto, RealPerson>().ReverseMap();


            CreateMap<LegalPerson, LegalPersonDTO>()
                .ForMember(l => l.Title,
                      opt => opt.MapFrom(x => string.Join(' ', x.Address, x.CompanyNo)))
                .ReverseMap();

            CreateMap<LegalPersonCreationDto, LegalPerson>().ReverseMap();
            CreateMap<LegalPersonUpdateDto, LegalPerson>().ReverseMap();


            CreateMap<Cash, CashDto>().ReverseMap();
            CreateMap<Cash, CashCreationDto>().ReverseMap();
            CreateMap<CashUpdateDto, Cash>().ReverseMap();


            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<Document, DocumentCreationDto>().ReverseMap();
            CreateMap<DocumentUpdateDto, Document>().ReverseMap();
        }
    }
}
