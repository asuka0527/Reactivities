using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        // CreateMap< FROM (DOMAIN) , TO(EDIT HANDLER) >
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
        }
    }
}