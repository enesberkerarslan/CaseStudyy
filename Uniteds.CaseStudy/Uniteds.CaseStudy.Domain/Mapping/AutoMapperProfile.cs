using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniteds.CaseStudy.Domain.DTOs;
using Uniteds.CaseStudy.Domain.Models;

namespace Uniteds.CaseStudy.Domain.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Note, NoteDto>()
                .ReverseMap();
         
        }
    }
}
