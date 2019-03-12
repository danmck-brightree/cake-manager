using AutoMapper;
using CakeManager.Shared;
using System.Linq;

namespace CakeManager.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CakeMark, Repository.Models.CakeMark>();
            CreateMap<Repository.Models.CakeMark, CakeMark>();

            CreateMap<Office, Repository.Models.Office>();
            CreateMap<Repository.Models.Office, Office>();
        }
    }
}
