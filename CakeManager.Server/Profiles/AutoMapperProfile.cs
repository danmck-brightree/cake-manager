using AutoMapper;
using CakeManager.Shared;

namespace CakeManager.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CakeMark, Repository.Models.CakeMark>();
            CreateMap<Repository.Models.CakeMark, CakeMark>();
        }
    }
}
