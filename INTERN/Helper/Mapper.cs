using AutoMapper;
using INTERN.DTO;
using INTERN.Model;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace INTERN.Helper
{
    public class Mapper : Profile
    {
        public Mapper() {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.NameType)); ;
            CreateMap<Model.Type, TypeDTO>();
            CreateMap<TypeDTO,Model.Type>();
        }
    }
}
