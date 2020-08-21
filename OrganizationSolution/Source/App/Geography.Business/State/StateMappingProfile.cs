namespace Geography.Business.Business.State
{
    using AutoMapper;
    using Geography.Business.Business.State.Models;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="CityMappingProfile" />.
    /// </summary>
    public class CityMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CityMappingProfile"/> class.
        /// </summary>
        public CityMappingProfile()
        {
            CreateMap<State, StateReadModel>()
                .ForMember(x => x.CountryCode, opt => opt.MapFrom(src => src.Country.IsoCode));

            CreateMap<StateCreateModel, State>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Country, opt => opt.Ignore())
                .ForMember(x => x.CountryId, opt => opt.Ignore());

            CreateMap<StateUpdateModel, State>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Country, opt => opt.Ignore())
                .ForMember(x => x.CountryId, opt => opt.Ignore());
        }
    }
}
