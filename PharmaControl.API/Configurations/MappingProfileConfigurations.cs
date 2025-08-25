using AutoMapper;
using PharmaControl.Application.DTO.Employee;
using PharmaControl.Common.Enuns;
using PharmaControl.Domain.Models;

namespace PharmaControl.API.Configurations;

public class MappingProfileConfigurations : Profile
{
    public MappingProfileConfigurations()
    {
        CreateMap<EmployeeRequestDto, EmployeeModel>()
            .ForMember(dest => dest.Role, opt
                => opt.MapFrom(src => ParseRole(src.Role)))
            .ForMember(dest => dest.Status, opt 
                => opt.MapFrom(src => ParseStatus(src.Status)));

        CreateMap<EmployeeModel, EmployeeResponseDto>();
    }

    private static StatusEnum ParseStatus(string status)
    {
        return Enum.TryParse<StatusEnum>(status, true, out StatusEnum parsedStatus)
            ? parsedStatus
            : StatusEnum.Ativo;
    }
    
    private static RoleEnum ParseRole(string role)
    {
        return Enum.TryParse<RoleEnum>(role, true, out RoleEnum parsedRole)
            ? parsedRole
            : RoleEnum.Atendente;
    } 
}
