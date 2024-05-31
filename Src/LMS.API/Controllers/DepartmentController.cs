using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.IServiceMappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API;

[Route("api/[controller]")]
[Authorize]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentServiceMapping _departmentService;
    private readonly IMapper _mapper;

    public DepartmentController(IDepartmentServiceMapping departmentService, IMapper mapper)
    {
        this._departmentService = departmentService;
        this._mapper = mapper;
    }


    [HttpGet]
    public async Task<List<DepartmentDto>> GetDepartmentList()
    {
        var departements = await _departmentService.GetAllAsync();

        var departmentResultDto = _mapper.Map<List<DepartmentDto>>(departements);

        return departmentResultDto.ToList();
    }

}
