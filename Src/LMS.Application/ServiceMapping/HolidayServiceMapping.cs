using System.Data.Entity.SqlServer.Utilities;
using AutoMapper;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Services;
using LMS.Domain.Entities;

namespace LMS.Application.ServiceMappings;

public class HolidayServiceMapping : GenericServiceAsync<Holiday, HolidayDto>, IHolidayServiceMapping
{
    public HolidayServiceMapping(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {

    }

    public bool ValidateLeaveDate(DateTime from, DateTime to)
    {
        bool result = false;
        var holidayList = base.GetAllAsync().Result;

        if (holidayList != null)
        {
            for (var date = from; date <= to; date = date.AddDays(1))
            {
                var checkHoliday = holidayList.Where(s => s.Date.ToString("dd-MMM-yyy") == date.ToString("dd-MMM-yyy")).FirstOrDefault();

                return !(checkHoliday != null);
            }

        }

        return result;
    }
}
