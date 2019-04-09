using AutoMapper;
using CakeManager.Shared;

namespace CakeManager.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Office, Repository.Models.Office>();
            CreateMap<Repository.Models.Office, Office>();

            CreateMap<ActiveDirectoryUser, Repository.Models.ActiveDirectoryUser>();
            CreateMap<Repository.Models.ActiveDirectoryUser, ActiveDirectoryUser>();
        }
    }
}
