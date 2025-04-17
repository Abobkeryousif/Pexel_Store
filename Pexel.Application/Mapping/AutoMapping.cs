using AutoMapper;
using Pexel.Core.DTOs.Products;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Productes, AddProductDto>().ReverseMap().ForMember(x => x.images, o => o.Ignore());
            CreateMap<Productes, UpdateProductDto>().ReverseMap().ForMember(i => i.images, o => o.Ignore());
        }
    }
}
