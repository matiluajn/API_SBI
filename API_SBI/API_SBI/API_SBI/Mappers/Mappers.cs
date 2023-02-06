using API_SBI.Entities;
using API_SBI.Entities.Dtos;
using AutoMapper;


namespace API_SBI.Mappers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<ServerPost, Salida>().ReverseMap();
        }
    }
}
