namespace Geography.Business.Country
{
    using AutoMapper;
    using Entity.Entities;
    using Geography.Business.Country.Models;

    /// <summary>
    /// Defines the <see cref="CountryMappingProfile" />.
    /// </summary>
    public class CountryMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryMappingProfile"/> class.
        /// </summary>
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
