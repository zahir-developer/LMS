
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Interfaces.IRepository;
using LMS.Application.Services;
using LMS.Application.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace LMS.Application.ServiceMappings
{
    public class LeaveTypeService : GenericServiceAsync<LeaveType, LeaveTypeDto>, ILeaveTypeService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LeaveTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}