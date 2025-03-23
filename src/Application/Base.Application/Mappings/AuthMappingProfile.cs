using AutoMapper;
using Base.Application.Features.Auth.Commands;
using Base.Application.Features.Auth.Requests;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Base.Application.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterUserRequest, RegisterUserCommand>();
        }
    }
}
