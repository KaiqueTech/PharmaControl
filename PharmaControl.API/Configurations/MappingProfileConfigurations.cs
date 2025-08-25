using AutoMapper;
using PharmaControl.Application.DTO.Employee;
using PharmaControl.Application.DTO.Supplier;
using PharmaControl.Common.Enuns;
using PharmaControl.Domain.Models;

namespace PharmaControl.API.Configurations;

public class MappingProfileConfigurations : Profile
{
    public MappingProfileConfigurations()
    {
        CreateMap<EmployeeRequestDto, EmployeeModel>()
            .ForMember(dest => dest.Role, opt
                => opt.MapFrom(src => ParseRole(src.Role)));

        CreateMap<EmployeeModel, EmployeeResponseDto>();

        
        CreateMap<SupplierRequestDto, SupplierModel>();

        CreateMap<SupplierModel, SupplierResponseDto>();
    }
    
    private static RoleEnum ParseRole(string role)
    {
        return Enum.TryParse<RoleEnum>(role, true, out RoleEnum parsedRole)
            ? parsedRole
            : RoleEnum.Atendente;
    } 
}
