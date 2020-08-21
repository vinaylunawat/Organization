namespace Geography.Business.Country
{
    using Geography.Business.Country.Models;
    using AutoMapper;
    using Entity.Entities;

    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryReadModel>();

            CreateMap<CountryCreateModel, Country>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.States, opt => opt.Ignore());

            CreateMap<CountryUpdateModel, Country>()
                .ForMember(x => x.States, opt => opt.Ignore());
        }
    }
}
