using AutoMapper;
using Projeto.WebAPI.Model;

namespace Projeto.WebAPI.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDetalhado>();
            CreateMap<ProdutoDetalhado, Produto>();
        }
    }
}