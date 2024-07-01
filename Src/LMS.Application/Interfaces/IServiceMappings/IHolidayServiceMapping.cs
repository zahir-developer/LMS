using LMS.Application.Interfaces.IServices;
using LMS.Domain.Entities;

namespace LMS.Application.Interfaces.IServiceMappings;

public interface IHolidayServiceMapping : IGenericServiceAsync<Holiday, HolidayDto>
{
    public bool ValidateLeaveDate(DateTime from, DateTime to);

}