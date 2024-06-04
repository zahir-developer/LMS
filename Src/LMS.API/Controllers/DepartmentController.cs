using System.Runtime.Intrinsics.Arm;
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
    [Authorize("Department_Get")]
    public async Task<List<DepartmentDto>> GetDepartmentList()
    {
        var departements = await _departmentService.GetAllAsync();

        var departmentResultDto = _mapper.Map<List<DepartmentDto>>(departements);

        return departmentResultDto.ToList();
    }

    [HttpPost]
    [Authorize("Department_Add")]
    public async Task<bool> AddDepartment(DepartmentDto departmentDto)
    {
        var addDepartment = await _departmentService.AddAsync(departmentDto);

        return addDepartment;
    }

    [HttpPut]
    [Authorize("Department_Update")]
    public async Task<bool> UpdateDepartment(DepartmentDto departmentDto)
    {
        var addDepartment = await _departmentService.UpdateAsync(departmentDto);

        return addDepartment;
    }

    [HttpDelete("{departmentId}")]
    [Authorize("Department_Delete")]
    public async Task<bool> DeleteDepartment(int departmentId)
    {
        var addDepartment = await _departmentService.DeleteByIdAsync(departmentId);

        return addDepartment;
    }

}
