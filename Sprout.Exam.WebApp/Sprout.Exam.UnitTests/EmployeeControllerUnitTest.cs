using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Services.Interfaces;
using Sprout.Exam.Entity.Entities;
using Sprout.Exam.WebApp.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.UnitTests
{
    public class EmployeeControllerUnitTest
    {
        private readonly Mock <ILogger<EmployeesController>> _mockLogger;
        private readonly ILogger<EmployeesController> _logger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IMapper _mapper;
                
        public EmployeeControllerUnitTest()
        {
            // Mock ILogger and IMapper, need to pass in the EmployeesController.
            _mockLogger = new Mock<ILogger<EmployeesController>>();
            _logger = _mockLogger.Object;

            _mockMapper = new Mock<IMapper>();
            _mapper = _mockMapper.Object;
        }

        [Fact]
        public async Task Task_GetAllAsync_Should_Return_OkResult()
        {
            // Arrange
            var employeeService = new Mock<IEmployeeService>();
                        
            employeeService.Setup(_ => _.GetEmployees()).ReturnsAsync(EmployeeMockData.GetEmployees());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            // Act
            var result = (OkObjectResult)await sut.Get();

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Task_GetById_Return_OkResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();
            int id = 3;

            employeeService.Setup(_ => _.GetEmployeeById(id)).ReturnsAsync(EmployeeMockData.GetEmployeeById());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var data = _mapper.Map<EmployeeDto>(EmployeeMockData.GetEmployeeById());
            var result = (OkObjectResult)await sut.GetById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Task_GetById_Return_NotFoundResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();
            int id = 4;

            employeeService.Setup(_ => _.GetEmployeeById(id)).ReturnsAsync(EmployeeMockData.GetEmployeeById());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var result = (NotFoundResult)await sut.GetById(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Task_Add_Employee_Return_CreatedResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();

            var employee = new CreateEmployeeDto() {
                FullName = "John Wick",
                Birthdate = new DateTime(1995, 03, 18),
                Tin = "232344353",
                TypeId = 1
            };

            var data = _mapper.Map<Employee>(employee);
            employeeService.Setup(_ => _.Add(data)).ReturnsAsync(EmployeeMockData.NewEmployee());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var result = (CreatedResult)await sut.Post(employee);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async void Task_Add_Employee_MatchResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();

            var employee = new CreateEmployeeDto()
            {
                FullName = "Johnny Bravoa",
                Birthdate = new DateTime(1986, 10, 3),
                Tin = "54353454354",
                TypeId = 1
            };

            var data = _mapper.Map<Employee>(employee);
            employeeService.Setup(_ => _.Add(data)).ReturnsAsync(EmployeeMockData.NewEmployee());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var result = (CreatedResult)await sut.Post(employee);

            //Assert
            Assert.IsType<CreatedResult>(result);

            var createdResult = result.Should().BeOfType<CreatedResult>().Subject;

            Assert.Equal(1, createdResult.Value);
        }

        [Fact]
        public async void Task_Update_Employee_Return_OkResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();

            var employee = new EditEmployeeDto()
            {
                Id = 3,
                FullName = "John Wick",
                Birthdate = new DateTime(1986, 10, 3),
                Tin = "54353454354",
                TypeId = 1
            };

            int id = 3;
            employeeService.Setup(_ => _.GetEmployeeById(id)).ReturnsAsync(EmployeeMockData.GetEmployeeById());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act            
            var data = _mapper.Map<Employee>(employee);
            employeeService.Setup(_ => _.Update(data)).Returns(Task.CompletedTask);
            var sut2 = new EmployeesController(_logger, _mapper, employeeService.Object);
            var updateResult = (OkObjectResult)await sut2.Put(employee);

            //Assert
            Assert.IsType<OkObjectResult>(updateResult);
        }

        [Fact]
        public async void Task_Update_Employee_Return_NotFoundResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();

            var employee = new EditEmployeeDto()
            {
                Id = 2,
                FullName = "James Doe",
                Birthdate = new DateTime(1988, 12, 23),
                Tin = "12345678",
                TypeId = 1
            };

            int id = 2;
            employeeService.Setup(_ => _.GetEmployeeById(id)).ReturnsAsync(EmployeeMockData.GetEmployeeById());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var result = (NotFoundResult)await sut.GetById(id);
            
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Task_Delete_Employee_Return_OkResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();
            var id = 1;

            employeeService.Setup(_ => _.GetEmployeeById(id)).ReturnsAsync(EmployeeMockData.GetEmployeeById());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var sut2 = new EmployeesController(_logger, _mapper, employeeService.Object);
            var updateResult = (OkObjectResult)await sut2.Delete(id);

            //Assert
            Assert.IsType<OkObjectResult>(updateResult);
        }

        [Fact]
        public async void Task_Delete_Employee_Return_NotFoundResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();
            var id = 1;

            employeeService.Setup(_ => _.GetEmployeeById(id)).ReturnsAsync(EmployeeMockData.GetEmployeeById());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var result = (NotFoundResult)await sut.GetById(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Task_Calculate_Employee_Regular_Salary_Should_Return_OkResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();
            var id = 3;

            employeeService.Setup(_ => _.GetEmployeeById(id)).ReturnsAsync(EmployeeMockData.GetEmployeeById());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            employeeService.Setup(_ => _.CalculateSalary(Common.Enums.EmployeeType.Regular, 2, 0)).Returns(1);
            var sut2 = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var result = (OkObjectResult)await sut2.Calculate(id, 2, 0);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Task_Calculate_Employee_Contractual_Salary_Should_Return_OkResult()
        {
            //Arrange
            var employeeService = new Mock<IEmployeeService>();
            var id = 3;

            employeeService.Setup(_ => _.GetEmployeeById(id)).ReturnsAsync(EmployeeMockData.GetEmployeeById());
            var sut = new EmployeesController(_logger, _mapper, employeeService.Object);

            employeeService.Setup(_ => _.CalculateSalary(Common.Enums.EmployeeType.Contractual, 0, 15)).Returns(1);
            var sut2 = new EmployeesController(_logger, _mapper, employeeService.Object);

            //Act
            var result = (OkObjectResult)await sut2.Calculate(id, 0, 15);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
