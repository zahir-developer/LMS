using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers.Pagination;
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


    [HttpGet("search")]
    [Authorize("Department_Search")]
    public async Task<PagedListResult<DepartmentDto>> GetDepartmentSearch([FromQuery] UserParams userParams)
    {
        var departements = await _departmentService.GetAllDepartmentSearch(userParams);

        return departements;
    }

    [HttpPost]
    [Authorize("Department_Add")]
    public async Task<bool> AddDepartment([FromBody] DepartmentDto departmentDto)
    {
        await _departmentService.AddAsync(departmentDto);
        return _departmentService.SaveChangesAsync();
    }

    [HttpPut]
    [Authorize("Department_Update")]
    public async Task<bool> UpdateDepartment([FromBody] DepartmentDto departmentDto)
    {
        await _departmentService.UpdateAsync(departmentDto);
        return _departmentService.SaveChangesAsync();
    }

    [HttpDelete("{departmentId}")]
    [Authorize("Department_Delete")]
    public async Task<bool> DeleteDepartment(int departmentId)
    {
        await _departmentService.DeleteByIdAsync(departmentId);
        return _departmentService.SaveChangesAsync();
    }

}
