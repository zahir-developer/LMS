using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Helpers.Pagination;
using LMS.Application.Interfaces.IServiceMappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API;

[Route("api/[controller]")]
[Authorize]
public class LeaveTypeController : ControllerBase
{
    private readonly ILeaveTypeServiceMapping _leaveTypeService;
    private readonly IMapper _mapper;

    public LeaveTypeController(ILeaveTypeServiceMapping leaveTypeService, IMapper mapper)
    {
        this._leaveTypeService = leaveTypeService;
        this._mapper = mapper;
    }

    [HttpGet]
    [Authorize("LeaveType_Get")]
    public async Task<List<LeaveTypeDto>> GetLeaveTypeList()
    {
        var departements = await _leaveTypeService.GetAllAsync();

        var leaveTypeResultDto = _mapper.Map<List<LeaveTypeDto>>(departements);

        return leaveTypeResultDto.ToList();
    }


    [HttpGet("search")]
    [Authorize("LeaveType_Search")]
    public async Task<PagedListResult<LeaveTypeDto>> GetLeaveTypeSearch([FromQuery] UserParams userParams)
    {
        var departements = await _leaveTypeService.GetAllLeaveTypeSearch(userParams);

        return departements;
    }

    [HttpPost]
    [Authorize("LeaveType_Add")]
    public async Task<bool> AddLeaveType([FromBody] LeaveTypeDto leaveTypeDto)
    {
        await _leaveTypeService.AddAsync(leaveTypeDto);
        return _leaveTypeService.SaveChangesAsync();
    }

    [HttpPut]
    [Authorize("LeaveType_Update")]
    public async Task<bool> UpdateLeaveType([FromBody] LeaveTypeDto leaveTypeDto)
    {
        await _leaveTypeService.UpdateAsync(leaveTypeDto);
        return _leaveTypeService.SaveChangesAsync();
    }

    [HttpDelete("{leaveTypeId}")]
    [Authorize("LeaveType_Delete")]
    public async Task<bool> DeleteLeaveType(int leaveTypeId)
    {
        await _leaveTypeService.DeleteByIdAsync(leaveTypeId);
        return _leaveTypeService.SaveChangesAsync();
    }

}
