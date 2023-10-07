using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace CompanyEmployees
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<DepartmentForCreationDto, Department>();
            CreateMap<ClientForCreationDto, Client>();
            CreateMap<EmployeeForUpdateDto, Employee>();
            CreateMap<DepartmentForUpdateDto, Department>();
            CreateMap<ClientForUpdateDto, Client>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<DepartmentForUpdateDto, Department>().ReverseMap();
            CreateMap<ClientForUpdateDto, Client>().ReverseMap();
        }
    }
}
