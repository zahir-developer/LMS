using AutoMapper;
using LMS.API.Controllers;
using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IServices;
using LMS.Application.IServiceMappings;
using LMS.Application.ServiceMappings;
using LMS.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace LMS.Tests;

public class AuthControllerTests
{
    private readonly Mock<ILogger<AuthController>> _logger;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IAuthTokenService> _authTokenServiceMock;
    private readonly Mock<IUserServiceMapping> _userMappingMock;
    private readonly AuthController _authController;
    private readonly Mock<IConfiguration> _config;
    
    public AuthControllerTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<AuthController>>();
        _config = new Mock<IConfiguration>();
        _userMappingMock = new Mock<IUserServiceMapping>();
        _authTokenServiceMock = new Mock<IAuthTokenService>();
        //_authController = new AuthController(_logger.Object, _authTokenServiceMock.Object);
    }

    [Test]
    public void Should_return_LoginDto_when_called()
    {
        //Assign
        LoginDto loginDto = new LoginDto();
        loginDto.Email = "admin@lms.com";
        loginDto.Password = "P@ssw0rd";

        //
        _authTokenServiceMock.Setup(s=>s.ValidateUser(loginDto)).Returns(It.IsAny<LoginResultDto>);

        _authTokenServiceMock.Verify(x=>x.ValidateUser(loginDto));

        Assert.Pass();
    }
}
