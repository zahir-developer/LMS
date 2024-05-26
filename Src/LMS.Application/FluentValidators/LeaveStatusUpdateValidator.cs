using LMS.Application.DTOs;
using FluentValidation;

namespace LMS.Application.FluentValidators
{
    public class LeaveStatusUpdateValidator : AbstractValidator<LeaveStatusUpdateDto>
    {
        public LeaveStatusUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEqual(0).WithMessage("UserLeaveId can't be null or zero.");
            RuleFor(x => x.UserId).NotNull().NotEqual(0).WithMessage("UserId can't be null or zero.");
            RuleFor(x => x.Status).NotNull().WithMessage("status can't be null or zero.");
        }
    }
}