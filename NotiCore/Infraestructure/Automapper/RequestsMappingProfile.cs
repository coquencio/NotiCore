using AutoMapper;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Automapper
{
    public class RequestsMappingProfile : Profile
    {
        public RequestsMappingProfile()
        {
            CreateMap<AddSourceRequest, Source> ();
        }
    }
}
